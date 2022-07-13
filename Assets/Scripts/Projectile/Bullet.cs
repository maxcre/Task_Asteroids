using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Bullet : MonoBehaviour
{
    [SerializeField]
    private Bullet_Type _bullet_Type;

    [SerializeField]
    private float _speed = 0.025f;
    private float _total_Distance = 0f;

    private Vector3 _direction;
    private Vector3 _last_Position;

    private Screen_Borders _borders;
    private Game_Input _game_Input;
    private Game_UI _game_UI;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _game_Input = Game_Input.Instace;
    }

    private void OnEnable()
    {
        _last_Position = transform.position;
        _total_Distance = 0f;
    }

    private void Update()
    {
        if (!_game_Input.Pause)
        {
            Movement();
            Check_Bounds();
        }
    }

    private void LateUpdate()
    {
        _last_Position = transform.position;
    }

    public void Init(Screen_Borders borders, Game_UI game_UI, Vector3 direction, Bullet_Type bullet_Type, Color color)
    {
        _borders = borders;
        _game_UI = game_UI;
        _direction = direction;
        _bullet_Type = bullet_Type;

        _sprite.color = color;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, (transform.position + _direction), _speed);

        _total_Distance += Vector3.Distance(_last_Position, transform.position);

        if (_total_Distance > _borders.Set_Horizontal() * 2)
        {
            Hide();
        }
    }

    private void Check_Bounds()
    {
        transform.position = _borders.Check_Bounds(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Ship player_Ship = null;
        Enemy_Ship enemy_Ship = null;
        Enemy_Asteroid enemy_Asteroid = null;

        if (collision.gameObject.TryGetComponent(out Enemy_Ship enemy))
            enemy_Ship = enemy;

        if (collision.gameObject.TryGetComponent(out Player_Ship player))
            player_Ship = player;

        if (collision.gameObject.TryGetComponent(out Enemy_Asteroid asteroid))
            enemy_Asteroid = asteroid;


        switch (_bullet_Type)
        {
            case Bullet_Type.Player:

                if(enemy_Ship != null)
                {
                    _game_UI.Add_Score(200);
                    enemy_Ship.Take_Damage();
                }

                if(enemy_Asteroid != null)
                {
                    switch(enemy_Asteroid._asteroid_Type)
                    {
                        case AsteroidType.Big:
                            _game_UI.Add_Score(20);
                            break;

                        case AsteroidType.Medium:
                            _game_UI.Add_Score(50);
                            break;

                        case AsteroidType.Small:
                            _game_UI.Add_Score(100);
                            break;
                    }
                    enemy_Asteroid.Take_Damage();
                }

                break;

            case Bullet_Type.Enemy:

                if(player_Ship != null)
                {
                    player_Ship.Take_Damage();
                }

                if (enemy_Asteroid != null)
                {
                    enemy_Asteroid.Take_Damage();
                }

                break;
        }

        Hide();
    }
}

public enum Bullet_Type
{
    Player,
    Enemy
}
