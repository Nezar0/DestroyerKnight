using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject enemy;
    void Start()
    {
        InvokeRepeating("Spawn", 0f, 1f);
    }
    void Spawn()
    {
        if (Random.Range(0, 5) == 0)
        {
            Instantiate(enemy, new Vector2(transform.position.x + Random.Range(50, 100), transform.position.y - 1.5f), Quaternion.identity);
        }
        if (Random.Range(0, 5) != 0)
        {
            Instantiate(enemy, new Vector2(transform.position.x - Random.Range(50, 100), transform.position.y - 1.5f), Quaternion.identity);
        }
    }
}
