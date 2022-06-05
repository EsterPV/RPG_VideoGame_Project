using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeDetector : MonoBehaviour
{
    public static Action<EnemigoInteraccion> EventoEnemigoDetectado;
    public static Action EventoEnemigoPerdido;

    public EnemigoInteraccion enemigoDetectado { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            enemigoDetectado = collision.GetComponent<EnemigoInteraccion>();
            if(enemigoDetectado.GetComponent<EnemigoVida>().Salud > 0)
            {
                EventoEnemigoDetectado?.Invoke(enemigoDetectado);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            EventoEnemigoPerdido?.Invoke();
        }
    }
}
