using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player_Ship : MonoBehaviour, ITakeDamage
{
    [SerializeField]
    private float _acceleration = 5.0f;
    [SerializeField]
    private float _max_Speed = 0.1f;

    [SerializeField]
    private float _rotation_Speed_Keyboard;
    [SerializeField]
    private float _rotation_Speed_Mouse;

    private float _current_Speed;
    private const float INERTIA_BRAKING = 1.0f;

    private bool _dead;

    private readonly string _key_Input = "Input_Save";

    private Vector3 _inertia;

    [SerializeField]
    private Transform _bullet_Spawn;

    private Screen_Borders _borders;
    private Base_Pooling _base_Pooling;
    private Game_Input _game_Input;
    private Game_UI _game_UI;
    private SpriteRenderer _sprite;
    private PolygonCollider2D _collider;
    private Animator _animator;

    public bool Invulnerability
    {
        get; private set;
    }

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _game_Input = Game_Input.Instace;
    }

    private void Update()
    {
        if (!_dead && !_game_Input.Pause)
        {
            Check_Save_Input();

            Check_Bounds();
            Movement();
            Attack();
            Animator();
        }
    }

    public void Init(Screen_Borders borders, Base_Pooling base_Pooling, Game_UI game_UI)
    {
        _borders = borders;
        _base_Pooling = base_Pooling;
        _game_UI = game_UI;

        StartCoroutine(_invulnerability());
    }

    public void Take_Damage()
    {
        if(!Invulnerability)
        {
            _game_UI.Remove_Health(1);
            _dead = true;
            StartCoroutine(_respawn());
        }
    }

    private void Movement()
    {
        if (Input.GetAxis("Vertical") > 0)
            _current_Speed = Input.GetAxis("Vertical") * _acceleration * Time.deltaTime;
        else
            _current_Speed = 0;

        _inertia += transform.up * _current_Speed * Time.deltaTime;
        _inertia = Vector3.ClampMagnitude(_inertia, _max_Speed);
        _inertia *= INERTIA_BRAKING;

        transform.Translate(_inertia, Space.World);
    }

    private void Rotation_Keyboard()
    {
        float angle = -Input.GetAxis("Horizontal");
        transform.RotateAround(transform.position, Vector3.forward, angle * _rotation_Speed_Keyboard * Time.deltaTime);
    }

    private void Rotation_Mouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePos - transform.position;
        direction.Normalize();

        transform.up = Vector2.Lerp(transform.up, direction, Time.deltaTime * _rotation_Speed_Mouse);
    }

    private void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            _base_Pooling.Spawn_Bullet(_game_UI, transform, _bullet_Spawn.position, Bullet_Type.Player, Color.green);
        }
    }

    private void Check_Bounds()
    {
        transform.position = _borders.Check_Bounds(transform.position);
    }

    private void Animator()
    {
        _animator.SetBool("invulnerability", Invulnerability);
    }

    private void Check_Save_Input()
    {
        // get save
        if (PlayerPrefs.HasKey(_key_Input))
        {
            int key = PlayerPrefs.GetInt(_key_Input);

            switch (key)
            {
                // keyboard
                case 0:
                    Rotation_Keyboard();
                    break;
                // keyboard + mouse
                case 1:
                    Rotation_Mouse();
                    break;
            }
        }
        else
        {
            Rotation_Keyboard();
        }
    }


    private IEnumerator _respawn()
    {
        _sprite.enabled = false;
        _collider.enabled = false;

        yield return new WaitForSeconds(1);

        _dead = false;

        _inertia = Vector3.zero;
        transform.position = _borders.Random_Range_Bounds();

        _sprite.enabled = true;
        _collider.enabled = true;

        StartCoroutine(_invulnerability());
    }
    
    private IEnumerator _invulnerability()
    {
        Invulnerability = true;
        yield return new WaitForSeconds(3.0f);
        Invulnerability = false;
    }
}
