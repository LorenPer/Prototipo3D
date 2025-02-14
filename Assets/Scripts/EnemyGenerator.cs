using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject enemyPrefab; //Objeto a generar
    [SerializeField] private Transform[] enemySpawners; //Emptys donde se van a generar los enemigos
    [SerializeField] private float spawnColdDown = 10; //Tiempo entre cada enemigo generado

    private void CreateEnemies()
    {
        //Metodo para que se genere un enemigo ubicado en un empty aleatorio
        int n = Random.Range(0, enemySpawners.Length);
        Instantiate(enemyPrefab, enemySpawners[n].position, enemySpawners[n].rotation);
    }
    void Start()
    {
        InvokeRepeating("CreateEnemies", 1.0f, spawnColdDown); //Invoke repeating para que se reactive el metodo para generar un enemigo
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
