using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DropItem
{
    [Header("Info")]
    public string Nombre;
    public InventarioItem item;
    public int cantidad;

    [Header("Probabilidad de aparición")]
    [Range(0, 100)]public float porcentajeDrop; //porcentaje de probabilidad de que aparezca el objeto tras derrotar a un enemigo

    //Propiedades 
    public bool ItemRecogido { get; set; }
}
