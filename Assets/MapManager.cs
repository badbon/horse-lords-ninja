using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject foodPrefab;
    public float spawnInterval = 2f;
    public float cameraHeight;
    public float cameraWidth;


    void Start()
    {
        StartCoroutine(SpawnFood());
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = cameraHeight * Camera.main.aspect;
    }

    IEnumerator SpawnFood()
    {
        while (true)
        {
            float randomX = Random.Range(-cameraWidth / 2, cameraWidth / 2);
            float randomY = Random.Range(-cameraHeight / 2, cameraHeight / 2);


            Instantiate(foodPrefab, new Vector2(randomX, randomY), Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
