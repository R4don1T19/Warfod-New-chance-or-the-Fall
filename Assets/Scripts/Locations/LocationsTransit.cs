using UnityEngine;
using UnityEngine.SceneManagement;
public class LocationsTransit : MonoBehaviour
{
    private PlayerDoorDetect PDD;
    private Transform SpawnerTransform;
    private Rigidbody2D rb;
    private string ToID;
    private string FromID;
    private bool HasTransit;
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
                Debug.Log($"Find CorrectDoor! {SpawnerTransform.position}");
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
        // Первая строка изменяет позицию-физику, вторая же фактическую позицию(они конфликтуют при смене локации).
        PlayerMovement.Instance.GetComponent<Rigidbody2D>().position = SpawnerTransform.position;   
        PlayerMovement.Instance.transform.position = SpawnerTransform.position;
        Debug.Log("Change Position");
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
