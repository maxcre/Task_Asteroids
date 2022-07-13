using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Object_Pooling : MonoBehaviour
{
    [SerializeField]
    private Pooling_Type _pooling_Type;

    public GameObject Get_Object()
    {
        return gameObject;
    }
    public bool Get_State()
    {
        return gameObject.activeInHierarchy;
    }
    public bool Check_Matches(Pooling_Type pooling_Type)
    {
        if (pooling_Type == _pooling_Type)
            return true;

        return false;
    }
}
public enum Pooling_Type
{
    Asteroid,
    Bullet
}
