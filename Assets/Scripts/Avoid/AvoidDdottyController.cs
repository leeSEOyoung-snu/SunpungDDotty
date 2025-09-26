using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidDdottyController : MonoBehaviour
{
    private Vector2 _direction;
    private int _ddottyId;
    
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private const float DestroyDistance = 20f;
    private const float MoveSpeed = 3f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    public void Init(int id, Vector2 dir)
    {
        _ddottyId = id;
        _sr.sprite = Resources.LoadAll<Sprite>("DdottySheet")[id];
        
        _direction = dir;
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        float distance = 0f;

        while (distance < DestroyDistance)
        {
            transform.Translate(_direction * MoveSpeed * Time.deltaTime);
            distance += MoveSpeed * Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
