using System;
using UnityEngine;

[Serializable]
public class ReceiveDdotty : Ddotty
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float mass;
    public float RandVelY => jumpForce;
    public float Mass => mass;
}

public class ReceiveDdottyController : DdottyControllerBase
{
    private bool _isDead = false;
    private int _collisionCnt = 0;
    
    private float _randVelY;
    
    private const int MaxCollisionCnt = 4;
    private const float RandVelXRange = 2f;
    private const float InitVelY = 12f, InitVelXMin = 1f, InitVelXMax = 2.5f;

    public override void Init(Ddotty dottyInfo)
    {
        base.Init(dottyInfo);

        try
        {
            ReceiveDdotty receiveDdotty = dottyInfo as ReceiveDdotty;
            Rb.mass = receiveDdotty.Mass;
            _randVelY = receiveDdotty.RandVelY;
        }
        catch (NullReferenceException e)
        { 
            Debug.LogError($"Receive Ddotty Type Casting Error [{e.Message}]");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        float velX = UnityEngine.Random.Range(InitVelXMin, InitVelXMax);
        velX *= gameObject.transform.position.x > 0f ? -1f : 1f;
        Vector2 initVel = new Vector2(velX, InitVelY);
        Rb.velocity = initVel;
    }

    private void SetRandomVelocity()
    {
        float velX = UnityEngine.Random.Range(-1f * RandVelXRange, RandVelXRange);
        Rb.velocity = new Vector2(velX, _randVelY);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead) return;
        
        if (other.gameObject.CompareTag("Cradle"))
        {
            _collisionCnt++;
            
            if (_collisionCnt == MaxCollisionCnt)
            {
                // 받아내기를 모두 끝냄
                _isDead = true;
                Rb.bodyType = RigidbodyType2D.Kinematic;
                Rb.velocity = Vector2.zero;
                gameObject.GetComponent<Collider2D>().enabled = false;
                
                ReceiveMainManager.Instance.UpdateScore(1);
                Destroy(gameObject);
            }
            else if (_collisionCnt < MaxCollisionCnt)
                SetRandomVelocity();
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            // 바닥에 떨어짐
            _isDead = true;
            Rb.bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<Collider2D>().enabled = false;
            
            ReceiveMainManager.Instance.UpdateLife(-1);
            Destroy(gameObject);
        }
    }
}
