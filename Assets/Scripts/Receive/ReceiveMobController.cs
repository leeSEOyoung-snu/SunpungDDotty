using System;
using UnityEngine;

public class ReceiveMobController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    private int _collisionCnt = 0;
    
    private const int MaxCollisionCnt = 4;
    private const float DefaultVelocityY = 8f, VelocityXRange = 2f;

    private void Awake()
    {
        SetRandomVelocity();
    }

    private void SetRandomVelocity()
    {
        float velX = UnityEngine.Random.Range(-1f * VelocityXRange, VelocityXRange);
        rb.velocity = new Vector2(velX, DefaultVelocityY);
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
