using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Enemy_Asteroid : MonoBehaviour, ITakeDamage
{
    public AsteroidType _asteroid_Type;

    [SerializeField]
    private float _min_Speed = 0.002f;
    [SerializeField]
    private float _max_Speed = 0.004f;
    private float _speed;

    private Screen_Borders _borders;
    private Base_Pooling _base_Pooling;
    private Game_Input _game_Input;
    private Enemy_Wave _enemy_Wave;

    private void Start()
    {
        _game_Input = Game_Input.Instace;
    }

    private void Update()
    {
        if (!_game_Input.Pause)
        {
            Movement();
            Check_Bounds();
        }
    }

    public void Init(Screen_Borders borders, Base_Pooling base_Pooling, Enemy_Wave enemy_Wave)
    {
        _borders = borders;
        _base_Pooling = base_Pooling;
        _enemy_Wave = enemy_Wave;

        _speed = Random.Range(_min_Speed, _max_Speed);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        _enemy_Wave.Add_Asteroid(this);
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        _enemy_Wave.Remove_Asteroid(this);
    }

    public void Take_Damage()
    {
        switch (_asteroid_Type)
        {
            case AsteroidType.Big:

                _base_Pooling.Spawn_Enemy_Asteroid(transform, 45.0f, AsteroidType.Medium);
                _base_Pooling.Spawn_Enemy_Asteroid(transform, -45.0f, AsteroidType.Medium);

                break;

            case AsteroidType.Medium:

                _base_Pooling.Spawn_Enemy_Asteroid(transform, 45.0f, AsteroidType.Small);
                _base_Pooling.Spawn_Enemy_Asteroid(transform, -45.0f, AsteroidType.Small);

                break;
        }

        Hide();
    }

    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, (transform.position + transform.up), _speed);
    }

    private void Check_Bounds()
    {
        transform.position = _borders.Check_Bounds(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player_Ship player_Ship))
        {
            if (!player_Ship.Invulnerability)
            {
                player_Ship.Take_Damage();
                Hide();
            }
        }

        if (collision.gameObject.TryGetComponent(out Enemy_Ship enemy_Ship))
        {
            enemy_Ship.Take_Damage();
            Hide();
        }
    }
}
