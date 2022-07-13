using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Enemy_Wave : MonoBehaviour
{
    [SerializeField]
    private Base_Pooling _base_Pooling;

    private List<Enemy_Asteroid> _asteroids = new List<Enemy_Asteroid>();

    private int _amount = 2;

    private float _time;


    private void Start()
    {
        StartCoroutine(_respawn_Enemy_Ship());
    }

    private void Add_Wave()
    {
        _amount++;

        StartCoroutine(_base_Pooling.Spawn_Enemy_Asteroid(2.0f, _amount));
    }

    public void Add_Asteroid(Enemy_Asteroid asteroid)
    {
        _asteroids.Add(asteroid);
    }

    public void Remove_Asteroid(Enemy_Asteroid asteroid)
    {
        _asteroids.Remove(asteroid);

        if (_asteroids.Count <= 0)
            Add_Wave();
    }


    public void Add_Enemy_Ship()
    {

    }

    public void Remove_Enemy_Ship()
    {

        StartCoroutine(_respawn_Enemy_Ship());
    }


    private IEnumerator _respawn_Enemy_Ship()
    {
        _time = UnityEngine.Random.Range(20f, 40f);

        yield return new WaitForSeconds(_time);

        _base_Pooling.Spawn_Enemy_Ship();
    }
}
