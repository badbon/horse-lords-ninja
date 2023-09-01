using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject foodPrefab;
    public GameObject[] enemyPrefabs;
    public float enemySpawnInterval = 0.5f;
    public float itemSpawnInterval = 0.5f;
    public int enemyPreSpawnCount = 25;
    public bool enemyAggressive = false; // Will enemies attack unprovoked by default?
    public float mapWidth = 100f;  // Size of the map width
    public float mapHeight = 100f; // Size of the map height

    void Start()
    {
        PreSpawnEnemies();
        StartCoroutine(SpawnFood());
        InvokeRepeating("SpawnEnemy", 0, enemySpawnInterval);
    }

    // Function to pre-spawn food
    private void PreSpawnEnemies()
    {
        for (int i = 0; i < enemyPreSpawnCount; i++)
        {
            SpawnSingleEnemy();
        }
    }

    private void SpawnSingleFood()
    {
        float randomX = Random.Range(-mapWidth / 2, mapWidth / 2);
        float randomY = Random.Range(-mapHeight / 2, mapHeight / 2);

        GameObject obj = Instantiate(foodPrefab, new Vector2(randomX, randomY), Quaternion.identity);
        obj.transform.parent = transform;
    }

    IEnumerator SpawnFood()
    {
        while (true)
        {
            SpawnSingleFood();
            yield return new WaitForSeconds(itemSpawnInterval);
        }
    }

    public void SpawnSingleEnemy()
    {
        float randomX = Random.Range(-mapWidth / 2, mapWidth / 2);
        float randomY = Random.Range(-mapHeight / 2, mapHeight / 2);

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject obj = Instantiate(enemyPrefabs[randomIndex],new Vector2(randomX, randomY),
         Quaternion.identity);
        
        obj.GetComponent<EnemyController>().aggressive = enemyAggressive;

        obj.transform.parent = transform;
    }
}
