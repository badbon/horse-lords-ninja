using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject foodPrefab;
    public float spawnInterval = 2f;
    public int preSpawnCount = 100; // Amount of food to spawn at the beginning
    public float mapWidth = 100f;  // Size of the map width
    public float mapHeight = 100f; // Size of the map height

    void Start()
    {
        PreSpawnFood();
        StartCoroutine(SpawnFood());
    }

    // Function to pre-spawn food
    private void PreSpawnFood()
    {
        for (int i = 0; i < preSpawnCount; i++)
        {
            SpawnSingleFood();
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
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
