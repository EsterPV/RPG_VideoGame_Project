using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Pocion Vida")]
public class ItemPocionVida : InventarioItem
{
    [Header("Pocion Info")]
    public float HPRestaurar; //Cuanta vida podremos restairar con una pocion

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.personajeVida.puedeSerCurado)
        {
            Inventario.Instance.Personaje.personajeVida.restaurarVida(HPRestaurar);
            return true;
        }
        return false;
    }

    public override string DescripcionItemCrafting()
    {
        string descripcion = $"Restaura {HPRestaurar} de Salud";
        return descripcion;
    }

     
}
