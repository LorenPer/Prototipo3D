using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject panelGameOver; //Guardamos el objeto deel panel de fin de partida.
    [SerializeField] private EnemyGenerator enemyGenerator; //Guardamos el objeto del generador de enemigos.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método que se ejecutará tras morir el jugador
    public void GameOver()
    {
        panelGameOver.SetActive(true); //Se activa el panel de fin de partida.
        enemyGenerator.enabled = false; //Se desactiva el generador de enemigos.
        Cursor.lockState = CursorLockMode.Confined; //El cursos se desbloquea.
    }

    //Método para recargar la escena. Este será ejecutado por el botón del panel de fin de partida.
    public void LoadSceneLevel()
    {
        SceneManager.LoadScene("Game");
        
    }
}
