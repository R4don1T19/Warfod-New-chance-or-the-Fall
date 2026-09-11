using UnityEngine;
public class CameraBehavior : MonoBehaviour
{
    public static CameraBehavior Instance;
    [SerializeField] private Transform PlayerTransform;
    private Vector3 CameraPosition;
    private int FixedPositionZ = -10;
    private int FixedPositionYA = 3;
    private int FixedPositionYB = 2;
    private float FixedPositionForLerp = 0.5f;
    public bool CameraBindedToDialogue = false;
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
        if (!CameraBindedToDialogue)
            BoundToPlayer();
        else
            BoundToDialogue();
    }
    private void BoundToPlayer()
    {
        CameraPosition = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y + FixedPositionYA, FixedPositionZ);
        transform.position = CameraPosition;
    }
    public void BoundToDialogue()
    {
        Transform NPCTransform = PlayerTransform.GetComponentInChildren<DialoguePlayerDetect>().NPCTrasform;
        Vector3 npcPosition = new Vector3(NPCTransform.position.x, NPCTransform.position.y + FixedPositionYB, FixedPositionZ);

        Vector3 PlayerPosition = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y + FixedPositionYB, FixedPositionZ);

        Vector3 PositionBetweenAandB = Vector3.Lerp(npcPosition, PlayerPosition, FixedPositionForLerp);
        transform.position = PositionBetweenAandB;
    }
}
