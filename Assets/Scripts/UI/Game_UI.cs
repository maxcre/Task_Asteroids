using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Game_UI : MonoBehaviour
{
    private Score _score;
    private Health _health;

    private void Awake()
    {
        _score = GetComponentInChildren<Score>();
        _health = GetComponentInChildren<Health>();
    }

    public void Add_Score(int score)
    {
        _score.Add_Score(score);
    }

    public void Remove_Health(int count)
    {
        _health.Remove_Health(count);
    }
}
