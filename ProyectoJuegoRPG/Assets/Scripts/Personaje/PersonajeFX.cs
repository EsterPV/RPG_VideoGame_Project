using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoPersonaje
{
    Player,
    IA
}

public class PersonajeFX : MonoBehaviour
{
    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("Config")]
    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    [SerializeField] private Transform canvasTextoPosicion;

    [Header("Tipo")]
    [SerializeField] private TipoPersonaje tipoPersonaje;

    private EnemigoVida _enemigoVida;

    public TipoPersonaje TipoPersonaje
    {
        get => default;
        set
        {
        }
    }

    private void Awake()
    {
        _enemigoVida = GetComponent<EnemigoVida>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab);
    }

    private IEnumerator IEMostrarTexto(float cantidad, Color color)
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia();
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>();
        texto.EstablecerTexto(cantidad, color);
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position;
        nuevoTextoGO.SetActive(true);

        yield return new WaitForSeconds(1f);
        nuevoTextoGO.SetActive(false);
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform);
    }

    private void RespuestaDanhoRecibido(float danho)
    {
        if(tipoPersonaje == TipoPersonaje.Player)
        {
            StartCoroutine(IEMostrarTexto(danho, Color.black));
        }

    }

    private void RespuestaDanhoAEnemigo(float danho, EnemigoVida enemigoVida)
    {
        if(tipoPersonaje == TipoPersonaje.IA && _enemigoVida == enemigoVida)
        {
            StartCoroutine(IEMostrarTexto(danho, Color.red));
        }
    }

    private void OnEnable()
    {
        IAController.EventoDanhoRealizado += RespuestaDanhoRecibido;
        PersonajeAtaque.EventoEnemigoDanhado += RespuestaDanhoAEnemigo;
    }

    private void OnDisable()
    {
        IAController.EventoDanhoRealizado -= RespuestaDanhoRecibido;
        PersonajeAtaque.EventoEnemigoDanhado -= RespuestaDanhoAEnemigo;


    }

   
}
