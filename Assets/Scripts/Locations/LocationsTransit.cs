using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LocationsTransit : MonoBehaviour
{
    public static LocationsTransit Instance;
    public bool CutSceneTransit = false;
    private PlayerDoorDetect PDD;
    private Transform SpawnerTransform;
    private Rigidbody2D rb;
    private string ToID;
    private string FromID;
    private bool HasTransit;
    private float BlackFE = 1f;
    private float WhiteFE = 0f;
    private float FixedTimeForFE = 1f;
    private string FixedScene;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }
    private void Start()
    {
        PDD = PlayerBaseMovement.Instance.GetComponentInChildren<PlayerDoorDetect>();
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
        PlayerBaseMovement.Instance.enabled = false;
        FixedScene = LocalScene;

        CameraFadeEffect.Instance.OnFadeComplete += AfterFadeEffect;
        StartCoroutine(CameraFadeEffect.Instance.FadeEffect(BlackFE, FixedTimeForFE));
    }
    private void AfterFadeEffect()
    {
        CameraFadeEffect.Instance.OnFadeComplete -= AfterFadeEffect;
        SceneManager.LoadScene(FixedScene);
        FixedScene = null;
    }
    private void ChangePosition()
    {  
        PlayerBaseMovement.Instance.ChangePositionOnTransit(SpawnerTransform.position);
        Debug.Log("Change Position");
    }
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        DataForBorders DFB = FindAnyObjectByType<DataForBorders>();
        if (DFB != null)
            CameraBehavior.Instance.GetBordersData(DFB);
        CameraBehavior.Instance.IfCameraSpawnedAfterBorder();

        if (CutSceneTransit)
        {
            GameObject Spawner = GameObject.Find("CutSceneSpawner");
            if (Spawner == null)
            {
                Debug.LogError("В загруженной сцене нет SceneChangeTarget!");
                return;
            }
            PlayerBaseMovement.Instance.ChangePositionOnTransit(Spawner.transform.position);
            CutSceneTransit = false;
            HasTransit = false;
            return;
        }
        if (HasTransit)
        {
            FindTheCorrectDoor(ToID);
            ChangePosition();
            StartCoroutine(CameraFadeEffect.Instance.FadeEffect(WhiteFE, FixedTimeForFE));
            PlayerBaseMovement.Instance.enabled = true;
            HasTransit = false;
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
