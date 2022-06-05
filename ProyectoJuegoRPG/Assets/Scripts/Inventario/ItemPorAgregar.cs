using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPorAgregar : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private InventarioItem inventarioItemReferencia;
    [SerializeField]private int cantidadPorAgregar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //si el objeto choca con el jugador
        {
            Inventario.Instance.AnhadirItem(inventarioItemReferencia, cantidadPorAgregar); //recogemos el objetro del suelo y lo guardamos en el inventario
            Destroy(gameObject); //destruimos el objeto
        }
    }
}
