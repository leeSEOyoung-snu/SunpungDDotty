using System;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Header("In Game")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifeText;
    
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    public void Init()
    {
        scoreText.text = MainManagerBase.Instance.GetCurrScore();
        lifeText.text = MainManagerBase.Instance.GetCurrLife();
    }

    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = MainManagerBase.Instance.GetFinalScore();
    }

    public void UpdateScore()
    {
        scoreText.text = MainManagerBase.Instance.GetCurrScore();
    }

    public void UpdateLife()
    {
        lifeText.text = MainManagerBase.Instance.GetCurrLife();
    }
}
