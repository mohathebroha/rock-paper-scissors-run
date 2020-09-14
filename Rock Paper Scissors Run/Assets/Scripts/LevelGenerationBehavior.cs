using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;

public class LevelGenerationBehavior : MonoBehaviour
{
    public TerrainChunk[] terrainChunks;
    public TerrainChunk firstChunkGenerated;

    private TerrainChunk prevChunkGenerated;

    public Vector3 spawnOrigin;
    private Vector3 spawnPosition;

    public int chunksToSpawn = 10;
    // Start is called before the first frame update
    void Start()
    {
        prevChunkGenerated = firstChunkGenerated;
        for (int i = 0; i < chunksToSpawn; ++i)
        {
            PickAndSpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            PickAndSpawn();
        }
    }

    TerrainChunk PickTerrainChunk()
    {
        List<TerrainChunk> validChunkList = new List<TerrainChunk>();
        TerrainChunk nextChunk = null;
        TerrainChunk.Direction nextDirection;

        switch (prevChunkGenerated.outDirection)
        {
            case TerrainChunk.Direction.North:
                nextDirection = TerrainChunk.Direction.South;
                spawnPosition += new Vector3(0, 0, prevChunkGenerated.chunkSize.y);
                break;
            case TerrainChunk.Direction.South:
                nextDirection = TerrainChunk.Direction.North;
                spawnPosition += new Vector3(0, 0, -prevChunkGenerated.chunkSize.y);
                break;
            case TerrainChunk.Direction.East:
                nextDirection = TerrainChunk.Direction.West;
                spawnPosition += new Vector3(prevChunkGenerated.chunkSize.x, 0, 0);
                break;
            case TerrainChunk.Direction.West:
                nextDirection = TerrainChunk.Direction.East;
                spawnPosition += new Vector3(-prevChunkGenerated.chunkSize.x, 0, 0);
                break;
            default:
                nextDirection = TerrainChunk.Direction.North;
                break;
        }

        foreach(TerrainChunk chunk in terrainChunks)
        {
            if (chunk.inDirection == nextDirection) validChunkList.Add(chunk);
        }

        nextChunk = validChunkList[Random.Range(0, validChunkList.Count)];
        return nextChunk;
    }

    void PickAndSpawn()
    {
        TerrainChunk nextChunk = PickTerrainChunk();
        GameObject chunkSpawnObject = nextChunk.terrainChunks[Random.Range(0, nextChunk.terrainChunks.Length)];
        prevChunkGenerated = nextChunk;
        Instantiate(chunkSpawnObject, spawnPosition + spawnOrigin, Quaternion.identity);
    }

    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin += originDelta;
    }

    private void OnEnable()
    {

    }
}
