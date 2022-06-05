using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Personaje")]
    [SerializeField] private Personaje personaje;

    [Header("Quests")]
    [SerializeField] private Quests[] questDisponibles;

    [Header("Inspector Quests")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;

    [Header("Personaje Quests")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;

    [Header("Panel Quest Completado")]
    [SerializeField] private GameObject panelQuestCompletado;
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questRecompensaOro;
    [SerializeField] private TextMeshProUGUI questRecompensaExp;
    [SerializeField] private TextMeshProUGUI questRecompensaItemCantidad;
    [SerializeField] private Image questRecompensaIcono;

    [Header("Fin del juego")]
    [SerializeField] private GameObject panelFinaldeJuego;

    public Quests QuestPorReclamar { get; private set; }
    public Quests QuestFinal { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CargarQuestInspector();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    AnhadirProgreso("Mata10", 1);
        //    AnhadirProgreso("Mata20", 1);
        //    AnhadirProgreso("Mata40", 1);
        //}
    }

    private void CargarQuestInspector()
    {
        for(int i = 0; i< questDisponibles.Length; i++)
        {
            InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
        }
    }

    private void AnhadirQuestPorCompletar(Quests questPorCompletar)
    {
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    public void AnhadirQuest(Quests questPorCompletar)
    {
        AnhadirQuestPorCompletar(questPorCompletar);
    }

    public void ReclamarRecompensa()
    {
        if(QuestPorReclamar == null)
        {
            return;
        }

        MonedasManager.Instance.AnhadirMonedas(QuestPorReclamar.recompensaOro);
        personaje.personajeExp.anhadirExperiencia(QuestPorReclamar.recompensaExp);
        Inventario.Instance.AnhadirItem(QuestPorReclamar.recompensaItem.item, QuestPorReclamar.recompensaItem.cantidad);
        panelQuestCompletado.SetActive(false);
        QuestPorReclamar = null;
    }

    public void siBoton()
    {
        panelFinaldeJuego.SetActive(false);
    }

    public void noBoton()
    {
        panelFinaldeJuego.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AnhadirProgreso(string questID, int cantidad)
    {
        Quests questPorActualizar = QuestExiste(questID);
        questPorActualizar.AnhadirProgreso(cantidad);
    }

    private Quests QuestExiste(string questID)
    {
        for(int i=0; i < questDisponibles.Length; i++)
        {
            if(questDisponibles[i].ID == questID)
            {
                return questDisponibles[i];
            }
        }

        return null;
    }

    private void MostrarQuestCompletado(Quests questCompletado)
    {
        panelQuestCompletado.SetActive(true);
        questNombre.text = questCompletado.nombre;
        questRecompensaOro.text = questCompletado.recompensaOro.ToString();
        questRecompensaExp.text = questCompletado.recompensaExp.ToString();
        questRecompensaItemCantidad.text = questCompletado.recompensaItem.cantidad.ToString();
        questRecompensaIcono.sprite = questCompletado.recompensaItem.item.Icono;
    }

    private void QuestCompletadoRespuesta(Quests questCompletado)
    {
        QuestPorReclamar = QuestExiste(questCompletado.ID);
        QuestFinal = QuestExiste("Mata40");
        if(QuestPorReclamar != null)
        {
            MostrarQuestCompletado(QuestPorReclamar);
            if(QuestPorReclamar == QuestFinal)
            {
                panelFinaldeJuego.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        Quests.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    private void OnDisable()
    {
        Quests.EventoQuestCompletado -= QuestCompletadoRespuesta;

    }

   
}
