using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaConfiner : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camara;

    private void OnTriggerEnter2D(Collider2D collision) //método que se llama cuando un objeto entra en colision con nuestro confiner de area
    {
        if (collision.CompareTag("Player")) //si el personaje está entrando en nuestro confiner
        {
            camara.gameObject.SetActive(true); //activamos la camara
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //método que se llama cuando un objeto sale de nuestro confiner de area
    {
        if (collision.CompareTag("Player")) //si el personaje está saliendo de nuestro confiner
        {
            camara.gameObject.SetActive(false); //desactivamos la camara
        }
    }
}
