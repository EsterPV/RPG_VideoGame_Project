using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogoManager : Singleton<DialogoManager>
{
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Image npcIcono;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    public NPCInteraccion NPCDisponible { get; set; }

    private Queue<string> dialogoSecuencia;
    private bool dialogoAnimacion;
    private bool despedidaMostrada;

    private void Start()
    {
        dialogoSecuencia = new Queue<string>();
    }

    private void Update()
    {
        if(NPCDisponible == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) )
        {
            
            
            if (yaSalio == true) { return; }
            else
            {
                ConfigurarPanel(NPCDisponible.Dialogo);
                
            }
        }

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(despedidaMostrada )
            {
                AbrirCerrarPanelDialogo(false);
                despedidaMostrada = false;
                yaSalio = false;
                return;
            }

            if (NPCDisponible.Dialogo.contieneInteraccionExtra)
            {
                UIManager.Instance.AbrirPanelInteraccion(NPCDisponible.Dialogo.interaccionExtra);
                AbrirCerrarPanelDialogo(false);
                yaSalio = false;
                return;
            }

            if (dialogoAnimacion)
            {
                ContinuarDialogo();
                return;
            }
          
        }
        
    }

    public void AbrirCerrarPanelDialogo (bool estado)
    {
        panelDialogo.SetActive(estado);
    }
    bool yaSalio;
    private void ConfigurarPanel(NPCDialogo nPCDialogo)
    {
        
        AbrirCerrarPanelDialogo(true);
        CargarDialogosSecuencia(nPCDialogo);

        npcIcono.sprite = nPCDialogo.Icono;
        npcNombreTMP.text = $"{nPCDialogo.Nombre}:";

        MostrarTextoAnimado(nPCDialogo.saludo);
        yaSalio = true;
       
            

    }

    private void CargarDialogosSecuencia(NPCDialogo npcDialogo)
    {
        if(npcDialogo.conversacion == null || npcDialogo.conversacion.Length <= 0)
        {
            return;
        }

        for(int i = 0; i < npcDialogo.conversacion.Length; i++)
        {
            dialogoSecuencia.Enqueue(npcDialogo.conversacion[i].oracion);
        }
    }

    private void ContinuarDialogo()
    {
        if(NPCDisponible == null)
        {
            return;
        }

        if (despedidaMostrada)
        {
            return;
        }

        if(dialogoSecuencia.Count == 0)
        {
            string despedida = NPCDisponible.Dialogo.despedida;
            MostrarTextoAnimado(despedida);
            despedidaMostrada = true;
            return;
        }
       
        
            string siguienteDialogo = dialogoSecuencia.Dequeue();
            MostrarTextoAnimado(siguienteDialogo);
   
        
    }

    private IEnumerator AnimarTexto(string oracion)
    {
        dialogoAnimacion = false;
        npcConversacionTMP.text = "";
        char[] letras = oracion.ToCharArray();

        for(int i = 0; i< letras.Length; i++)
        {
            npcConversacionTMP.text += letras[i];
            yield return new WaitForSeconds(0.0f);
        }


        dialogoAnimacion = true;

    }

    private void MostrarTextoAnimado( string oracion)
    {
        StartCoroutine(AnimarTexto(oracion));
    }
}
