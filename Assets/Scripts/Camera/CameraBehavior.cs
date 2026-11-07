using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
public class CameraBehavior : MonoBehaviour
{
    public static CameraBehavior Instance;
    [SerializeField] private Transform PlayerTransform;
    [Header("Границы для камеры")]
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;
    private float upBorder;
    private float downBorder;
    private Vector3 CameraPosition;
    private int FixedPositionZ = -10;
    private int FixedPositionYA = 3; // Привязка камеры к игроку
    private int FixedPositionYB = 1; // Привязка камеры к диалогу
    private float FixedPositionForLerp = 0.5f;
    public bool CameraBindedToDialogue = false;
    public bool CameraInCutscene = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    private void Start()
    {
        PlayerTransform = PlayerBaseMovement.Instance.transform;
    }
    private void FixedUpdate()
    {
        if (CameraInCutscene)
            return;
        else if (!CameraBindedToDialogue)
            BoundToPlayer();
        else if (CameraBindedToDialogue)
            BoundToDialogue();

        IfCameraSpawnedAfterBorder();
    }
    private void BoundToPlayer()
    {
        CameraPosition = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y + FixedPositionYA, FixedPositionZ);
        transform.position = CameraPosition;
    }
    public void BoundToDialogue()
    {
        // мне лень менять, короче теперь не координаты НПС, а координаты объекта, на котором и есть диалог(при настройки учитывать это), суть не меняет.
        Transform NPCTransform = PlayerTransform.GetComponentInChildren<DialoguePlayerDetect>().DialogueObject.transform;
        if (NPCTransform == null)
        {
            BoundToPlayer();
            return;
        }    
        Vector3 npcPosition = new Vector3(NPCTransform.position.x, NPCTransform.position.y + FixedPositionYB, FixedPositionZ);

        Vector3 PlayerPosition = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y + FixedPositionYB, FixedPositionZ);

        Vector3 PositionBetweenAandB = Vector3.Lerp(npcPosition, PlayerPosition, FixedPositionForLerp);
        transform.position = PositionBetweenAandB;
    }
    public void IfCameraSpawnedAfterBorder()
    {
        float PosX = Mathf.Clamp(transform.position.x, leftBorder, rightBorder);
        float PosY = Mathf.Clamp(transform.position.y, downBorder, upBorder);
        transform.position = new Vector3(PosX, PosY, FixedPositionZ);
    }
    public void GetBordersData(DataForBorders DFB)
    {
        leftBorder = DFB.LeftBorder;
        rightBorder = DFB.RightBorder;
        upBorder = DFB.UpBorder;
        downBorder = DFB.DownBorder;
    }
}
