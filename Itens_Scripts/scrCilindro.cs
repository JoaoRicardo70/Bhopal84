using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCilindro : MonoBehaviour, IInteractable
{
    scrAudio soundEfeito;
    public void Usar()
    {
        soundEfeito=FindObjectOfType<scrAudio>();
        soundEfeito.Play("tanque");
        Debug.Log("+ 1 cilindro");
        scrGlobal.cilindro++;
        Destroy(gameObject);
    }
}
