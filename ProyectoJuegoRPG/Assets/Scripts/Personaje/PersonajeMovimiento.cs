using System;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    private Rigidbody2D _rigigbody2D;
    private Vector2 _input; //vector para guardar  las direcciones
    private Vector2 _direccionMovimiento; //vector para manipular hacia donde se mueve el personaje

    [SerializeField] private float velocidad; //serializedField para poder manipularlo en Unity y privado para no modificarlo en otras clases sin querer

    public Vector2 DireccionMovimiento => _direccionMovimiento; // Creo esta propiedad que regresa lo mismo que _direccionMovimiento para acceder de forma segura desde otras clases
    public bool enMovimiento => _direccionMovimiento.magnitude > 0f; //propiedad para definir si el personaje está en movimiento o no (si la margnitud de direccionM es mayor que 0 nos movemos)

    private void Awake()
    {
        _rigigbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //Guarda hacia que dirección nos queremos mover
        
        //X (MOVIMIENTO HORIZONTAL)
        if(_input.x > 0.1f) // si el vector en x es mayor que 0.1 significa que nos movemos a la derecha
        {
            _direccionMovimiento.x = 1f; //direccion x es positivo (derecha)

        }else if(_input.x< 0f) //si el vector x es menor que 0 nos movemos a la izquierda
        {
            _direccionMovimiento.x = -1f; //direccion x es negativo (izquierda)
        }
        else
        {
            _direccionMovimiento.x = 0f; //No nos movemos horizontalmente
        }

        //Y (MOVIMIENTO VERTICAL)
        if (_input.y > 0.1f) // si el vector en x es mayor que 0.1 significa que nos movemos hacia arriba
        {
            _direccionMovimiento.y = 1f; //direccion x es positivo (arriba)

        }
        else if (_input.y < 0f) //si el vector x es menor que 0 nos movemos hacia abajo
        {
            _direccionMovimiento.y = -1f; //direccion x es negativo (abajo)
        }
        else
        {
            _direccionMovimiento.y = 0f; //No nos movemos verticalmente
        }
    }

    private void FixedUpdate()
    {
        _rigigbody2D.MovePosition(_rigigbody2D.position + _direccionMovimiento * velocidad * Time.fixedDeltaTime);
        //Movemos nuestro personaje teniendo en cuenta la posicion actual, direccion del movimiento (segun las teclas que apretamos), la velocidad, el time
    }
}
