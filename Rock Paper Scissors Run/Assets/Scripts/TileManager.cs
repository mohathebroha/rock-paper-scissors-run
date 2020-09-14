using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //public EnemyManager enemyManager;
    public Vector3 spawnOrigin = Vector3.zero;
    public List<GameObject> tilePrefabs;
    private List<GameObject> activeTiles;
    private Transform playerTransform;
    private float spawnLocationZ = 0f;
    private float deleteBuffer = 30.0f;
    private float tileLength = 30.0f;
    private int maxTilesOnScreen = 8;
    private int lastPrefabIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.Find("Player").transform;
        for (int i = 0; i < maxTilesOnScreen; ++i)
        {
            if (i < 1) SpawnTile(0);
            else SpawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsOnNextTile())
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject gameObject = (prefabIndex == -1) ? Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject
            : Instantiate(tilePrefabs[prefabIndex]) as GameObject;

        gameObject.transform.SetParent(transform);
        gameObject.transform.position = Vector3.forward * spawnLocationZ;
        spawnLocationZ += tileLength;
        activeTiles.Add(gameObject);
    }

    private bool PlayerIsOnNextTile()
    {
        return playerTransform.position.z - deleteBuffer > (spawnLocationZ - maxTilesOnScreen * tileLength);
    }

    private void DeleteTile()
    {
        var enemyManager = activeTiles[0].GetComponent<EnemyManager>();
        enemyManager.DespawnActiveEnemies();
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Count <= 1) return 0;
        int randomIndex = lastPrefabIndex;
        do {randomIndex = Random.Range(0, tilePrefabs.Count);}
            while (randomIndex == lastPrefabIndex);
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }


    public void ChangeSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin += originDelta;
    }
}
