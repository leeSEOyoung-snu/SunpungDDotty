using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MommyCannonController : MonoBehaviour
{
    [Header("Programmer")]
    [SerializeField] private bool moveX;
    [SerializeField] private bool moveY;
    [SerializeField] private float moveRange;
    [SerializeField] private float directionFactor;
    
    [Header("Level Design")]
    [SerializeField] private float moveSpeed;

    private float _center;

    private void Awake()
    {
        _center = moveX ? transform.position.x : transform.position.y;
    }

    private void Update()
    {
        float criteria = moveX ? transform.position.x - _center : transform.position.y - _center;
        
        directionFactor = 
            criteria >= moveRange ? -1f 
            : criteria <= moveRange * -1f ? 1f 
            : directionFactor;
        
        Vector2 direction = new Vector2(moveX ? 1 : 0, moveY ? 1 : 0);
        transform.Translate(direction * directionFactor * Time.deltaTime * moveSpeed);
    }
}
