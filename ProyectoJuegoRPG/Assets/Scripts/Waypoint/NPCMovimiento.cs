using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovimiento : WaypointMovimiento
{
    [SerializeField] private DireccionMovimiento direccion;

    private readonly int caminarAbajo = Animator.StringToHash("CaminarAbajo");

    protected override void GirarPersonaje()
    {
        if (direccion != DireccionMovimiento.Horizontal) //si la direccion del movimiento es vertical 
        {
            return; //salimos del método
        }

        if (PuntoDestino.x > ultimaPosicion.x) //si no hemos alcanzado el punto 
        {
            transform.localScale = new Vector3(1, 1, 1); //giramos el personaje a la derecha
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); //giramos el personaje a la izquierda

        }
    }

    protected override void GirarVertical()
    {
        if ( direccion != DireccionMovimiento.Vertical)
        {
            return;
        }

        if(PuntoDestino.y > ultimaPosicion.y)
        {
            animator.SetBool(caminarAbajo, false);
        }
        else
        {
            animator.SetBool(caminarAbajo, true);
        }
    }
}
