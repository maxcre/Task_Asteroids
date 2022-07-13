using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Screen_Borders : MonoBehaviour
{
    private float _horizontal_Border;
    private float _vertical_Border;

    private Rect _screen_Border;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;

        if (_camera != null)
        {
            _horizontal_Border = _camera.orthographicSize / Screen.height * Screen.width;
            _vertical_Border = _camera.orthographicSize;

            _screen_Border = new Rect(-_horizontal_Border, -_vertical_Border, _horizontal_Border * 2, _vertical_Border * 2);
        }
        else
        {
            Debug.LogError("Camera not found!");
        }
    }

    public Vector3 Check_Bounds(Vector3 position)
    {
        if (!Rect_Contains(position))
        {
            if (position.x > _screen_Border.width / 2 || position.x < _screen_Border.x)
            {
                return new Vector3(position.x * -1, position.y, 0);
            }
            if (position.y < _screen_Border.y || position.y > _screen_Border.height / 2)
            {
                return new Vector3(position.x, position.y * -1, 0);
            }
        }

        return position;
    }

    public Vector3 Random_Bounds()
    {
        return new Vector2(Random.value < 0.5f ? _horizontal_Border : -_horizontal_Border,
                           Random.value < 0.5f ? _vertical_Border : -_vertical_Border);
    }

    public Vector3 Random_Range_Bounds()
    {
        return new Vector2(Random.Range(_horizontal_Border, -_horizontal_Border),
                           Random.Range(_vertical_Border, -_vertical_Border));
    }

    public bool Rect_Contains(Vector2 position)
    {
        if (_screen_Border.Contains(position))
            return true;

        return false;
    }

    public float Set_Horizontal()
    {
        return _horizontal_Border;
    }

    public float Set_Vertical()
    {
        return _vertical_Border;
    }
}
