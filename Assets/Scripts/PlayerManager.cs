using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    //Variables del movimiento del jugador.
    [SerializeField] private float speed = 20;
    [SerializeField] private float turnSpeed = 50;

    //Tiempo entre disparos
    [SerializeField] private float fireRate = 3;

    //Cámara principal
    [SerializeField] private Camera mainCamera;


    //Variables para la barra de salud.
    [SerializeField] private int maxHealth = 100;
    private float currentHealth = 100;
    [SerializeField] private float damageTaken = 5;
    [SerializeField] private Image lifeBar;

    //Variables para la barra de recarga.
    public Image reloadBar;
    public float maxReload = 3;
    public float currentReload = 0;
    private bool canShoot = true; //Booleano que determinará si el jugador puede disparar o no.

    public Rigidbody rb;
    private AudioSource[] audioSources; //Array que almacena todos los AudioSources del Objeto

    //Particulas
    [SerializeField] private ParticleSystem smallExplosion, bigExplosion;


    private void Start()
    {
        
        
    }

    private void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>(); //Guardamos el Rigid Body del objeto en una variable para que sea mas facil trabajar con el.
        currentHealth = maxHealth; //La vida se llena al máximo al iniciar la partida.
        lifeBar.fillAmount = 1; 
        audioSources = GetComponents<AudioSource>(); //Guardamos todos los AudioSources del componente en el array.
        currentReload = maxReload; //La barra de recarga se llema al máximo.
    }
    
    private void Update()
    {
        /*
         * Disparará cuando presionemos el click del ratón y la variable canShoot sea verdadera.
         * De esa forma, el disparo tendrá un tiempo para poder disparar de nuevo.
        */
        if (Input.GetMouseButtonDown(0) && canShoot) 
        {
            StartCoroutine(Shoot(fireRate));
        }
    }

    //FixedUpdate para los movimientos con físicas.
    private void FixedUpdate()
    {
        Movimiento();
        Turn();
        
    }
    
    private void Movimiento() 
    {
        float accelerate = Input.GetAxis("Vertical");
        rb.AddForce(transform.forward * accelerate * speed);
    }
    
    private void Turn()
    {
        float xRotate = Input.GetAxis("Mouse X");
        rb.AddTorque(transform.up * xRotate * turnSpeed);
    }
    

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shooter;

    //Corrutina para poder disparar con un tiempo de pausa.
    private IEnumerator Shoot (float tiempo)
    {
        canShoot = false;
        reloadBar.fillAmount = 0.0f; // La barra se vacia al disparar
        Instantiate(bulletPrefab, shooter.position, shooter.rotation);
        audioSources[1].Play(); //Reproducimos el segundo audio del array. Disparo.
        yield return new WaitForSeconds(1); //Esperamos un segundo antes de que se reproduzca el sonido de recarga
        audioSources[2].Play(); //Se reproduce el tercer sonido del array. Recarga.
        yield return new WaitForSeconds(tiempo-1);// La barra se llena cuando se puede volver a disparar.
        reloadBar.fillAmount = maxReload;
        canShoot = true;
    }

    //Método para la muerte del jugador
    private void Death()
    {
        mainCamera.transform.SetParent(null); //La cámara deja de seguir al jugador.
        Destroy(gameObject); // El objeto del jugador es destruido
        gameManager.GameOver(); //Ejecuta el método de fin de partida del script GameManager.
    }

    //Método del collider para detectar los proyectiles que lo golpean.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet")) // Si el projectil es de un enemigo reducirá su vida, tambien la barra de vida se reducirá
        {
            currentHealth -= damageTaken;
            lifeBar.fillAmount = currentHealth / maxHealth;
            smallExplosion.Play(); //Se ejecutan la particula de explosión
            audioSources[1].Play(); //Suena el disparo.
            Destroy(other.gameObject); //El proyectil se destruye
            if (currentHealth <= 0)  //Si la vida baja de 0 se ejecutará del método de muerte.
            { 
                Death();
            }
        }
    }

}
