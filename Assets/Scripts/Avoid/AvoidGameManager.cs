using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AvoidGameManager : MonoBehaviour
{
    public static AvoidGameManager Instance { get; private set; }

    [SerializeField] private GameObject ddottyPrefab;

    private void Awake()
    {
        var newD = Instantiate(ddottyPrefab, new Vector3(-9, 0, 0), Quaternion.identity);
        newD.GetComponent<AvoidDdottyController>().Init(2, Vector2.right);
    }
}
