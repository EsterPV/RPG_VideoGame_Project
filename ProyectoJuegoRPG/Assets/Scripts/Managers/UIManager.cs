using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : Singleton<UIManager>
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Paneles")]
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelTienda;
    [SerializeField] private GameObject panelCrafting;
    [SerializeField] private GameObject panelCraftingInfo;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject panelInspectorQuests;
    [SerializeField] private GameObject panelPersonajeQuests;

    [Header("Barra")]
    [SerializeField] private Image vidaPlayer;
    [SerializeField] private Image manaPlayer;
    [SerializeField] private Image expPlayer;

    [Header("Texto")]
    [SerializeField] private TextMeshProUGUI vidaTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI nivelTMP;
    [SerializeField] private TextMeshProUGUI monedasTMP;

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI statDanhoTMP;
    [SerializeField] private TextMeshProUGUI statDefensaTMP;
    [SerializeField] private TextMeshProUGUI statCriticoTMP;
    [SerializeField] private TextMeshProUGUI statBloqueoTMP;
    [SerializeField] private TextMeshProUGUI statVelocidadTMP;
    [SerializeField] private TextMeshProUGUI statNivelTMP;
    [SerializeField] private TextMeshProUGUI statExpTMP;
    [SerializeField] private TextMeshProUGUI statExpReqTMP;
    [SerializeField] private TextMeshProUGUI atributoFuerzaTMP;
    [SerializeField] private TextMeshProUGUI atributoInteligenciaTMP;
    [SerializeField] private TextMeshProUGUI atributoDestrezaTMP;
    [SerializeField] private TextMeshProUGUI atributoPtosDisponiblesTMP;

    private float vidaActual;
    private float vidaMax;
    private float manaActual;
    private float manaMax;
    private float expActual;
    private float expRequeridaNuevoNivel;

    

    // Update is called once per frame
    void Update()
    {
        actualizarUIPersonaje();
        actualizarPanelStats();
    }

    private void actualizarUIPersonaje()
    {
        vidaPlayer.fillAmount = Mathf.Lerp(vidaPlayer.fillAmount, vidaActual / vidaMax, 10f * Time.deltaTime);

        manaPlayer.fillAmount = Mathf.Lerp(manaPlayer.fillAmount, manaActual / manaMax, 10f * Time.deltaTime);

        expPlayer.fillAmount = Mathf.Lerp(expPlayer.fillAmount, expActual / expRequeridaNuevoNivel, 10f * Time.deltaTime);


        vidaTMP.text = $"{vidaActual}/{vidaMax}";
        manaTMP.text = $"{manaActual}/{manaMax}";
        expTMP.text = $"{((expActual/expRequeridaNuevoNivel)* 100):F2}%";
        nivelTMP.text = $"Nivel {stats.Nivel}";
        monedasTMP.text = MonedasManager.Instance.MonedasTotales.ToString();

    }

    private void actualizarPanelStats()
    {
        if(panelStats.activeSelf == false) // si el panel no está activo no hacemos nada
        {
            return;
        }

        statDanhoTMP.text = stats.Danho.ToString();
        statDefensaTMP.text = stats.Defensa.ToString();
        statCriticoTMP.text = $"{stats.PorcentajeCritico}%";
        statBloqueoTMP.text = $"{stats.PorcentajeBloqueo}%";
        statVelocidadTMP.text = stats.Velocidad.ToString();
        statNivelTMP.text = stats.Nivel.ToString();
        statExpTMP.text = stats.ExpActual.ToString();
        statExpReqTMP.text = stats.ExpReq.ToString();

        //actualizamos el texto de los atributos
        atributoFuerzaTMP.text = stats.Fuerza.ToString();
        atributoInteligenciaTMP.text = stats.Inteligencia.ToString();
        atributoDestrezaTMP.text = stats.Destreza.ToString();
        atributoPtosDisponiblesTMP.text = $"Puntos: {stats.puntosDisponibles}";

    }

    public void actualizarvidaPersonaje(float pVidaActual, float pVidaMax)
    {
        vidaActual=pVidaActual ;
        vidaMax = pVidaMax;
    }

    public void actualizarManaPersonaje(float pManaActual, float pManaMax)
    {
        manaActual = pManaActual;
        manaMax = pManaMax;
    }

    public void actualizarExpPersonaje(float pExpActual, float pExpRequerida)
    {
        expActual = pExpActual;
        expRequeridaNuevoNivel = pExpRequerida;
    }

    #region Paneles

    public void AbrirCerrarPanelStats()
    {
        if ((Input.GetKeyDown(KeyCode.Space))) { return; }
        //cierra y abre el panel de stats activeself nos regresa si el panel está activo o no (true o false)
        panelStats.SetActive(!panelStats.activeSelf);

        if ((panelStats.activeSelf || panelInventario.activeSelf || panelPersonajeQuests.activeSelf) == true)
        {
            Time.timeScale = 0;
        }
        else if ((panelStats.activeSelf || panelInventario.activeSelf || panelPersonajeQuests.activeSelf) == false)
        {
            Time.timeScale = 1;
        }
    }

    public void AbrirCerrarPanelInventario()
    {
        if ((Input.GetKeyDown(KeyCode.Space))) { return; }
        panelInventario.SetActive(!panelInventario.activeSelf);

        if ((panelStats.activeSelf || panelInventario.activeSelf || panelPersonajeQuests.activeSelf) == true)
        {
            Time.timeScale = 0;
        }else if ((panelStats.activeSelf || panelInventario.activeSelf || panelPersonajeQuests.activeSelf) == false)
        {
            Time.timeScale = 1;
        }
    }

    public void AbrirCerrarPersonajeQuest()
    {
        if ((Input.GetKeyDown(KeyCode.Space))) { return; }
        panelPersonajeQuests.SetActive(!panelPersonajeQuests.activeSelf);

        if ((panelStats.activeSelf || panelInventario.activeSelf || panelPersonajeQuests.activeSelf) == true)
        {
            Time.timeScale = 0;
        }
        else if ((panelStats.activeSelf || panelInventario.activeSelf || panelPersonajeQuests.activeSelf) == false)
        {
            Time.timeScale = 1;
        }
    }

    public void AbrirCerrarPanelQuests()
    {
        panelInspectorQuests.SetActive(!panelInspectorQuests.activeSelf);
    }

    public void AbrirCerrarPanelTienda()
    {
        panelTienda.SetActive(!panelTienda.activeSelf);
    }

    public void AbrirPanelCrafting()
    {
        panelCrafting.SetActive(true);
    }

    public void CerrarPanelCrafting()
    {
        panelCrafting.SetActive(false);
        AbrirCerrarPanelCraftingInfo(false);
    }

    public void AbrirCerrarPanelCraftingInfo(bool estado)
    {
        panelCraftingInfo.SetActive(estado);
    }

    public void AbrirPanelInteraccion(InteraccionExtraNPC tipoInteraccion)
    {
        switch (tipoInteraccion)
        {
            case InteraccionExtraNPC.Quests:
                AbrirCerrarPanelQuests();
                break;
            case InteraccionExtraNPC.Tienda:
                AbrirCerrarPanelTienda();
                break;
            case InteraccionExtraNPC.Crafting:
                AbrirPanelCrafting();
                break;
        }
    }

   

    #endregion
}
