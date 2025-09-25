using System;
using UnityEngine;

public class ReceivePlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed, jumpForce;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            Move(Vector2.left);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            Move(Vector2.right);
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) 
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
}
