using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoArma
{
    Magia,
    Melee
}

[CreateAssetMenu(menuName = "Personaje/Arma")]
public class Arma : ScriptableObject
{
    [Header("Config")]
    public Sprite armaIcono;
    public Sprite iconoSkill;
    public TipoArma tipo;
    public float danho;

    [Header("Arma Mágica")]
    public Proyectil proyectilPrefab;
    public float manaRequerida;


    [Header("Stats")]
    public float chanceCritico;
    public float chanceBloqueo;
}
