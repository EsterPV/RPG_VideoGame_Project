using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeAnimaciones : MonoBehaviour
{
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerCaminar;
    [SerializeField] private string layerAtacar;


    private Animator _animator;
    private PersonajeMovimiento _personajeMovimiento; //Creo un objeto de la clase PersonajeMovimiento
    private PersonajeAtaque _personajeAtaque;

    private readonly int direccionX = Animator.StringToHash("x"); //variable de solo lectura que creo para la x
    private readonly int direccionY = Animator.StringToHash("y");  //variable de solo lectura que creo para la y
    private readonly int derrotado = Animator.StringToHash("Derrotado");  

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _personajeMovimiento = GetComponent<PersonajeMovimiento>();
        _personajeAtaque = GetComponent<PersonajeAtaque>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        actualizarLayer();

        if(_personajeMovimiento.enMovimiento== false) //si el personaje no se mueve
        {
            return; //No sigo leyendo
        }

        _animator.SetFloat(direccionX, _personajeMovimiento.DireccionMovimiento.x); //Muestro la animacion correspondiente segun la posicion de x
        _animator.SetFloat(direccionY, _personajeMovimiento.DireccionMovimiento.y); //Muestro la animacion correspondiente segun la posicion de y

    }

    private void ActivarLayer(string nombreLayer)
    {
        for(int i=0; i< _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0); //desactivamos todos los layers
        }
        _animator.SetLayerWeight(_animator.GetLayerIndex(nombreLayer), 1); //activamos el layer que nos interesa
    }

    private void actualizarLayer()
    {
        if (_personajeAtaque.Atacando)
        {
            ActivarLayer(layerAtacar);
        }
        else if (_personajeMovimiento.enMovimiento) //si el personaje se mueve
        {
            ActivarLayer(layerCaminar); //activo el layer de caminar
        }
        else // si no se mueve
        {
            ActivarLayer(layerIdle); //activo el idle
        }
    }

    public void revivirPersonaje()
    {
        ActivarLayer(layerIdle);
        _animator.SetBool(derrotado, false);
    }

    private void personajeDerrotadoRespuesta()
    {
        if (_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle))==1)
        {
            _animator.SetBool(derrotado, true);
        }
    }

    private void OnEnable()
    {
        PersonajeVida.eventoPersonajeDerrotado += personajeDerrotadoRespuesta;
    }

    private void OnDisable()
    {
        PersonajeVida.eventoPersonajeDerrotado -= personajeDerrotadoRespuesta;

    }
}
