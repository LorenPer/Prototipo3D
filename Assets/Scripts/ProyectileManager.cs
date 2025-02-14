using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileManager : MonoBehaviour
{
    [SerializeField] private int speed = 100;
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void Start()
    {
        Destroy(gameObject, 5);
    }


}
