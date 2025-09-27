using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DdottyControllerBase : MonoBehaviour
{
    protected Ddotty DdottyInfo;
    protected Rigidbody2D Rb;
    private SpriteRenderer _sr;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    public virtual void Init(Ddotty ddottyInfo)
    {
        DdottyInfo = ddottyInfo;
        
        transform.localScale = Vector3.one * DdottyInfo.Scale;
        _sr.sprite = Resources.LoadAll<Sprite>("DdottySheet")[ddottyInfo.Id];
    }
}
