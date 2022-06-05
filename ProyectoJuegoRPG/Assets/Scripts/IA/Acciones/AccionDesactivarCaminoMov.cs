using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Desactivar Camino Movimiento")]
public class AccionDesactivarCaminoMov : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        if (controller.EnemigoMovimientoProp == null)
        {
            return;
        }

        controller.EnemigoMovimientoProp.enabled = false;
    }
}
