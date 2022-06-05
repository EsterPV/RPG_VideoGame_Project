using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Config")]
    [SerializeField]private GameObject panelLoot;
    [SerializeField] private LootButton lootBtnPrefab;
    [SerializeField] private Transform lootContenedor;

    public void MostrarLoot(EnemigoLoot enemigoLoot)
    {
        panelLoot.SetActive(true);
        if (ContenedorOcupado())
        {
            foreach(Transform hijo in lootContenedor.transform)
            {
                Destroy(hijo.gameObject);
            }
        }

        for(int i = 0; i < enemigoLoot.LootSeleccionado.Count; i++)
        {
            CargarLootPanel(enemigoLoot.LootSeleccionado[i]);
        }
    }

    private void CargarLootPanel(DropItem dropItem)
    {
        if (dropItem.ItemRecogido)
        {
            return;
        }

        LootButton loot = Instantiate(lootBtnPrefab, lootContenedor);
        loot.ConfigurarLootItem(dropItem);
        loot.transform.SetParent(lootContenedor);
    }
    
    public void CerrarPanel()
    {
        panelLoot.SetActive(false);
    }

    private bool ContenedorOcupado()
    {
        LootButton[] hijos = lootContenedor.GetComponentsInChildren<LootButton>();
        if(hijos.Length > 0)
        {
            return true;
        }
        
         return false;

    }
}
