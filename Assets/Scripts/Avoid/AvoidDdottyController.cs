using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class AvoidDdotty : Ddotty
{
    [SerializeField] private float moveSpeed;

    [HideInInspector] public Vector2 direction;
    
    public float MoveSpeed => moveSpeed;
}

public class AvoidDdottyController : DdottyControllerBase
{
    private Vector2 _direction;
    private float _moveSpeed;

    private const float DestroyDistance = 20f;

    public override void Init(Ddotty dottyInfo)
    {
        base.Init(dottyInfo);

        try
        {
            AvoidDdotty receiveDdotty = dottyInfo as AvoidDdotty;
            _moveSpeed = receiveDdotty.MoveSpeed;
            _direction = receiveDdotty.direction;
        }
        catch (NullReferenceException e)
        { 
            Debug.LogError($"Avoid Ddotty Type Casting Error [{e.Message}]");
            Destroy(gameObject);
        }

        Rb.bodyType = RigidbodyType2D.Kinematic;
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        float distance = 0f;

        while (distance < DestroyDistance)
        {
            transform.Translate(_direction * _moveSpeed * Time.deltaTime);
            distance += _moveSpeed * Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cradle"))
        {
            AvoidMainManager.Instance.UpdateLife(-1);
            Destroy(gameObject);
        }
    }
}
