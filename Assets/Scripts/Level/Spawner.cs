using UnityEngine;

public sealed class Spawner : MonoBehaviour
{
    [SerializeField]
    private Player_Ship _player_Ship_Prefab;
    [SerializeField]
    private Enemy_Ship _enemy_Ship_Prefab;
    [SerializeField]
    private Enemy_Asteroid _enemy_Asteroid_Prefab;

    [SerializeField]
    private Screen_Borders _borders;
    [SerializeField]
    private Parametrs _parametrs;

    private Player_Ship _player_Ship;

    public void Spawn_Bullet (GameObject obj_Bullet, Game_UI game_UI, Transform direction, Vector3 spawn_Position, Bullet_Type bullet_Type, Color color)
    {
        if (obj_Bullet != null)
        {
            Bullet bullet = obj_Bullet.GetComponent<Bullet>();

            bullet.transform.position = spawn_Position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.Show();

            bullet.Init(_borders, game_UI, direction.up, bullet_Type, color);
        }
    }

    // asteroid children
    public void Spawn_Enemy_Asteroid(Base_Pooling base_Pooling, GameObject obj_Asteroid, Enemy_Wave enemy_Wave, Transform direction, float angle, AsteroidType asteroidType)
    {
        if (obj_Asteroid != null)
        {
            Enemy_Asteroid asteroid = obj_Asteroid.GetComponent<Enemy_Asteroid>();

            asteroid.transform.position = direction.position;
            asteroid.transform.localEulerAngles = new Vector3(0, 0, direction.localEulerAngles.z + angle);
            asteroid._asteroid_Type = asteroidType;

            asteroid.Init(_borders, base_Pooling, enemy_Wave);
            asteroid.Show();

            // Set scale
            for (int x = 0; x < _parametrs.Asteroid_parametrs.Count; x++)
            {
                if (_parametrs.Asteroid_parametrs[x].Asteroid_Type == asteroidType)
                {
                    asteroid.transform.localScale = _parametrs.Asteroid_parametrs[x].Asteroid_Scale;
                }
            }
        }
    }

    // asteroid parrent 
    public void Spawn_Enemy_Asteroid(Base_Pooling base_Pooling, GameObject obj_Asteroid, Enemy_Wave enemy_Wave)
    {
        Vector3 random_Position = Random.value < 0.5f ? new Vector3(_borders.Random_Bounds().x, _borders.Random_Range_Bounds().y, 0) :
                                                        new Vector3(_borders.Random_Range_Bounds().x, _borders.Random_Bounds().y, 0);

        if (obj_Asteroid != null)
        {
            Enemy_Asteroid asteroid = obj_Asteroid.GetComponent<Enemy_Asteroid>();

            asteroid.transform.position = random_Position;
            asteroid.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-360, 360));
            asteroid._asteroid_Type = AsteroidType.Big;

            asteroid.Init(_borders, base_Pooling, enemy_Wave);
            asteroid.Show();

            // Set scale
            for (int y = 0; y < _parametrs.Asteroid_parametrs.Count; y++)
            {
                if (_parametrs.Asteroid_parametrs[y].Asteroid_Type == AsteroidType.Big)
                {
                    asteroid.transform.localScale = _parametrs.Asteroid_parametrs[y].Asteroid_Scale;
                }
            }
        }
    }

    public void Spawn_Enemy_Ship(Base_Pooling base_Pooling, Game_UI game_UI, Enemy_Wave enemy_Wave)
    {
        Vector3 position = _borders.Random_Bounds();
        position.y = (Random.Range(position.y, -position.y) * 60) / 100;

        Enemy_Ship enemy_Ship = Instantiate(_enemy_Ship_Prefab, position, Quaternion.identity);

        enemy_Ship.Init(_borders, _player_Ship, base_Pooling, enemy_Wave, game_UI);
        enemy_Ship.Show();
    }

    public void Spawn_Player_Ship(Base_Pooling base_Pooling, Game_UI game_UI)
    {
        _player_Ship = Instantiate(_player_Ship_Prefab, Vector3.zero, Quaternion.identity);
        _player_Ship.Init(_borders, base_Pooling, game_UI);
    }
}
