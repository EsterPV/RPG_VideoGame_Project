using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootButton : MonoBehaviour
{
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;

    //propiedades
    public DropItem ItemPorRecoger { get; set; }

    public void ConfigurarLootItem(DropItem dropItem)
    {
        ItemPorRecoger = dropItem;
        itemIcono.sprite = dropItem.item.Icono;
        itemNombre.text = $"{dropItem.item.Nombre} x{dropItem.cantidad}";
    }

    public void RecogerItem()
    {
        if(ItemPorRecoger == null) { return; }

        Inventario.Instance.AnhadirItem(ItemPorRecoger.item, ItemPorRecoger.cantidad);
        ItemPorRecoger.ItemRecogido = true;
        Destroy(gameObject);
    }
}
