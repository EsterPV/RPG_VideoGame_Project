using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionManager : MonoBehaviour
{
    public static Action<EnemigoInteraccion> EventoEnemigoSeleccionado;
    public static Action EventoObjetoNoSeleccionado;

    public EnemigoInteraccion EnemigoSeleccionado { get; set; }

    private Camera camara;

    // Start is called before the first frame update
    void Start()
    {
        camara = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        SeleccionarEnemigo();
    }

    private void SeleccionarEnemigo()
    {
        if (Input.GetMouseButtonDown(0)) //Si se aprieta el boton izquierdo del mouse
        {
            RaycastHit2D hit = Physics2D.Raycast(camara.ScreenToWorldPoint(Input.mousePosition),
               Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemigo"));

            if (hit.collider != null)
            {
                EnemigoSeleccionado = hit.collider.GetComponent<EnemigoInteraccion>();
                EnemigoVida enemigoVida = EnemigoSeleccionado.GetComponent<EnemigoVida>();
                if(enemigoVida.Salud > 0f)
                {
                    EventoEnemigoSeleccionado?.Invoke(EnemigoSeleccionado);
                }
                else
                {
                    EnemigoLoot loot = EnemigoSeleccionado.GetComponent<EnemigoLoot>();
                    LootManager.Instance.MostrarLoot(loot);
                }
                
            }
            else
            {
                EventoObjetoNoSeleccionado?.Invoke();
            }
        }
    }
}
