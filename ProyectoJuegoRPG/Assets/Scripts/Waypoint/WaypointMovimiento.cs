using UnityEngine;

public enum DireccionMovimiento
{
    Horizontal,
    Vertical
}

public class WaypointMovimiento : MonoBehaviour
{
    [SerializeField] protected float velocidad;

    public Vector3 PuntoDestino => waypoint.ObtenerPosMovimiento(puntoActualIndice);

    protected Waypoint waypoint;
    protected Animator animator;
    protected int puntoActualIndice;
    protected Vector3 ultimaPosicion;

    // Start is called before the first frame update
    void Start()
    {
        puntoActualIndice = 0;
        animator = GetComponent<Animator>();
        waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        MoverPersonaje();
        GirarPersonaje();
        GirarVertical();
        if (ComprobarPuntoAlcanzado())
        {
            ActualizarIndexMovimiento();
        }
    }

    private void MoverPersonaje()
    {
        transform.position = Vector3.MoveTowards(transform.position, PuntoDestino, velocidad * Time.deltaTime);
    }

    private bool ComprobarPuntoAlcanzado()
    {
        float distanciaPuntoActual = (transform.position - PuntoDestino).magnitude;
        if(distanciaPuntoActual < 0.1f)
        {
            ultimaPosicion = transform.position;
            return true;
        }
        return false;
    }

    private void ActualizarIndexMovimiento()
    {
        if(puntoActualIndice == waypoint.Puntos.Length - 1)
        {
            puntoActualIndice = 0;
        }
        else
        {
            if (puntoActualIndice < waypoint.Puntos.Length - 1)
            {
                puntoActualIndice++;
            }
        }
    }

    protected virtual void GirarPersonaje()
    {
       
    }

    protected virtual void GirarVertical()
    {

    }

}
