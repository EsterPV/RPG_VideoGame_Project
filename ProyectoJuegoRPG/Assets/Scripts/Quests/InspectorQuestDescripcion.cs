using TMPro;
using UnityEngine;

public class InspectorQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa;

    public override void ConfigurarQuestUI(Quests questPorCargar)
    {
        base.ConfigurarQuestUI(questPorCargar);

        questRecompensa.text = $"- {questPorCargar.recompensaOro} oro" +
            $"\n- {questPorCargar.recompensaExp} exp"+
            $"\n- {questPorCargar.recompensaItem.item.Nombre} x{questPorCargar.recompensaItem.cantidad}";

    }

    public void AceptarQuest()
    {
        if(QuestCargado == null)
        {
            return;
        }

        QuestManager.Instance.AnhadirQuest(QuestCargado);
        gameObject.SetActive(false);
    }
}
