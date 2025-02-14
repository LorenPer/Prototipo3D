using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private AudioSource[] audioSources; //Array que alamcenará todas los AudioSource del objeto.
    [SerializeField] private float maxHealth = 100; //Vida máxima
    private float currentHealth = 100; //Vida actual
    [SerializeField] private float damageTaken = 25; //Daño recivido
    [SerializeField] private ParticleSystem smallExplosion, bigExplosion;

    //Variables de movimiento
    [SerializeField] private float speed = 20; //Velocidad de movimiento del enemigo
    [SerializeField] private float distanceToPlayer = 50; //Distancia en la que queremos que se mantenga el enemigo ante el jugador
    [SerializeField] private GameObject player; //El objetivo al que se dirigirá el enemigo, el cual será el jugador.

    [SerializeField] private GameObject bulletPrefab; //El prefab del projectil que va a disparar
    [SerializeField] private Transform shooter; //Empty que será el punto donde aparecerán los proyectiles al dispararlos.
    [SerializeField] private float shootDelay = 3; //Tiempo entre cada disparo

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //Busca de forma autamatica el objeto con la etiqueta "Player" que será el jugador.
        audioSources = GetComponents<AudioSource>(); //Guarda en el array audioSources todos los audios del objeto.
        InvokeRepeating("Attack", 1, shootDelay); //Invoke repeating para que dispare de forma repetida.
        currentHealth = maxHealth; //Que la vida actual del enemigo sea la máxima al ser generado.
        //Detenemos las animaciones de las particulas para que no se reproduzcan al generarse.
        smallExplosion.Stop();
        bigExplosion.Stop();
    }

    private void Update()
    {
        //Si no existe el jugador, que no traten de perseguirlo
        if (player == null)
        {
            return;
        }

        //Que el enemigo mire y persiga al jugador siempre
        transform.LookAt(player.transform.position);
        FollowPlayer();
    }

    //Método para disparar que sera ejecytado por un Unvoke Repeating.
    private void Attack()
    {
        //El metodo ejecutará el sonido de disparo y generará un projectil.
        audioSources[0].Play();
        Instantiate(bulletPrefab, shooter.position, shooter.rotation);
    }

    //Método para mantener la distancia con el jugador.
    private void FollowPlayer()
    {
        //Primero calculará la distancia con el jugador y luego las compara para indicar si debe seguir a delante o parar.
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > distanceToPlayer)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    //Método que se ejecuta al entrar un objeto con su collider, que servirá para que reciba daño y morir.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet")){ //Si el proyectil es del jugador disminuirá su vida,destruir el proyectil, ejecutar la explosión y su sonido
            currentHealth -= damageTaken;
            Destroy(other.gameObject);
            audioSources[0].Play();
            smallExplosion.Play();
            Debug.Log(currentHealth);
            if (currentHealth <= 0) { //Si la vida baja a 0 se ejecutará un metodo para morir.
                Death();
            }
        }
    }

    //Metodo que se ejecuta al morir el enemigo.
    private void Death()
    {
        bigExplosion.Play(); //Se reproducirá una explosión grande.
        audioSources[1].Play(); //Se reproducirá un sonido de explosión
        Destroy(gameObject,1.0f); //El enemigo se destruirá.
    }

}
