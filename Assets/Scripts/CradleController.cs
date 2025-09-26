using UnityEngine;

public class CradleController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundedDistance;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    
    private readonly Vector2 _initPos = new Vector2(0, -1f);

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        transform.position = _initPos;
        rb.velocity = Vector2.zero;
    }
    
    private void Update()
    {
        if (transform.position.y < -6f)
        {
            Respawn();
            return;
        }
        
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundedDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * groundedDistance, Color.red);
        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            Move(Vector2.left);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            Move(Vector2.right);
        if (isGrounded && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))) 
            Jump();
    }

    private void Move(Vector2 direction)
    {
        rb.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Respawn()
    {
        transform.position = _initPos;
        rb.velocity = Vector2.zero;
    }
}
