using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{

    [Header("Items")]
    [SerializeField] private InventarioItem[] itemsInventario;
    [SerializeField] private Personaje personaje;
    [SerializeField] private int cantidadSlots;

    public Personaje Personaje => personaje;
    public int CantidadSlots => cantidadSlots;
    public InventarioItem[] ItemsInventario => itemsInventario;

    //public InventarioItem InventarioItem
    //{
    //    get => default;
    //    set
    //    {
    //    }
    //}

    private void Start()
    {
        itemsInventario = new InventarioItem[cantidadSlots];
    }

    public void AnhadirItem(InventarioItem itemPorAnhadir, int cantidad)
    {
        if(itemPorAnhadir == null)
        {
            return;
        }

        List<int> indices = VerificarExistencias(itemPorAnhadir.ID); //Comprueba si ya existe el item en el inventario

        if (itemPorAnhadir.esAcumulable)
        {
            if(indices.Count > 0)
            {
                for( int i = 0; i < indices.Count; i++)
                {
                    if(itemsInventario[indices[i]].Cantidad < itemPorAnhadir.AcumulacionMax) //si el item seleccionado no supera el maximo de items acumulados
                    {
                        itemsInventario[indices[i]].Cantidad += cantidad;
                        if(itemsInventario[indices[i]].Cantidad > itemPorAnhadir.AcumulacionMax) //si el item seleccionado si supera el maximo de items acumulados
                        {
                            int diferencia = itemsInventario[indices[i]].Cantidad - itemPorAnhadir.AcumulacionMax; //obtenemos la diferencia de lo que sobra
                            itemsInventario[indices[i]].Cantidad = itemPorAnhadir.AcumulacionMax; //establecemos la cantidad a su acumulacion maxima, y que no salga de ahi

                            AnhadirItem(itemPorAnhadir, diferencia); //llamamos el mismo metodo, añadiendo el mismo item con la diferencia 
                        }

                        InventarioUI.Instance.DibujarItemInventario(itemPorAnhadir, itemsInventario[indices[i]].Cantidad, indices[i]); //actualizamos el inventario
                        return;
                    }
                }
            }
        }

      if(cantidad <= 0)
        {
            return;
        }

      if(cantidad > itemPorAnhadir.AcumulacionMax) //Si hemos recogido un item que tiene una cantidad que se pasa de lo que podemos acumular en un slot
        {
            AnhadirItemSlotSDisponible(itemPorAnhadir, itemPorAnhadir.AcumulacionMax); //colocamos el maximo en un slot libre 
            cantidad -= itemPorAnhadir.AcumulacionMax; //actualizamos la cantidad que nos sobra
            AnhadirItem(itemPorAnhadir, cantidad); //y volvemos a añadir la cantidad que nos sobra en otro slot libre
        }
        else //si no pasa del maximo que podemos guardar en un slot
        {
            AnhadirItemSlotSDisponible(itemPorAnhadir, cantidad); //simplemente lo guardamos
        }

    }

    private List<int> VerificarExistencias(string itemID)
    {
        List<int> indexDeItem = new List<int>();
        for(int i=0; i < itemsInventario.Length; i++)
        {
            if(itemsInventario[i] != null)
            {
                if (itemsInventario[i].ID == itemID) //SI ENCONTRAMOS UN ITEM CON EL MISMO ID EN LA LISTA 
                {
                    indexDeItem.Add(i); //lo añadimos a la lista
                }
            }
        }
        return indexDeItem;
    }

    public int ObtenerCantidadItems(string itemID)
    {
        List<int> indices = VerificarExistencias(itemID);

        int cantidadTotal = 0;
        foreach(int indice in indices)
        {
            if(itemsInventario[indice].ID == itemID)
            {
                cantidadTotal += itemsInventario[indice].Cantidad;
            }
        }

        return cantidadTotal;
    }

    public void ConsumirItem(string itemID)
    {
        List<int> indices = VerificarExistencias(itemID);
        if(indices.Count > 0)
        {
            EliminarItem(indices[indices.Count - 1]);
        }
    }

    private void AnhadirItemSlotSDisponible( InventarioItem item, int cantidad) //AÑADE ITEM EN UN SLOT VACÍO
    {
        for(int i = 0; i <itemsInventario.Length; i++) //recorro todo el inventario
        {
            if(itemsInventario[i]== null) //si encontramos un slot vacio
            {
                itemsInventario[i] = item.copiarItem();  //anhadimos el item creando una nueva instancia del objeto
                itemsInventario[i].Cantidad = cantidad; //y actualizamos su cantidad
                InventarioUI.Instance.DibujarItemInventario(item, cantidad, i);
                return; //y salimos
            }
        }
    }

    private void EliminarItem(int indice)
    {
        itemsInventario[indice].Cantidad--; //elimina un item de la cantidad
        if(itemsInventario[indice].Cantidad <= 0) //si no hay más cantidad de items en el slot seleccionado
        {
            itemsInventario[indice].Cantidad = 0; //declaramos 0 de cantidad de dicho item
            itemsInventario[indice] = null; //eliminamos item
            InventarioUI.Instance.DibujarItemInventario(null, 0, indice); //actualizamos el slot a vacío
        }
        else //si hay items en el slot
        {
            InventarioUI.Instance.DibujarItemInventario(itemsInventario[indice], itemsInventario[indice].Cantidad, indice); //actualizamos el slot con el item y la cantidad actual tras usarlo
        }
    }


    public void MoverItem(int indiceInicial, int indiceFinal)
    {
        if(itemsInventario[indiceInicial] == null || itemsInventario[indiceFinal] != null) //si no hay item por moiver en el slot seleccionado salimos o si hay ya un item en el slot donde queremos poner el nuevo item tb salimos
        {
            return;
        }

        //copiamos el item en el slot final
        InventarioItem itemPorMover = itemsInventario[indiceInicial].copiarItem();
        itemsInventario[indiceFinal] = itemPorMover;
        InventarioUI.Instance.DibujarItemInventario(itemPorMover, itemPorMover.Cantidad, indiceFinal);

        //borramos el item del slot inicial
        itemsInventario[indiceInicial] = null;
        InventarioUI.Instance.DibujarItemInventario(null, 0, indiceInicial);
    }

    private void usarItem(int indice)
    {
        if(itemsInventario[indice] == null) //si no existe ningun item salimos del método
        {
            return;
        }

        if (itemsInventario[indice].UsarItem()) //si se pudo usar un item
        {
            EliminarItem(indice); //eliminamos uno ya que significa que lo hemos usado
        }
    }

    private void EquiparItem(int indice)
    {
        if (itemsInventario[indice] == null) //si no existe ningun item salimos del método
        {
            return;
        }

        if (itemsInventario[indice].Tipo != TiposItem.Armas)
        {
            return;
        }

        ItemsInventario[indice].EquiparItem();

    }

    private void BorrarItem(int indice)
    {

        if (itemsInventario[indice] == null) //si no existe ningun item salimos del método
        {
            return;
        }


        if (itemsInventario[indice].Tipo != TiposItem.Armas)
        {
            return;
        }

        itemsInventario[indice].BorrarItem();
    }

    #region escuchando eventos

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int indice)
    {
        switch (tipo)
        {
            case TipoDeInteraccion.Usar:
                usarItem(indice);
                break;
            case TipoDeInteraccion.Equipar:
                EquiparItem(indice);
                break;
            case TipoDeInteraccion.Borrar:
                BorrarItem(indice);
                break;
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
