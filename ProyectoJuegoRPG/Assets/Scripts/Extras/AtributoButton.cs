using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoAtributo //lista donde recojo los botones
{
    Fuerza,
    Inteligencia,
    Destreza
}

public class AtributoButton : MonoBehaviour
{
    public static Action<TipoAtributo> EventoAgregarAtributo; //recojo la lista de botones tipoAtributo para lanzar un evento
    [SerializeField] private TipoAtributo tipo; //propiedad segun el tipo de atributo de cada boton

    public void AgregarAtributo() //lanzo el evento según el tipo de boton que hemos apretado
    {
        EventoAgregarAtributo?.Invoke(tipo); //simbolo de pregunta significa: "si esto no es nulo, lo invocamos"
    }

    //En el script de Personaje se va a escuchar este evento
}
