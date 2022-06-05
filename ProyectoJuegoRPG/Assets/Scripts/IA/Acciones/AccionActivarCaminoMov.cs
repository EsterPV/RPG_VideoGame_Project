using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Activar Camino Movimiento")]
public class AccionActivarCaminoMov : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        if(controller.EnemigoMovimientoProp == null)
        {
            return;
        }

        controller.EnemigoMovimientoProp.enabled = true;
    }
}
