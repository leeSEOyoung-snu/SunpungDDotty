using System;
using UnityEngine;

public class ReceivePlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            Move(Vector2.left);
        else if (Input.GetKey(KeyCode.RightArrow))
            Move(Vector2.right);
    }

    private void Move(Vector2 direction)
    {
        rb.AddForce(direction * moveSpeed, ForceMode2D.Impulse);
    }
}
