using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{

    [SerializeField] private PersonajeStats stats;

    public PersonajeAtaque personajeAtaque { get; private set; }
    public PersonajeVida personajeVida { get; private set; }
    public PersonajeAnimaciones personajeAnimaciones { get; private set; }
    public PersonajeMana personajeMana { get; private set; }
    public PersonajeExperiencia personajeExp { get; private set; }

    public PersonajeVida PersonajeVida
    {
        get => default;
        set
        {
        }
    }

    public PersonajeMana PersonajeMana
    {
        get => default;
        set
        {
        }
    }

    public PersonajeAtaque PersonajeAtaque
    {
        get => default;
        set
        {
        }
    }

    public PersonajeAnimaciones PersonajeAnimaciones
    {
        get => default;
        set
        {
        }
    }

    public PersonajeExperiencia PersonajeExperiencia
    {
        get => default;
        set
        {
        }
    }

    private void Awake()
    {
        personajeAtaque = GetComponent<PersonajeAtaque>();
        personajeVida = GetComponent<PersonajeVida>();
        personajeAnimaciones = GetComponent<PersonajeAnimaciones>();
        personajeMana = GetComponent<PersonajeMana>();
        personajeExp = GetComponent<PersonajeExperiencia>();
    }

    public void restaurarPersonaje()
    {
        personajeVida.restaurarPersonaje();
        personajeAnimaciones.revivirPersonaje();
        personajeMana.restablecerMana();
    }

    //método que escucha el evento lanzado por los botones del script AtributoButton
    private void AtributoRespuesta(TipoAtributo tipo) 
    {
        if(stats.puntosDisponibles <=0) //si los puntos son cero o menos
        {
            return; //salgo del método
        }
        //segun el tipo de boton apretado
        switch (tipo)
        {
            case TipoAtributo.Fuerza:
                stats.Fuerza++;
                stats.anhadirBonusAtributoFuerza();
                break;
            case TipoAtributo.Inteligencia:
                stats.Inteligencia++;
                stats.anhadirBonusAtributoInteligencia();
                break;
            case TipoAtributo.Destreza:
                stats.Destreza++;
                //stats.anhadirBonusAtributoDestreza();
                break;
        }

        stats.puntosDisponibles -= 1;
    }

    private void OnEnable()
    {
        AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtributoButton.EventoAgregarAtributo -= AtributoRespuesta;

    }

}
