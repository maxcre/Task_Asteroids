using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class Score : MonoBehaviour
{
    private int _amount_Score = 0;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Start()
    {
        Add_Score(0);
    }

    public void Add_Score(int count)
    {
        _amount_Score += count;
        _text.text = "Score: " + _amount_Score.ToString();
    }
}
