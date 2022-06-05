using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBase : MonoBehaviour
{
    [SerializeField] protected float saludInicial;
    [SerializeField] protected float saludMax;

    public float Salud { get; protected set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Salud = saludInicial;
    }

    public void recibirDaño(float cantidad)
    {
        if(cantidad <= 0) //Si la cantidad de salud es igual o menor a 0
        {
            return; //dejamos de leer 
        }

        if (Salud > 0f) //si salud es mayor que 0
        {
            Salud -= cantidad; //resta a salud la cantidad que cnviene
            ActualizarBarraVida(Salud, saludMax);
            if(Salud <= 0f)
            {
                Salud = 0f;
                ActualizarBarraVida(Salud, saludMax);
                personajeDerrotado();
            }
        }
    }

    protected virtual void ActualizarBarraVida(float vidaActual, float vidaMax)
    {

    }

    protected virtual void personajeDerrotado()
    {

    }

}
