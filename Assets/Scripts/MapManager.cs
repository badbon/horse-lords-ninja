using System.Collections;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject foodPrefab;
    public GameObject[] enemyPrefabs;
    public float enemySpawnInterval = 0.5f;
    public float itemSpawnInterval = 0.5f;
    public int enemyPreSpawnCount = 25;
    public bool enemyAggressive = false;
    public float mapWidth = 10;  // Number of tiles in width
    public float mapHeight = 10; // Number of tiles in height
    public float tileScale = 1f; // Scale of each tile
    public static MapManager instance;
    public Vector3 enemySpawnPointOffset;

    void Start()
    {
        instance = this;

        CreateTiles();
        PreSpawnEnemies();
        StartCoroutine(SpawnFood());
        InvokeRepeating("SpawnSingleEnemy", 0, enemySpawnInterval);
    }

    private void CreateTiles()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector2(x * tileScale, y * tileScale), Quaternion.identity);
                tile.transform.localScale = new Vector2(tileScale, tileScale);
                tile.transform.parent = transform;
            }
        }
    }

    private void PreSpawnEnemies()
    {
        for (int i = 0; i < enemyPreSpawnCount; i++)
        {
            SpawnSingleEnemy();
        }
    }

    private void SpawnSingleFood()
    {
        float randomX = Random.Range(0, mapWidth * tileScale) - (mapWidth * tileScale) / 2;
        float randomY = Random.Range(0, mapHeight * tileScale) - (mapHeight * tileScale) / 2;

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
        float randomX = Random.Range(0, mapWidth * tileScale) - (mapWidth * tileScale) / 2;
        float randomY = Random.Range(0, mapHeight * tileScale) - (mapHeight * tileScale) / 2;
        // use offset to ensure correct center
        randomX += enemySpawnPointOffset.x;
        randomY += enemySpawnPointOffset.y;

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject obj = Instantiate(enemyPrefabs[randomIndex], new Vector2(randomX, randomY), Quaternion.identity);

        obj.GetComponent<EnemyController>().aggressive = enemyAggressive;
        obj.transform.parent = transform;
    }
}
