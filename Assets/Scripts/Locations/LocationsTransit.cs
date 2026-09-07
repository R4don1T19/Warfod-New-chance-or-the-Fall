using UnityEngine;
using UnityEngine.SceneManagement;
public class LocationsTransit : MonoBehaviour
{
    private PlayerDoorDetect PDD;
    private Transform SpawnerTransform;
    private string ToID;
    private string FromID;
    private bool HasTransit = false;
    private void Start()
    {
        PDD = PlayerMovement.Instance.GetComponentInChildren<PlayerDoorDetect>();
    }
    private void Update()
    {
        if (PDD.readyToEnter)
        {
            if (Input.GetKeyDown(KeyCode.E))
                Transit(PDD.SceneToLoad);
        }
    }
    private void FindTheCorrectDoor(string ToID)
    {
        if (ToID == null)
            return;

        LocationsDoorID[] DoorsList = FindObjectsByType<LocationsDoorID>();
        foreach(LocationsDoorID Door in DoorsList)
        {
            if (Door.FromID == ToID)
            {
                SpawnerTransform = Door.transform;
                break;
            }
        }
    }
    private void Transit(string LocalScene)
    {
        ToID = PDD.ToID;
        FromID = PDD.FromID;
        HasTransit = true;
        SceneManager.LoadScene(LocalScene);
    }
    private void ChangePosition()
    {
        PlayerMovement.Instance.transform.position = SpawnerTransform.position;
    }
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (HasTransit)
        {
            FindTheCorrectDoor(ToID);
            ChangePosition();
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
