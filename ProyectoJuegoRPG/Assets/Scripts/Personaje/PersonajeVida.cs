using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PersonajeVida : VidaBase //esta clase hereda de la clase VidaBase
{
    public static Action eventoPersonajeDerrotado;

    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TextMeshProUGUI gameOver;

    public bool derrotado { get; private set; }
    public bool puedeSerCurado => Salud < saludMax;

    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void Start()
    {
        base.Start(); //llama al start de la clase VidaBase
        //Cada vez que inicio el juego actualizo la barra de vida
        ActualizarBarraVida(Salud, saludMax);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    recibirDaño(10);
        //}
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    restaurarVida(10);
        //}
        if (derrotado == true)
        {
       
            System.Threading.Thread.Sleep(4000);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            panelGameOver.SetActive(false);
        }
    }


    public void restaurarVida(float cantidad)
    {
        if (derrotado)
        {
            return;
        }

        if (puedeSerCurado)
        {
            Salud += cantidad;
            if(Salud > saludMax)
            {
                Salud = saludMax;
            }
            ActualizarBarraVida(Salud, saludMax);
        }
    }

    protected override void personajeDerrotado()
    {
        boxCollider2D.enabled = false;
        derrotado = true;
        panelGameOver.SetActive(true);
        eventoPersonajeDerrotado?.Invoke(); //esta linea es lo mismo que el siguiente if
        //if(eventoPersonajeDerrotado != null)
        //{
        //    eventoPersonajeDerrotado.Invoke();
        //}
    }

    public void restaurarPersonaje()
    {
        boxCollider2D.enabled = true;
        derrotado = false;
        Salud = saludInicial;
        ActualizarBarraVida(Salud, saludInicial);
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        UIManager.Instance.actualizarvidaPersonaje(vidaActual, vidaMax); //instancio la clase UIManager y llamo a su método actualizarvidapersonaje
    }
   
}
