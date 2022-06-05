using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TipoDeInteraccion
{
    Click,
    Usar,
    Equipar,
    Borrar
}

public class InventarioSlots : MonoBehaviour
{
    public static Action<TipoDeInteraccion, int> EventoSlotInteraccion;

    [SerializeField] private Image itemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TextMeshProUGUI cantidadTMP;

    public int Index { get; set; }

    private Button boton;

    private void Awake()
    {
        boton = GetComponent<Button>();
    }

    public void ActualizarSlot(InventarioItem item, int cantidad)
    {
        itemIcono.sprite = item.Icono;
        cantidadTMP.text = cantidad.ToString();
    }

    public void ActivarSlotUI(bool estado)
    {
        itemIcono.gameObject.SetActive(estado);
        fondoCantidad.SetActive(estado);
    }

    public void SeleccionarSlot()
    {
        boton.Select();
    }

    public void ClickSlot()
    {
        EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Click, Index);

        //Mover item
        if(InventarioUI.Instance.IndiceSlotInicial != -1) //si el indice es distinto del valor predeterminado del objeto que queremos mover
        {
            if(InventarioUI.Instance.IndiceSlotInicial != Index)
            {
                //Mover
                Inventario.Instance.MoverItem(InventarioUI.Instance.IndiceSlotInicial, Index);
            }
        }
    }

    public void UsarItemSlot() 
    {
        if(Inventario.Instance.ItemsInventario[Index] != null)//verifica si hay un item en el slot seleccionado, y si es asi 
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Usar, Index); //lanza el evento de interaccion de que queremos usar ese item con su respectivo item
        }
    }

    public void SlotEquiparItem()
    {

        if (Inventario.Instance.ItemsInventario[Index] != null)//verifica si hay un item en el slot seleccionado, y si es asi 
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Equipar, Index); //lanza el evento de interaccion de que queremos equipar ese item con su respectivo item
        }
    }

    public void SlotBorrarItem()
    {

        if (Inventario.Instance.ItemsInventario[Index] != null)//verifica si hay un item en el slot seleccionado, y si es asi 
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Borrar, Index); //lanza el evento de interaccion de que queremos equipar ese item con su respectivo item
        }
    }

}
