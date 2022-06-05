using TMPro;
using UnityEngine;

public class QuestDescripcion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questDescripcion;

    public Quests QuestCargado { get; set; }

    public virtual void ConfigurarQuestUI(Quests questPorCargar)
    {
        QuestCargado = questPorCargar;
        questNombre.text = questPorCargar.nombre;
        questDescripcion.text = questPorCargar.descripcion;
    }
}
