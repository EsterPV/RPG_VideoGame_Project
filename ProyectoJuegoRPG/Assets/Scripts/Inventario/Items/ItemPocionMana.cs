using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pocion Mana")]

public class ItemPocionMana : InventarioItem
{
    [Header("Pocion Info")]
    public float MPRestaurar; //Cuanto mana podremos restairar con una pocion

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.personajeMana.sePuedeRestaurar)
        {
            Inventario.Instance.Personaje.personajeMana.RestaurarMana(MPRestaurar);
            return true;
        }
        return false;
    }
}
