using UnityEngine;

public class PlayerBaseMovement : MonoBehaviour
{
    public static PlayerBaseMovement Instance;
    private PlayerSurfaceConfig PSC;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private Quaternion FixedA = new Quaternion(0, 180, 0, 0);
    private Quaternion FixedB = new Quaternion(0, 0, 0, 0);
    private float fixedYA = 1.0f; // фиксированный игрик для перехода между локациями
    private int fixedYB = 0; // фиксированный игрик для передвижения в целом 
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
        float localDirection = Input.GetAxisRaw("Horizontal");
        Walk(localDirection);
        FlipThePlayer(localDirection);
        if (localDirection == 0)
            animator.SetBool("Run", false);
        else
            animator.SetBool("Run", true);
    }
    private void Walk(float direction)
    {
        Vector2 Walk = new Vector2(direction * speed, fixedYB);
        // Игрик зафиксирован, чтобы не было воздействия на этот вектор движения.
        // За место Y в PSD установлен якорь(surfaceAnchor), который и тянет вниз.
        rb.Slide(Walk, Time.fixedDeltaTime, PSC.SlideConfig);
    }
    private void FlipThePlayer(float direction)
    {
        if (direction < 0)
            transform.rotation = FixedA;
        else if (direction > 0)
            transform.rotation = FixedB;
    }
    public void ChangePositionOnTransit(Vector3 localPosition)
    {
        // Первая строка изменяет позицию-физику, вторая же фактическую позицию(они конфликтуют при смене локации).
        rb.position = new Vector2(localPosition.x, localPosition.y + fixedYA);
        transform.position = new Vector2(localPosition.x, localPosition.y + fixedYA);
    }
}