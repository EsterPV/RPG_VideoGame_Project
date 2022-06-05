using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Seguir Personaje")]

public class AccionSeguirPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        SeguiPersonaje(controller);
    }

    private void SeguiPersonaje( IAController controller)
    {
        if(controller.PersonajeReferencia == null)
        {
            return;
        }

        Vector3 direccionAPersonaje = controller.PersonajeReferencia.position - controller.transform.position;
        Vector3 direccion = direccionAPersonaje.normalized;
        float distancia = direccionAPersonaje.magnitude;

        if(distancia >= 1.30f)
        {
            controller.transform.Translate(direccion * controller.VelocidadMovimiento * Time.deltaTime);
        }
    }
}
