using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Arma")]
public class ItemArma : InventarioItem
{
    [Header("Arma")]
    public Arma arma;

    public override bool EquiparItem()
    {
        if(ContenedorArma.Instance.ArmaEquipada != null)
        {
            return false;
        }

        ContenedorArma.Instance.EquiparArma(this);
        return true;

    }

    public override bool BorrarItem()
    {
        if(ContenedorArma.Instance.ArmaEquipada == null)
        {
            return false;
        }

        ContenedorArma.Instance.BorrarArma();
        return true;
    }

    public override string DescripcionItemCrafting()
    {
        string descripcion = $"- Crítico: {arma.chanceCritico}%\n" + 
                    $"- Bloqueo: {arma.chanceBloqueo}";
        return descripcion;
    }
}
