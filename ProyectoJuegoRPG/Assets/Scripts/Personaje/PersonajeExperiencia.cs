using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats; //almacenar el nivel en el que se encuentra nuestro personaje

    [Header("Config")]
    [SerializeField] private int nivelMax; //nivel máximo al que puede llegar el personaje
    [SerializeField] private int expBase; //cantidad de experiencia para llegar al siguiente nivel
    [SerializeField] private int valorIncremental; //valor por el que multiplicamos la experiencia al llegar a un nuevo nivel

    private float expActual;
    //variables para controlar la experiencia
    private float expActualTemp;
    private float expRequeridaSiguienteNivel;

    public PersonajeStats PersonajeStats
    {
        get => default;
        set
        {
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stats.Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        stats.ExpReq = expRequeridaSiguienteNivel;
        actualizarBarraExperiencia();
        stats.resetearValores();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            anhadirExperiencia(2f);
        }
    }

    //método para aumentar la experiencia de nuestro personaje
    public void anhadirExperiencia(float experienciaObtenida)
    {
        if(experienciaObtenida > 0) //la experiencia siempre debe ser mayor a 0
        {
            float expRestanteNuevoNivel = expRequeridaSiguienteNivel - expActualTemp;
            if(experienciaObtenida >= expRestanteNuevoNivel) //si la experiencia obtenida es mayor o igual a la que se necesita para subir de nivel
            {
                experienciaObtenida -= expRestanteNuevoNivel;
                expActual += experienciaObtenida;
                actualizarNivel();
                anhadirExperiencia(experienciaObtenida);
            }
            else
            {
                expActual += experienciaObtenida;
                expActualTemp += experienciaObtenida;
                if(expActualTemp == expRequeridaSiguienteNivel)
                {
                    actualizarNivel();
                }
            }
        }
        stats.ExpActual = expActual;
        actualizarBarraExperiencia();
    }
     //método para subir de nivel, resetear la experiencia a 0 y aumentar la cantidad para alcanzar el nuevo siguiente nivel
    public void actualizarNivel()
    {
        if (stats.Nivel < nivelMax)
        {
            stats.Nivel++; //sube nivel
            expActualTemp = 0f; //actualiza la experiencia a conseguir para siguiente nivel a 0
            expRequeridaSiguienteNivel *= valorIncremental; //aumento la cantidad de experiencia a conseguir para nuevo nivel
            stats.ExpReq = expRequeridaSiguienteNivel;
            stats.puntosDisponibles += 3;

        }
    }

    private void actualizarBarraExperiencia()
    {
        UIManager.Instance.actualizarExpPersonaje(expActualTemp, expRequeridaSiguienteNivel);
    }

    private void RespuestaEnemigoDerrotado(float exp)
    {
        anhadirExperiencia(exp);
    }

    private void OnEnable()
    {
        EnemigoVida.EventoEnemigoDerrotado += RespuestaEnemigoDerrotado;
    }

 

    private void OnDisable()
    {
        EnemigoVida.EventoEnemigoDerrotado -= RespuestaEnemigoDerrotado;
    }
}
