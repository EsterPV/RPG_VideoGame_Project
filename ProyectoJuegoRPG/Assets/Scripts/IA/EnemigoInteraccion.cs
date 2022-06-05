using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoDeDeteccion
{
    Rango,
    Melee
}
public class EnemigoInteraccion : MonoBehaviour
{
    [SerializeField] private GameObject seleccionRangoFX;
    [SerializeField] private GameObject seleccionMeleeFX;

    public void MostrarEnemigoSeleccionado(bool estado, TipoDeDeteccion tipo)
    {

        if(tipo == TipoDeDeteccion.Rango)
        {
            seleccionRangoFX.SetActive(estado);
        }
        else
        {
            seleccionMeleeFX.SetActive(estado);
        }

        
    }

    public void DesactivarSpritesSeleccion()
    {
        seleccionMeleeFX.SetActive(false);
        seleccionRangoFX.SetActive(false);
    }

    
}
