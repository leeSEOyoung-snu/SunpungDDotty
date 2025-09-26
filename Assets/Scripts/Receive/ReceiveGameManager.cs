using System;
using TMPro;
using UnityEngine;

public class ReceiveGameManager : MonoBehaviour
{
    public static ReceiveGameManager Instance { get; private set; }
    
    [SerializeField] private GameObject mobPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private int _score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else
        {
            Destroy(this);
        }
    }

    private void Init()
    {
        
    }

    public void UpdateScore(int delta)
    {
        _score += delta;
        scoreText.text = _score.ToString();
    }

    public void SpawnMob()
    {
        Instantiate(mobPrefab);
    }
}
