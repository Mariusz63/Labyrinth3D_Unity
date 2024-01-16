using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectPrefab;
    [Range(0f, 100f)] public float spawnChancePercentage = 10f;

    void Start()
    {
        SpawnRandomly();
    }

    private void SpawnRandomly()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue <= spawnChancePercentage)
        {
            // Spawn the object
            SpawnObject();
        }
        else
        {
            Destroy(gameObjectPrefab);
        }
    }

    private void SpawnObject()
    {
        // Instantiate the chest prefab or do any other spawning logic
        Instantiate(gameObjectPrefab, transform.position, Quaternion.identity);
    }
}
