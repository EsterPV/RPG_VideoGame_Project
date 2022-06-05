using Random = UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TiposDeAtaque
{
    Melee,
    Embestida
}

public class IAController : MonoBehaviour
{
    public static Action<float> EventoDanhoRealizado;

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Estados")]
    [SerializeField] private IAEstado estadoInicial;
    [SerializeField] private IAEstado estadoDefault;

    [Header("Configuracion")]
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float rangoAtaque;
    [SerializeField] private float rangoEmbestida;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadEmbestida;
    [SerializeField] private LayerMask personajeLayerMask;

    [Header("Ataque")]
    [SerializeField] private float danho;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] TiposDeAtaque tipoAtaque;

    [Header("Debug")]
    [SerializeField]private bool mostrarDeteccion;
    [SerializeField]private bool mostrarRangoAtaque;
    [SerializeField]private bool mostrarRangoEmbestida;

    private float tiempoParaSigAtaque;
    private BoxCollider2D boxCollider2;

    public Transform PersonajeReferencia { get; set; }
    public IAEstado EstadoActual { get; set; }
    public EnemigoMovimiento EnemigoMovimientoProp { get; set; }
    public float RangoDeteccion => rangoDeteccion;
    public float VelocidadMovimiento => velocidadMovimiento;
    public float Danho => danho;
    public TiposDeAtaque TipoAtaque => tipoAtaque;
    public LayerMask PersonajeLayerMask => personajeLayerMask;
    public float RangoAtaqueDeterminado => tipoAtaque == TiposDeAtaque.Embestida ? rangoEmbestida : rangoAtaque;

    public TiposDeAtaque TiposDeAtaque
    {
        get => default;
        set
        {
        }
    }

    //private void rangoAtaqueEmbestida() //este método es lo mismo que lo definido en la prop de RangoAtaqueDeterminado(linea 49)
    //{
    //    if(tipoAtaque == TiposDeAtaque.Embestida)
    //    {
    //        RangoAtaqueDeterminado = rangoEmbestida;
    //    }
    //    else
    //    {
    //        RangoAtaqueDeterminado = rangoAtaque;
    //    }
    //}

    private void Start()
    {
        boxCollider2 = GetComponent<BoxCollider2D>();
        EstadoActual = estadoInicial;
        EnemigoMovimientoProp = GetComponent<EnemigoMovimiento>();
    }

    private void Update()
    {
        EstadoActual.EjecutarEstado(this);
    }

    public void CambiarEstado(IAEstado nuevoEstado)
    {
        if(nuevoEstado != estadoDefault)
        {
            EstadoActual = nuevoEstado;
        }
    }

    public void AtaqueMelee(float cantidadDanho)
    {
        if(PersonajeReferencia != null)
        {
            AplicarDanhoAlPersonaje(cantidadDanho);
        }
    }

    public void AtaqueEmbestida(float cantidadDanho)
    {
        StartCoroutine(IEEmbestida(cantidadDanho));
    }

    private IEnumerator IEEmbestida(float cantidad) //coroutine
    {
        Vector3 personajePosicion = PersonajeReferencia.position;
        Vector3 posInicial = transform.position; //posicion actual enemigo
        Vector3 direccionHaciaPersonaje = (personajePosicion - posInicial).normalized;
        Vector3 posAtaque = personajePosicion - direccionHaciaPersonaje * 0.5f;
        boxCollider2.enabled = false;

        float transicionAtaque = 0f;
        while(transicionAtaque <= 1f)
        {
            transicionAtaque += Time.deltaTime * velocidadMovimiento;
            float interpolacion = (-Mathf.Pow(transicionAtaque, 2) + transicionAtaque) * 4f; //formula que hace que el enemigo hace que vaya desde su posicion inicial a la posicion del personaje y luego vuelva a la inicial
            transform.position = Vector3.Lerp(posInicial, posAtaque, interpolacion);
            yield return null;
        }

        if(PersonajeReferencia != null)
        {
            AplicarDanhoAlPersonaje(cantidad);
        }

        boxCollider2.enabled = true;
    }

    public void AplicarDanhoAlPersonaje(float cantidadDanho)
    {
        float danhoPorRealizar = 0;
        if(Random.value < stats.PorcentajeBloqueo / 100)
        {
            return;
        }

        danhoPorRealizar = Mathf.Max(cantidadDanho - stats.Defensa, 1f); //el enemigo hace como min 1 de daño
        PersonajeReferencia.GetComponent<PersonajeVida>().recibirDaño(danhoPorRealizar);
        EventoDanhoRealizado?.Invoke(danhoPorRealizar);
    }

    public bool PersonajeEnRangoDeAtaque(float rango)
    {
        float distanciaHaciaPersonaje = (PersonajeReferencia.position - transform.position).sqrMagnitude;
        if(distanciaHaciaPersonaje < Mathf.Pow(rango, 2))
        {
            return true;
        }
        return false;
    }

    public bool EsTiempoDeAtacar()
    {
        if(Time.time > tiempoParaSigAtaque)
        {
            return true;
        }

        return false;
    }

    public void ActualizarTiempoEntreAtaques()
    {
        tiempoParaSigAtaque = Time.time + tiempoEntreAtaques;
    }

    private void OnDrawGizmos()
    {
        if (mostrarDeteccion)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position,rangoDeteccion);
        }

        if (mostrarRangoAtaque)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, rangoAtaque);
        }

        if (mostrarRangoEmbestida)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangoEmbestida);
        }
    }

}
