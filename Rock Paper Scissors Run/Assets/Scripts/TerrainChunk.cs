using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TerrainChunkData")]
public class TerrainChunk : ScriptableObject
{
    public enum Direction { North, South, East, West }
    public Direction inDirection;
    public Direction outDirection;

    public Vector2 chunkSize = new Vector2(10f, 10f);

    public GameObject[] terrainChunks;

    
}
