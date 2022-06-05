using System;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine;

public class PersonajeAtaque : MonoBehaviour
{
    public static Action<float, EnemigoVida> EventoEnemigoDanhado;

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("Ataque")]
    [SerializeField] private float tiempoEntreAtaque;
    [SerializeField] private Transform[] posicionesDisparo;

    public Arma ArmaEquipada { get; private set; }
    public EnemigoInteraccion EnemigoObjetivo { get; private set; }
    public bool Atacando { get; set; }

    private PersonajeMana _personajeMana;
    private int indexDireccionDisparo;
    private float tiempoParaSigAtaque;

    private void Awake()
    {
        _personajeMana = GetComponent<PersonajeMana>();
    }

    private void Update()
    {
        ObtenerDireccionDisparo();

        if(Time.time > tiempoParaSigAtaque)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(ArmaEquipada == null || EnemigoObjetivo == null) { return; }

                UsarArma();
                tiempoParaSigAtaque = Time.time + tiempoEntreAtaque;
                StartCoroutine(IEEstablecerCondicionAtaque());
            }

            
           
        }
    }

    private void UsarArma()
    {
        if (ArmaEquipada.tipo == TipoArma.Magia)
        {
            if (_personajeMana.manaActual < ArmaEquipada.manaRequerida)
            {
                return;
            }

            GameObject nuevoProyectil = pooler.ObtenerInstancia();
            nuevoProyectil.transform.localPosition = posicionesDisparo[indexDireccionDisparo].position;

            Proyectil proyectil = nuevoProyectil.GetComponent<Proyectil>();
            proyectil.IniciarProyectil(this);

            nuevoProyectil.SetActive(true);
            _personajeMana.usarMana(ArmaEquipada.manaRequerida);
        }
        else
        {
            float danho = ObtenerDanho();
            EnemigoVida enemigoVida = EnemigoObjetivo.GetComponent<EnemigoVida>();
            enemigoVida.recibirDaño(danho);
            EventoEnemigoDanhado?.Invoke(danho, enemigoVida);
        }
    }

    public float ObtenerDanho()
    {
        float cantidad = stats.Danho;
        if(Random.value < stats.PorcentajeCritico / 100)
        {
            cantidad *= 2;
        }

        return cantidad;
    }

    private IEnumerator IEEstablecerCondicionAtaque()
    {
        Atacando = true;
        yield return new WaitForSeconds(0.3f);
        Atacando = false;
    }

    public void EquiparArma(ItemArma armaPorEquipar)
    {
        ArmaEquipada = armaPorEquipar.arma;
        if(ArmaEquipada.tipo == TipoArma.Magia)
        {
            pooler.CrearPooler(ArmaEquipada.proyectilPrefab.gameObject);
        }

        stats.AnhadirBonusPorArma(ArmaEquipada);
    }

    public void BorrarArma()
    {
        if(ArmaEquipada == null)
        {
            return;
        }

        if (ArmaEquipada.tipo == TipoArma.Magia)
        {
            pooler.DestruirPooler();
        }
        stats.BorrarBonusPorArma(ArmaEquipada);
        ArmaEquipada = null;
    }

    private void ObtenerDireccionDisparo()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.x > 0.1f)
        {
            indexDireccionDisparo = 1;
        }
        else if (input.x < 0f)
        {
            indexDireccionDisparo = 3;
        }
        else if (input.y > 0.1f)
        {
            indexDireccionDisparo = 0;
        }
        else if (input.y < 0f)
        {
            indexDireccionDisparo = 2;
        }
    }

    private void EnemigoRangoSeleccionado(EnemigoInteraccion enemigoSeleccionado)
    {
        if(ArmaEquipada == null) {return;}

        if(ArmaEquipada.tipo != TipoArma.Magia) {return;}

        if(EnemigoObjetivo == enemigoSeleccionado) {return;}

        EnemigoObjetivo = enemigoSeleccionado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeDeteccion.Rango);
    }

    private void EnemigoNoSeleccionado()
    {
        if(EnemigoObjetivo == null) {return;}

        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeDeteccion.Rango);
        EnemigoObjetivo = null;
    }

    private void EnemigoMeleeDetectado(EnemigoInteraccion enemigoDetectado)
    {
        if (ArmaEquipada == null) { return; }
        if (ArmaEquipada.tipo != TipoArma.Melee) { return; }

        EnemigoObjetivo = enemigoDetectado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeDeteccion.Melee);
    }


    private void EnemigoMeleePerdido()
    {
        if (ArmaEquipada == null) { return; }
        if (EnemigoObjetivo == null) { return; }
        if (ArmaEquipada.tipo != TipoArma.Melee) { return; }

        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeDeteccion.Melee);
        EnemigoObjetivo = null;
    }

    private void OnEnable()
    {
        SeleccionManager.EventoEnemigoSeleccionado += EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado += EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado += EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido += EnemigoMeleePerdido;
    }

    private void OnDisable()
    {
        SeleccionManager.EventoEnemigoSeleccionado -= EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado -= EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido -= EnemigoMeleePerdido;

    }

}
