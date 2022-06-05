using TMPro;
using UnityEngine;

public class TextoAnimacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI danhoTexto;

    public void EstablecerTexto(float cantidadDanho, Color color)
    {
        danhoTexto.text = cantidadDanho.ToString();
        danhoTexto.color = color;
    }
}
