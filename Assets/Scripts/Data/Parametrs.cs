using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Parametrs", order = 1)]
public class Parametrs : ScriptableObject
{
    public List<Asteroid_Parametrs> Asteroid_parametrs;

    [System.Serializable]
    public struct Asteroid_Parametrs
    {
        public AsteroidType Asteroid_Type;
        public Vector3 Asteroid_Scale;
    }
}

public enum AsteroidType
{
    Big,
    Medium,
    Small
}
