using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventarioUI : Singleton<InventarioUI>
{
    [Header("Panel Inventario Descripcion")]
    [SerializeField] private GameObject panelInventarioDescripcion;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion;

    [SerializeField] private InventarioSlots slotPrefab;
    [SerializeField] private Transform contenedor;

    public int IndiceSlotInicial { get; private set; }

    public InventarioSlots SlotSeleccionado { get; private set; }

    List<InventarioSlots> slotsDisponibles = new List<InventarioSlots>();

    // Start is called before the first frame update
    void Start()
    {
        IniciarInventario();
        IndiceSlotInicial = -1; //valor de indice predeterminado para poder mover objetos por el inventario
    }

    private void Update()
    {
        ActualizarSlotSeleccionado();
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(SlotSeleccionado != null)
            {
                IndiceSlotInicial = SlotSeleccionado.Index;
            }
        }
    }

    private void IniciarInventario() //Método donde añado 24 slots al inventario por medio de un prefab
    {
        for(int i =0; i< Inventario.Instance.CantidadSlots; i++)
        {
            InventarioSlots nuevoSlot = Instantiate(slotPrefab, contenedor);
            nuevoSlot.Index = i; //adjudicamos un indice a cada slot (.index es una prop de la clase InventarioSlots)
            slotsDisponibles.Add(nuevoSlot);
        }
    }

    private void ActualizarSlotSeleccionado()
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;
        if(goSeleccionado == null)
        {
            return;
        }

        InventarioSlots slot = goSeleccionado.GetComponent<InventarioSlots>();
        if(slot != null)
        {
            SlotSeleccionado = slot;
        }
    }

    public void DibujarItemInventario(InventarioItem itemPorAnhadir, int cantidad, int indiceItem)
    {
        InventarioSlots slot = slotsDisponibles[indiceItem];
        if(itemPorAnhadir != null)
        {
            slot.ActivarSlotUI(true);
            slot.ActualizarSlot(itemPorAnhadir, cantidad);
        }
        else
        {
            slot.ActivarSlotUI(false);
        }
    }

    private void ActualizarInventarioDescripcion(int indice)
    {
        if (Inventario.Instance.ItemsInventario[indice] != null) //si existe un item en este indice del slot
        {
            itemIcono.sprite = Inventario.Instance.ItemsInventario[indice].Icono;
            itemNombre.text = Inventario.Instance.ItemsInventario[indice].Nombre;
            itemDescripcion.text = Inventario.Instance.ItemsInventario[indice].Descripcion;
            panelInventarioDescripcion.SetActive(true);
        }
        else
        {
            panelInventarioDescripcion.SetActive(false);

        }
    }

    public void UsarItem()
    {
        if(SlotSeleccionado != null)
        {
            SlotSeleccionado.UsarItemSlot();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    public void EquiparItem()
    {
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotEquiparItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    public void BorrarItem()
    {
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotBorrarItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    #region Evento
    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int indice)
    {
        if (tipo == TipoDeInteraccion.Click)
        {
            ActualizarInventarioDescripcion(indice);
        }
    }

    private void OnEnable()
    {
        InventarioSlots.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlots.EventoSlotInteraccion -= SlotInteraccionRespuesta;

    }
    #endregion

 


  
}
