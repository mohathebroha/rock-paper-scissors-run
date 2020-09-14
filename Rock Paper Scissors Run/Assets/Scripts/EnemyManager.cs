using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float obstacleSpaceRadius = 1f;
    public int maxSpawnAttemptsPerObstacle = 20;
    public int maxEnemies = 8;

    public GameObject enemyRock;
    public GameObject enemyScissors;
    public GameObject enemyPaper;

    private GameObject[] enemyModels;

    private int numberOfEnemies;
    private Transform prefabTransform;
    private Vector3 prefabPosition;
    private GameObject selectedEnemy;
    private List<GameObject> activeEnemies;


    // Start is called before the first frame update
    void Start()
    {
        activeEnemies = new List<GameObject>();
        InitializeEnemyModels();

        prefabTransform = gameObject.transform;
        prefabPosition = prefabTransform.localPosition;
        numberOfEnemies = PickNumberOfEnemies();
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeEnemyModels()
    {
        enemyModels = new GameObject[3]
        {
            //enemyRock, enemyScissors
            enemyRock, enemyPaper, enemyScissors
        };
    }

    private void SpawnEnemies()
    {
       // Debug.Log("attempting spawn");
        for (int i = 0; i < numberOfEnemies; ++i)
        {
            selectedEnemy = enemyModels[Random.Range(0, enemyModels.Length)];
            int spawnAttempts = 0;
            Vector3 enemyPosition = Vector3.zero;
            bool isValidPosition = false;
            while (!isValidPosition && spawnAttempts < maxSpawnAttemptsPerObstacle)
            {
                spawnAttempts++;

                enemyPosition = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(0.6f, 1.5f), Random.Range(prefabPosition.z - 30, prefabPosition.z + 30));

                Collider[] colliders = Physics.OverlapSphere(enemyPosition, obstacleSpaceRadius);
                if (colliders.Length == 0) isValidPosition = true;
            }
            if (isValidPosition)
            {
                //Debug.Log("Instantiating enemy");
                Quaternion quaternion = Quaternion.identity;
                if (selectedEnemy == enemyPaper)
                {
                    quaternion *= Quaternion.Euler(0, 180, 0);
                }
                else if (selectedEnemy == enemyScissors)
                {
                    quaternion *= Quaternion.Euler(0, 90, 0);
                }
                GameObject instantiatedEnemy = Instantiate(selectedEnemy, enemyPosition + selectedEnemy.transform.position, quaternion);
                activeEnemies.Add(instantiatedEnemy);
            }
            else
            {
                //Debug.Log("No valid position found");
            }

        }
    }

    private int PickNumberOfEnemies()
    {
        return Random.Range(2, maxEnemies);
    }

    public void DespawnActiveEnemies()
    {
        foreach (GameObject item in activeEnemies)
        {
            //Destroy(item);
            //item.GetComponent<EnemyBehavior>().DespawnEnemy();
        }
        activeEnemies.Clear();
        Debug.Log("Destroyed enemies");
    }
}
