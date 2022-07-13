using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Base_Pooling : MonoBehaviour
{
    [SerializeField]
    private List<Object_Pooling> _pool_Prefabs = new List<Object_Pooling>();

    [SerializeField]
    private int _pool_Amount;

    [SerializeField]
    private Spawner _spawner;
    [SerializeField]
    private Enemy_Wave _enemy_wave;
    [SerializeField]
    private Game_UI _game_UI;

    private List<Object_Pooling> _pooled_Objects = new List<Object_Pooling>();

    private void Start()
    {
        Instantiate_Pooling(_pool_Amount);

        Spawn_Player_Ship();
        StartCoroutine(Spawn_Enemy_Asteroid(0.0f, 2));
    }

    private void Instantiate_Pooling(int amount)
    {
        Object_Pooling object_Pooling;

        for (int x = 0; x < amount; x++)
        {
            for (int y = 0; y < _pool_Prefabs.Count; y++)
            {
                object_Pooling = Instantiate(_pool_Prefabs[y]);
                object_Pooling.gameObject.SetActive(false);
                _pooled_Objects.Add(object_Pooling);
            }
        }
    }

    private GameObject Get_Pooled_Object(Pooling_Type pooling_Type)
    {
        for (int i = 0; i < _pool_Amount; i++)
        {
            if (_pooled_Objects[i].Check_Matches(pooling_Type) && !_pooled_Objects[i].Get_State())
            {
                return _pooled_Objects[i].Get_Object();
            }
        }
        return null;
    }


    public void Spawn_Bullet(Game_UI game_UI, Transform direction, Vector3 spawn_Position, Bullet_Type bullet_Type, Color color)
    {
        _spawner.Spawn_Bullet(Get_Pooled_Object(Pooling_Type.Bullet),game_UI, direction, spawn_Position, bullet_Type, color);
    }

    // asteroid children
    public void Spawn_Enemy_Asteroid(Transform direction, float angle, AsteroidType asteroidType)
    {
        _spawner.Spawn_Enemy_Asteroid(this, Get_Pooled_Object(Pooling_Type.Asteroid), _enemy_wave, direction, angle, asteroidType);
    }

    // asteroid parrent 
    public IEnumerator Spawn_Enemy_Asteroid(float time, int amount)
    {
        yield return new WaitForSeconds(time);

        for (int x = 0; x < amount; x++)
        {
            _spawner.Spawn_Enemy_Asteroid(this, Get_Pooled_Object(Pooling_Type.Asteroid), _enemy_wave);
        }
    }

    public void Spawn_Enemy_Ship()
    {
        _spawner.Spawn_Enemy_Ship(this, _game_UI,_enemy_wave);
    }

    public void Spawn_Player_Ship()
    {
        _spawner.Spawn_Player_Ship(this, _game_UI);
    }
}
