using System.Collections;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public Transform spawner;
    public GameObject enemy;
    public float spawnRate = 2f;
    public bool canSpawn = true;
    public float randomSpawnRate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemies()
    {
        while (canSpawn)
        {
            randomSpawnRate = Random.Range(0.1f, 0.5f);
            yield return new WaitForSeconds(spawnRate + randomSpawnRate);
            Instantiate(enemy, spawner.position, Quaternion.Euler(0, 0, 0));
        }
        
    }
}
