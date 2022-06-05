using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contiene todos los tipos de item
public enum TiposItem
{
    Armas,
    Pociones,
    Pergaminos,
    Ingredientes,
    Tesoros
}

public class InventarioItem : ScriptableObject
{
    [Header("Parametros")]
    public string ID;
    public string Nombre;
    public Sprite Icono;
    [TextArea]public string Descripcion;

    [Header("Informacion")]
    public TiposItem Tipo;
    public bool esConsumible;
    public bool esAcumulable; //si se pueden guardar mas de un item en un solo slot o no
    public int AcumulacionMax;  //maximo de cantidad que se puede guardar de un solo item en un slot

  [HideInInspector] public int Cantidad; //controla cuanta cantidad queda del item

    public Inventario Inventario
    {
        get => default;
        set
        {
        }
    }

    public InventarioItem copiarItem() //metodo que crea una nueva instancia del item seleccionado para que cuando guardemos la cantidad sobrante en un slot diferente sean dos objetos distintos con dos cantidades distintas
    {
        InventarioItem nuevaInstancia = Instantiate(this);
        return nuevaInstancia;
    }

    #region metodos que se pueden sobrescribir en otras clases
    public virtual bool UsarItem()
    {
        return true;
    }

    public virtual bool EquiparItem()
    {
        return true;
    }

    public virtual bool BorrarItem()
    {
        return true;
    }

    public virtual string DescripcionItemCrafting()
    {
        return "";
    }
    #endregion
}
