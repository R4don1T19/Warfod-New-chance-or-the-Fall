using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    private Rigidbody2D rb;
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
    }
    private void FixedUpdate()
    {
        Walk(Input.GetAxisRaw("Horizontal"));
    }
    private void Walk(float localDirection)
    {
        rb.linearVelocity = new Vector2(localDirection * speed, rb.linearVelocity.y);
    }
}