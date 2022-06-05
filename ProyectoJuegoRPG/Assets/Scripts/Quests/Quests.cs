using UnityEngine;
using System;

[CreateAssetMenu]
public class Quests : ScriptableObject
{
    public static Action<Quests> EventoQuestCompletado;

    [Header("Info")]
    public string nombre;
    public string ID;
    public int cantidadObjetivo;

    [Header("Descripcion")]
    [TextArea] public string descripcion;

    [Header("Recompensas")]
    public int recompensaOro;
    public float recompensaExp;
    public QuestRecompensaItem recompensaItem;

    [HideInInspector] public int cantidadActual;
    [HideInInspector] public bool questCompletado;

    public void AnhadirProgreso(int cantidad)
    {
        cantidadActual += cantidad;
        VerificarQuestCompletado();
    }

    private void VerificarQuestCompletado()
    {
        if (cantidadActual >= cantidadObjetivo)
        {
            cantidadActual = cantidadObjetivo;
            QuestCompletado();
        }
    }

    private void QuestCompletado()
    {
        if (questCompletado)
        {
            return;
        }
        questCompletado = true;
        EventoQuestCompletado?.Invoke(this);
    }

    private void OnEnable()
    {
        questCompletado = false;
        cantidadActual = 0;
    }
}



[Serializable]
public class QuestRecompensaItem
{
    public InventarioItem item;
    public int cantidad;
}