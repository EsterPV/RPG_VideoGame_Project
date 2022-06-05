using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Musica : MonoBehaviour
{
   [SerializeField] private new AudioSource audio;
   [SerializeField] private BoxCollider2D box;
    bool haEntrado = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && haEntrado == false )
        {
            audio.Play();
            haEntrado = true;
            box.enabled=false;
        }else if(collision.CompareTag("Player") && haEntrado == true)
        {
            audio.Pause();
            haEntrado = false;
        }
    }
}
