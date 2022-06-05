using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemigoVida : VidaBase
{
    public static Action<float> EventoEnemigoDerrotado;

    [Header("Vida")]
    [SerializeField] private EnemigoBarraVida barraVidaPrefab;
    [SerializeField] private Transform barraVidaPos;

    [Header("Rastros")]
    [SerializeField] private GameObject rastros;

    private EnemigoBarraVida enemigoBarraVidaCreada;
    private EnemigoInteraccion _enemigoInteraccion;
    private EnemigoMovimiento _enemigoMovimiento;
    private EnemigoLoot _enemigoLoot;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private IAController _controller;

    public EnemigoBarraVida EnemigoBarraVida
    {
        get => default;
        set
        {
        }
    }

    private void Awake()
    {
        _enemigoInteraccion = GetComponent<EnemigoInteraccion>();
        _enemigoMovimiento = GetComponent<EnemigoMovimiento>();
        _enemigoLoot = GetComponent<EnemigoLoot>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _controller = GetComponent<IAController>();
    }

    protected override void Start()
    {
        base.Start();
        CrearBarraVida();
    }


    private void CrearBarraVida()
    {
        enemigoBarraVidaCreada = Instantiate(barraVidaPrefab, barraVidaPos);
        ActualizarBarraVida(Salud, saludMax);
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        enemigoBarraVidaCreada.ModificarSalud(vidaActual, vidaMax);
    }

    protected override void personajeDerrotado()
    {
        DesactivarEnemigo();
        EventoEnemigoDerrotado?.Invoke(_enemigoLoot.ExpGanada);
        QuestManager.Instance.AnhadirProgreso("Mata10", 1);
        QuestManager.Instance.AnhadirProgreso("Mata20", 1);
        QuestManager.Instance.AnhadirProgreso("Mata40", 1);
    }

    private void DesactivarEnemigo()
    {
        rastros.SetActive(true);
        enemigoBarraVidaCreada.gameObject.SetActive(false);
        _spriteRenderer.enabled = false;
        _enemigoMovimiento.enabled = false;
        _controller.enabled = false;
        _boxCollider2D.isTrigger = true;
        _enemigoInteraccion.DesactivarSpritesSeleccion();
        
    }

}
