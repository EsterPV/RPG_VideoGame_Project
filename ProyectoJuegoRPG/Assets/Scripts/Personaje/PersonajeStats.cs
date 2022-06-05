using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Stats")]
public class PersonajeStats : ScriptableObject
{
    [Header("Stats")]
    public float Danho = 5f;
    public float Defensa = 2f;
    public float Velocidad = 5f;
    public float Nivel;
    public float ExpActual;
    public float ExpReq;
    [Range(0f, 100f)] public float PorcentajeCritico;
    [Range(0f, 100f)] public float PorcentajeBloqueo;

    [Header("Atributos")]
    public int Fuerza;
    public int Inteligencia;
    public int Destreza;

    [HideInInspector]public int puntosDisponibles;

    public void anhadirBonusAtributoFuerza()
    {
        Danho += 2f;
        Defensa += 1f;
        PorcentajeBloqueo += 0.03f;
    }

    public void anhadirBonusAtributoInteligencia()
    {
        Danho += 3f;
        PorcentajeCritico += 0.02f;
    }

    public void AnhadirBonusPorArma(Arma arma)
    {
        Danho += arma.danho;
        PorcentajeCritico += arma.chanceCritico;
        PorcentajeBloqueo += arma.chanceBloqueo;
    }

    public void BorrarBonusPorArma(Arma arma)
    {
        Danho -= arma.danho;
        PorcentajeCritico -= arma.chanceCritico;
        PorcentajeBloqueo -= arma.chanceBloqueo;
    }

    public void resetearValores()
    {
        Danho = 5f;
        Defensa = 2f;
        Velocidad = 5f;
        Nivel = 1;
        ExpActual = 0f;
        ExpReq = 0f;
        PorcentajeBloqueo = 0f;
        PorcentajeCritico = 0f;

        Fuerza = 0;
        Inteligencia = 0;
        Destreza = 0;
        puntosDisponibles = 0;
    }

    

}
