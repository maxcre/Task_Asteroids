using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class Health : MonoBehaviour
{
    [SerializeField]
    private int _amount_Health = 3;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Start()
    {
        Remove_Health(0);
    }

    public void Remove_Health(int count)
    {
        _amount_Health -= count;
        _text.text = "Health: " + _amount_Health.ToString();

        if (_amount_Health <= 0)
            SceneManager.LoadScene(1);
    }
}
