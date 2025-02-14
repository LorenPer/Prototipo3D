using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private float speed = 20;
    [SerializeField] private float turnSpeed = 50;
    [SerializeField] private float fireRate = 3;

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
        Rigidbody rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        lifeBar.fillAmount = 1;
        audioSources = GetComponents<AudioSource>(); //Guardamos todos los AudioSources del componente en el arra
        currentReload = maxReload;
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
        audioSources[1].Play(); //Reproducimos el segundo audio del array.
        yield return new WaitForSeconds(1); //Esperamos un segundo antes de que se reproduzca el sonido de recarga
        audioSources[2].Play(); //Se reproduce el tercer sonido del array.
        yield return new WaitForSeconds(tiempo-1);// La barra se llena cuando se puede volver a disparar.
        reloadBar.fillAmount = maxReload;
        canShoot = true;
    }

    private void Death()
    {
        mainCamera.transform.SetParent(null);
        Destroy(gameObject);
        gameManager.GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            currentHealth -= damageTaken;
            lifeBar.fillAmount = currentHealth / maxHealth;
            smallExplosion.Play();
            audioSources[1].Play();
            Destroy(other.gameObject);
            if (currentHealth <= 0) 
            { 
                Death();
            }
        }
    }

}
