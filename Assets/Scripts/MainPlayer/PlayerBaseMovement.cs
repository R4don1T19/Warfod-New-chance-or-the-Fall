using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    private PlayerSurfaceConfig PSC;
    private Rigidbody2D rb;
    private int fixedY = 0;
    public int speed;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        PSC = GetComponentInChildren<PlayerSurfaceConfig>();
    }
    private void FixedUpdate()
    {
        Walk(Input.GetAxisRaw("Horizontal"));
    }
    private void Walk(float localDirection)
    {
        Vector2 Walk = new Vector2(localDirection * speed, fixedY);
        // Игрик зафиксирован, чтобы не было воздействия на этот вектор движения.
        // За место Y в PSD установлен якорь(surfaceAnchor), который и тянет вниз.
        rb.Slide(Walk, Time.fixedDeltaTime, PSC.SlideConfig);
    }
}