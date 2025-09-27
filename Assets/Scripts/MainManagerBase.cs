using UnityEngine;

public abstract class MainManagerBase : MonoBehaviour
{
    public static MainManagerBase Instance { get; private set; }

    private CanvasManager _canvasManager;
    public bool IsGameOver { get; private set; }

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

    protected virtual void Init()
    {
        IsGameOver = false;
        _canvasManager = FindObjectOfType<CanvasManager>();
        _canvasManager.Init();
    }

    public abstract void UpdateScore(int delta);
    protected void UpdateScore()
    { 
        _canvasManager.UpdateScore();
    }

    public abstract void UpdateLife(int delta);
    protected void UpdateLife()
    {
        _canvasManager.UpdateLife();
    }

    protected void OnPlayerDeath()
    {
        Time.timeScale = 0;
        IsGameOver = true;
        
        _canvasManager.OnGameOver();
    }

    public abstract string GetFinalScore();
    public abstract string GetCurrScore();
    public abstract string GetCurrLife();
}
