using UnityEngine;

public class ReceiveMobController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    private bool _isDead = false;
    private int _collisionCnt = 0;
    
    private const int MaxCollisionCnt = 4;
    private const float RandVelY = 8f, RandVelXRange = 2f;
    private const float InitVelY = 12f, InitVelXMin = 1f, InitVelXMax = 2.5f;

    private void Start()
    {
        float velX = UnityEngine.Random.Range(InitVelXMin, InitVelXMax);
        velX *= gameObject.transform.position.x > 0f ? -1f : 1f;
        Vector2 initVel = new Vector2(velX, InitVelY);
        rb.velocity = initVel;
    }

    private void SetRandomVelocity()
    {
        float velX = UnityEngine.Random.Range(-1f * RandVelXRange, RandVelXRange);
        rb.velocity = new Vector2(velX, RandVelY);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDead) return;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            _collisionCnt++;
            
            if (_collisionCnt == MaxCollisionCnt)
            {
                // 받아내기를 모두 끝냄
                _isDead = true;
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = Vector2.zero;
                gameObject.GetComponent<Collider2D>().enabled = false;
                
                ReceiveGameManager.Instance.UpdateScore();
                Destroy(gameObject);
            }
            else if (_collisionCnt < MaxCollisionCnt)
                SetRandomVelocity();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            // 바닥에 떨어짐
            _isDead = true;
            rb.bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<Collider2D>().enabled = false;
            
            ReceiveGameManager.Instance.UpdateLife();
            Destroy(gameObject);
        }
    }
}
