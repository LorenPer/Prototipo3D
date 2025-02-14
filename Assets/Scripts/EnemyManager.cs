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
    [SerializeField] private float speed = 20;
    [SerializeField] private float distanceToPlayer = 50;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shooter;
    [SerializeField] private float shootDelay = 3;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSources = GetComponents<AudioSource>();
        InvokeRepeating("Attack", 1, shootDelay);
        currentHealth = maxHealth;
        smallExplosion.Stop();
        bigExplosion.Stop();
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        transform.LookAt(player.transform.position);
        FollowPlayer();
    }

    private void Attack()
    {
        audioSources[0].Play();
        Instantiate(bulletPrefab, shooter.position, shooter.rotation);
    }

    private void FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > distanceToPlayer)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet")){
            currentHealth -= damageTaken;
            Destroy(other.gameObject);
            audioSources[0].Play();
            smallExplosion.Play();
            Debug.Log(currentHealth);
            if (currentHealth <= 0) {
                Death();
            }
        }
    }

    private void Death()
    {
        bigExplosion.Play();
        audioSources[1].Play();
        Destroy(gameObject,1.0f);
    }

}
