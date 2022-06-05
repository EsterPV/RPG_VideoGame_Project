using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMana : MonoBehaviour
{
    [SerializeField] private float manaInicial;
    [SerializeField] private float manaMax;
    [SerializeField] private float regeneracionSegundo;

    public float manaActual { get; private set; }
    public bool sePuedeRestaurar => manaActual < manaMax;


    private PersonajeVida _personajeVida;

    private void Awake()
    {
        _personajeVida = GetComponent<PersonajeVida>();
    }

    // Start is called before the first frame update
    void Start()
    {
        manaActual = manaInicial;
        actualizarBarraMana();

        InvokeRepeating(nameof(regenerarMana), 1, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            usarMana(10f);
        }
    }

    public void usarMana(float cantidad)
    {
        if(manaActual >= cantidad)
        {
            manaActual -= cantidad;
            actualizarBarraMana();
        }
    }

    public void RestaurarMana(float cantidad)
    {
        if(manaActual >= manaMax)
        {
            return;
        }

        manaActual += cantidad;
        if(manaActual > manaMax)
        {
            manaActual = manaMax;
        }

        UIManager.Instance.actualizarManaPersonaje(manaActual, manaMax);
    }

    private void regenerarMana()
    {
        if(_personajeVida.Salud > 0f && manaActual < manaMax)
        {
            manaActual += regeneracionSegundo;
            actualizarBarraMana();
        }
    }

    public void restablecerMana()
    {
        manaActual = manaInicial;
        actualizarBarraMana();
    }

    private void actualizarBarraMana()
    {
        UIManager.Instance.actualizarManaPersonaje(manaActual, manaMax);
    }
}
