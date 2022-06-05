using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] puntos;
    public Vector3[] Puntos => puntos;

    public Vector3 PosicionActual { get; set; } //propiedad para generar los puntos en la posicion central del personaje seleccionado
    private bool juegoIniciado;

    private void Start()
    {
        juegoIniciado = true;
        PosicionActual = transform.position;
    }

    public Vector3 ObtenerPosMovimiento(int indice) //método para mover el NPC desde el indice 
    {
        return PosicionActual + puntos[indice];
    }

    private void OnDrawGizmos() //dibujamos nuestra herramienta para mover los NPC  por las lineas gizmos
    {
        if(juegoIniciado == false && transform.hasChanged) //Mientras no estemos jugando y si cambiamos la posicion de nuestro NPC
        {
            PosicionActual = transform.position; //Cambiamos la posicion de los puntos a la de nuestro NPC
        }

        if(puntos == null || puntos.Length <= 0)
        {
            return;
        }
        
        for(int i = 0; i< puntos.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(puntos[i] + PosicionActual, 0.5f);
            if(i < puntos.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(puntos[i] + PosicionActual, puntos[i + 1] + PosicionActual);
            }
        }
    }
}
