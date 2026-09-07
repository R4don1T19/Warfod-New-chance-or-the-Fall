using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;
    private Vector3 CameraPosition;
    private int FixedPositionZ = -10;
    private int FixedPositionY = 2;
    private void Start()
    {
        PlayerTransform = PlayerMovement.Instance.transform;
    }
    private void FixedUpdate()
    {
        BoundToPlayer();
    }
    private void BoundToPlayer()
    {
        CameraPosition = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y + FixedPositionY, FixedPositionZ);
        transform.position = CameraPosition;
    }
}
