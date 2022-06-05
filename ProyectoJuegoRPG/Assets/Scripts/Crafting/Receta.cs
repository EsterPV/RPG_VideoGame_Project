using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Receta 
{
    public string nombre;

    [Header("1er Material")]
    public InventarioItem item1;
    public int item1CantidadRequerida;

    [Header("2do Material")]
    public InventarioItem item2;
    public int item2CantidadRequerida;

    [Header("Resultado")]
    public InventarioItem itemResultado;
    public int itemResultadoCantidad;
}
