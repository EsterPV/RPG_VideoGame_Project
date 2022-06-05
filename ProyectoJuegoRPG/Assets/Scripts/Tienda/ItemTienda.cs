using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTienda : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemPrecio;
    [SerializeField] private TextMeshProUGUI cantidadPorComprar;

    public ItemVenta ItemCargado { get; private set; }

    private int cantidad;
    private int precioInicial;
    private int precioActual;

    private void Update()
    {
        cantidadPorComprar.text = cantidad.ToString();
        itemPrecio.text = precioActual.ToString();
    }

    public void ConfigurarItemVenta(ItemVenta itemVenta)
    {
        ItemCargado = itemVenta;
        itemIcono.sprite = itemVenta.item.Icono;
        itemNombre.text = itemVenta.item.Nombre;
        itemPrecio.text = itemVenta.precio.ToString();
        cantidad = 1;
        precioInicial = itemVenta.precio;
        precioActual = itemVenta.precio;
    }

    public void ComprarItem()
    {
        if(MonedasManager.Instance.MonedasTotales >= precioActual)
        {
            Inventario.Instance.AnhadirItem(ItemCargado.item, cantidad);
            MonedasManager.Instance.BorrarMonedas(precioActual);
            cantidad = 1;
            precioActual = precioInicial;
        }
    }

    public void SumarItemPorComprar()
    {
        int precioDeCompra = precioInicial * (cantidad + 1);
        if(MonedasManager.Instance.MonedasTotales >= precioDeCompra)
        {
            cantidad++;
            precioActual = precioInicial * cantidad;
        }
    }

    public void RestarItemPorComprar()
    {

        if(cantidad == 1) { return; }

        cantidad--;
        precioActual = precioInicial * cantidad;
    }

}
