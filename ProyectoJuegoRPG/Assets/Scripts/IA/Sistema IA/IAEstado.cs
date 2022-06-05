using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Estado")]
public class IAEstado : ScriptableObject
{
    public IAAccion[] acciones;
    public IATransicion[] transiociones;

    public IAEstado IAEstado1
    {
        get => default;
        set
        {
        }
    }

    public void EjecutarEstado(IAController controller)
    {
        EjecutarAcciones(controller);
        RealizarTransiciones(controller);
    }

    private void EjecutarAcciones(IAController controller)
    {
        if(acciones == null || acciones.Length <= 0)
        {
            return;
        }

        for(int i=0; i < acciones.Length; i++)
        {
            acciones[i].Ejecutar(controller);
        }
    }

    private void RealizarTransiciones(IAController controller)
    {
        if(transiociones == null || transiociones.Length <= 0)
        {
            return;
        }

        for(int i = 0; i < transiociones.Length; i++)
        {
            bool decisionValor = transiociones[i].decision.Decidir(controller);
            if (decisionValor)
            {
                controller.CambiarEstado(transiociones[i].estadoVerdadero);
            }
            else
            {
                controller.CambiarEstado(transiociones[i].estadoFalso);

            }
        }
    }
}
