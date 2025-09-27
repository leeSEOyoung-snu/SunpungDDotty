using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubakMainManager : MainManagerBase
{
    private int _score = 0;

    protected override void Init()
    {
        base.Init();
    }
    
    public override void UpdateScore(int delta)
    {
        _score += delta;
        base.UpdateScore();
    }

    public override void UpdateLife(int delta) { }

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
        return string.Empty;
    }
}
