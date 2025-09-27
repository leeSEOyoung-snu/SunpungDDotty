using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ReceiveMainManager : MainManagerBase
{
    [FormerlySerializedAs("mobPrefab")]
    [Header("Programmer")]
    [SerializeField] private GameObject ddottyPrefab;
    [SerializeField] private Transform ddottyParent;

    [Header("Level Design")]
    [SerializeField] private List<ReceiveDdotty> ddottyInfo;
    [SerializeField] private float spawnTimeMin;
    [SerializeField] private float spawnTimeMax;
    
    private int _score = 0;
    private int _life = 3;
    
    private readonly Vector2 _ddottyInitPos = new Vector2(8f, -4f);

    protected override void Init()
    {
        base.Init();
        
        ReserveSpawn();
    }

    public override void UpdateScore(int delta)
    {
        _score += delta;
        base.UpdateScore();
    }

    public override void UpdateLife(int delta)
    {
        _life += delta;
        base.UpdateLife();

        if (_life <= 0)
            base.OnPlayerDeath();
    }

    private void SpawnDdotty()
    {
        int sign = Random.Range(0, 2) * 2 - 1;
        Vector2 initPos = new Vector2(_ddottyInitPos.x * sign, _ddottyInitPos.y);
        GameObject newMob = Instantiate(ddottyPrefab, initPos, Quaternion.identity, ddottyParent);
        
        int randInt = Random.Range(0, ddottyInfo.Count);
        newMob.AddComponent<ReceiveDdottyController>().Init(ddottyInfo[randInt]);
    }

    private void ReserveSpawn()
    {
        if (base.IsGameOver) return;
        SpawnDdotty();
        float reserveTime = Random.Range(spawnTimeMin, spawnTimeMax);
        Invoke(nameof(ReserveSpawn), reserveTime);
    }
    
    public override string GetFinalScore()
    {
        return "Score: " + _score;
    }

    public override string GetCurrScore()
    {
        return "Score: " + _score;
    }

    public override string GetCurrLife()
    {
        return "Life: " + _life;
    }
}
