
using UnityEngine;
using UnityEngine.UI;

public class ContenedorArma : Singleton<ContenedorArma>
{
    [SerializeField] private Image armaIcono;
    [SerializeField] private Image armaSkillIcono;

    public ItemArma ArmaEquipada { get; set; }

   public void EquiparArma(ItemArma itemArma)
    {
        ArmaEquipada = itemArma;
        armaIcono.sprite = itemArma.arma.armaIcono;
        armaIcono.gameObject.SetActive(true);

        if(itemArma.arma.tipo == TipoArma.Magia)
        {
            armaSkillIcono.sprite = itemArma.arma.iconoSkill;
            armaSkillIcono.gameObject.SetActive(true);
        }

        Inventario.Instance.Personaje.personajeAtaque.EquiparArma(itemArma);
    }

    public void BorrarArma()
    {
        armaIcono.gameObject.SetActive(false);
        armaSkillIcono.gameObject.SetActive(false);
        ArmaEquipada = null;
        Inventario.Instance.Personaje.personajeAtaque.BorrarArma();
    }

}
