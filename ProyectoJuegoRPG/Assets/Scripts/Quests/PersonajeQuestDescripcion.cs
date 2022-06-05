using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonajeQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;
    [SerializeField] private TextMeshProUGUI tareaObjetivo;

    [Header("Item")]
    [SerializeField] private Image recompensaItemIcono;
    [SerializeField] private TextMeshProUGUI recompensaItemCantidad;

    private void Update()
    {
        if (QuestCargado.questCompletado)
        {
            return;
        }

        tareaObjetivo.text = $"{QuestCargado.cantidadActual}/{QuestCargado.cantidadObjetivo}";
    }

    public override void ConfigurarQuestUI(Quests questPorCargar)
    {
        base.ConfigurarQuestUI(questPorCargar);
        recompensaOro.text = questPorCargar.recompensaOro.ToString();
        recompensaExp.text = questPorCargar.recompensaExp.ToString();
        tareaObjetivo.text = $"{questPorCargar.cantidadActual}/{questPorCargar.cantidadObjetivo}";

        recompensaItemIcono.sprite = questPorCargar.recompensaItem.item.Icono;
        recompensaItemCantidad.text = questPorCargar.recompensaItem.cantidad.ToString();
    }

    private void QuestCompletadoRespuesta(Quests questRespuesta)
    {
        if(questRespuesta.ID == QuestCargado.ID)
        {
            tareaObjetivo.text = $"{QuestCargado.cantidadActual}/{QuestCargado.cantidadObjetivo}";
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (QuestCargado.questCompletado)
        {
            gameObject.SetActive(false);
        }

        Quests.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    private void OnDisable()
    {
        Quests.EventoQuestCompletado -= QuestCompletadoRespuesta;

    }
}
