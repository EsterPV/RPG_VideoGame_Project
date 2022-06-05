using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisiones/Personaje En Rango De Ataque")]
public class DecisionPersonajeRangoAtaque : IADecision
{
    public override bool Decidir(IAController controller)
    {
        return EnRangoDeAtaque(controller);
    }

    private bool EnRangoDeAtaque(IAController controller)
    {
        if(controller.PersonajeReferencia == null)
        {
            return false;
        }

        float distancia = (controller.PersonajeReferencia.position - controller.transform.position).sqrMagnitude;
        if(distancia < Mathf.Pow(controller.RangoAtaqueDeterminado, 2))
        {
            return true;
        }
        return false;
    }
}
