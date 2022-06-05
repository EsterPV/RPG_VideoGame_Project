using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salir : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        salir();
    }

    public void salir()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            
        }
        
    }
}
