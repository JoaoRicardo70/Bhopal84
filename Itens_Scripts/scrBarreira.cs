using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBarreira : MonoBehaviour, IInteractable
{

    [SerializeField] private Collider Obstaculo;

 
    //TODO: melhorar esse sistema aqui
    public void Usar()
    {
        if (Obstaculo.tag=="Portao")
        {
            if (scrGlobal.chave)
            {
                Obstaculo.enabled=false;
                Destroy(Obstaculo.gameObject);
                Debug.Log("Abriu portao");
            }   
        }else
        {
            if (scrGlobal.alicate)
            {
                Obstaculo.enabled=false;
                Destroy(Obstaculo.gameObject);
            }   
        }
    }
}
