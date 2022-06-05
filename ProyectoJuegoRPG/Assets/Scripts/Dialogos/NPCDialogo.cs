using System;
using UnityEngine;

public enum InteraccionExtraNPC
{
    Quests,
    Tienda,
    Crafting
}

[CreateAssetMenu]
public class NPCDialogo : ScriptableObject
{
    [Header("Informacion")]
    public string Nombre;
    public Sprite Icono;
    public bool contieneInteraccionExtra;
    public InteraccionExtraNPC interaccionExtra;

    [Header("Saludo")]
    [TextArea]public string saludo;

    [Header("Conversacion")]
    public DialogoTexto[] conversacion;

    [Header("Despedida")]
    [TextArea] public string despedida;


}

[Serializable]
public class DialogoTexto
{
    [TextArea] public string oracion;
}
