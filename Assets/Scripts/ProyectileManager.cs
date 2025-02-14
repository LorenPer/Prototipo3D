using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//El proyectil funcionará distinto según su etiqueta.
public class ProyectileManager : MonoBehaviour
{
    [SerializeField] private int speed = 100; //Velocidad del projectil
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); //El projectil se moverá a la velocidad indicada en deltaTime
    }
    void Start()
    {
        Destroy(gameObject, 5); //El objeto se autodestruye tras pasar 5 segundos.
    }


}
