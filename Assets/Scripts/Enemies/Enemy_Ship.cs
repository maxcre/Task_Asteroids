using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Enemy_Ship : MonoBehaviour, ITakeDamage
{
    [SerializeField]
    private float _speed;

    private float _attack_Time;
    private float _attack_Value;

    private Vector3 _direction;

    private Screen_Borders _borders;
    private Base_Pooling _base_Pooling;
    private Game_Input _game_Input;
    private Game_UI _game_UI;
    private Enemy_Wave _enemy_Wave;
    private Player_Ship _target;

    [SerializeField]
    private Transform _bullet_Spawn;

    private void Start()
    {
        _game_Input = Game_Input.Instace;
    }

    private void Update()
    {
        if (!_game_Input.Pause)
        {
            Attack();
            Movement();
            Rotation();
            Check_Bounds();
        }
    }

    public void Init(Screen_Borders borders, Player_Ship target, Base_Pooling base_Pooling, Enemy_Wave enemy_Wave, Game_UI game_UI)
    {
        _borders = borders;
        _target = target;
        _base_Pooling = base_Pooling;
        _enemy_Wave = enemy_Wave;
        _game_UI = game_UI;

        if (transform.position.x < 0)
            _direction = transform.right;
        else
            _direction = -transform.right;

        Attack_Value_Random();
    }

    public void Show()
    {
        _enemy_Wave.Add_Enemy_Ship();
    }

    public void Hide()
    {
        Destroy(gameObject);

        _enemy_Wave.Remove_Enemy_Ship();
    }

    public void Take_Damage()
    {
        Hide();
    }

    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, (transform.position + _direction), _speed);
    }

    private void Rotation()
    {
        Vector2 direction = _target.gameObject.transform.position - transform.position;
        direction.Normalize();

        transform.up = Vector2.Lerp(transform.up, direction, Time.deltaTime * 999f);
    }

    private void Attack()
    {
        _attack_Time += Time.deltaTime;
        if (_attack_Time >= _attack_Value)
        {
            _base_Pooling.Spawn_Bullet(_game_UI, transform, _bullet_Spawn.position, Bullet_Type.Enemy, Color.red);

            Attack_Value_Random();

            _attack_Time = 0.0f;
        }
    }

    private void Attack_Value_Random()
    {
        _attack_Value = Random.Range(2, 6);
    }

    private void Check_Bounds()
    {
        transform.position = _borders.Check_Bounds(transform.position);
    }
}
