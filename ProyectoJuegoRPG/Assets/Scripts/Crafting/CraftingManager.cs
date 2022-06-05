using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecetaTarjeta recetaTarjetaPrefab;
    [SerializeField] private Transform recetaContenedor;

    [Header("Receta Info")]
    [SerializeField] private Image primerMaterialIcono;
    [SerializeField] private Image segundoMaterialIcono;
    [SerializeField] private TextMeshProUGUI primerMaterialNombre;
    [SerializeField] private TextMeshProUGUI segundoMaterialNombre;
    [SerializeField] private TextMeshProUGUI segundoMaterialCantidad;
    [SerializeField] private TextMeshProUGUI primerMaterialCantidad;
    [SerializeField] private TextMeshProUGUI recetaMensaje;
    [SerializeField] private Button btnCraftear;

    [Header("Receta Item Resultado")]
    [SerializeField] private Image itemResultadoIcono;
    [SerializeField] private TextMeshProUGUI itemResultadoNombre;
    [SerializeField] private TextMeshProUGUI itemResultadoDescripcion;

    [Header("Recetas")]
    [SerializeField] private RecetaLista recetas;

    public Receta RecetaSeleccionada { get; set; }

    private void Start()
    {
        CargarRecetas();
    }

    private void CargarRecetas()
    {
        for(int i=0; i<recetas.recetas.Length; i++)
        {
            RecetaTarjeta recetaTarjeta = Instantiate(recetaTarjetaPrefab, recetaContenedor);
            recetaTarjeta.ConfigurarRecetaTarjeta(recetas.recetas[i]);
        }
    }

    public void MostrarReceta(Receta receta)
    {
        RecetaSeleccionada = receta;
        primerMaterialIcono.sprite = receta.item1.Icono;
        segundoMaterialIcono.sprite = receta.item2.Icono;
        primerMaterialNombre.text = receta.item1.Nombre;
        segundoMaterialNombre.text = receta.item2.Nombre;
        primerMaterialCantidad.text = $"{Inventario.Instance.ObtenerCantidadItems(receta.item1.ID)}/{receta.item1CantidadRequerida}";
        segundoMaterialCantidad.text = $"{Inventario.Instance.ObtenerCantidadItems(receta.item2.ID)}/{receta.item2CantidadRequerida}";

        if (SePuedeCraftear(receta))
        {
            recetaMensaje.text = "Receta Disponible";
            btnCraftear.interactable = true;
        }
        else
        {
            recetaMensaje.text = "No cuentas con suficientes materiales";
            btnCraftear.interactable = false;
        }

        itemResultadoIcono.sprite = receta.itemResultado.Icono;
        itemResultadoNombre.text = receta.itemResultado.Nombre;
        itemResultadoDescripcion.text = receta.itemResultado.DescripcionItemCrafting();
    }

    public bool SePuedeCraftear(Receta receta)
    {
        if(Inventario.Instance.ObtenerCantidadItems(receta.item1.ID) >= receta.item1CantidadRequerida &&
            Inventario.Instance.ObtenerCantidadItems(receta.item2.ID) >= receta.item2CantidadRequerida)
        {
            return true;
        }

        return false;
    }

    public void Craftear()
    {
        for ( int i = 0; i < RecetaSeleccionada.item1CantidadRequerida; i++)
        {
            Inventario.Instance.ConsumirItem(RecetaSeleccionada.item1.ID);
        }

        for (int i = 0; i < RecetaSeleccionada.item2CantidadRequerida; i++)
        {
            Inventario.Instance.ConsumirItem(RecetaSeleccionada.item2.ID);
        }

        Inventario.Instance.AnhadirItem(RecetaSeleccionada.itemResultado, RecetaSeleccionada.itemResultadoCantidad);
        MostrarReceta(RecetaSeleccionada);
    }

}
