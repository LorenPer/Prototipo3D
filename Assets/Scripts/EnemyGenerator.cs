using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] enemySpawners;
    [SerializeField] private float spawnColdDown = 10;

    private void CreateEnemies()
    {
        int n = Random.Range(0, enemySpawners.Length);
        Instantiate(enemyPrefab, enemySpawners[n].position, enemySpawners[n].rotation);
    }
    void Start()
    {
        InvokeRepeating("CreateEnemies", 1.0f, spawnColdDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
