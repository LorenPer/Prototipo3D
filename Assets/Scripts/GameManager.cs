using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private EnemyGenerator enemyGenerator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        panelGameOver.SetActive(true);
        enemyGenerator.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadSceneLevel()
    {
        SceneManager.LoadScene("Game");
        
    }
}
