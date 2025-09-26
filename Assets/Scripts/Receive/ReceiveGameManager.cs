using TMPro;
using UnityEngine;

public class ReceiveGameManager : MonoBehaviour
{
    public static ReceiveGameManager Instance { get; private set; }
    
    [SerializeField] private GameObject mobPrefab;
    [SerializeField] private TextMeshProUGUI scoreText, lifeText;
    [SerializeField] private float minRespawnTime, maxRespawnTime;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    
    private int _score = 0;
    private int _life = 3;

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
        scoreText.text = _score.ToString();
        lifeText.text = _life.ToString();
    }

    public void UpdateScore(int delta = 1)
    {
        _score += delta;
        scoreText.text = _score.ToString();
    }

    public void UpdateLife(int delta = -1)
    {
        _life += delta;
        lifeText.text = _life.ToString();

        if (_life <= 0)
            OnPlayerDeath();
    }

    public void SpawnMob()
    {
        Instantiate(mobPrefab);
    }

    private void OnPlayerDeath()
    {
        finalScoreText.text = "Score: " + _score.ToString();
        gameOverPanel.SetActive(true);
    }
}
