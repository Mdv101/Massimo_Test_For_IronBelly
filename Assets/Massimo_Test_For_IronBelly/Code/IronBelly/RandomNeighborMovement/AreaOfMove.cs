using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class AreaOfMove
{
    public float speed;
    
    [Header("Min Value")]
    public float minX;
    public float minY;
    public float minZ;
    
    [Header("Max Value")]
    public float maxX;
    public float maxY;
    public float maxZ;

    public Vector3 GetRandomPointInArea()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        float z = Random.Range(minZ, maxZ);
        
        return new Vector3(x,y,z);
    }
}