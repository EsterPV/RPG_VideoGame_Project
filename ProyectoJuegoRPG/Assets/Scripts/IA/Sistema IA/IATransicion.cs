using System;
using UnityEngine;

[Serializable]
public class IATransicion 
{
    public IADecision decision;
    public IAEstado estadoVerdadero;
    public IAEstado estadoFalso;
}
