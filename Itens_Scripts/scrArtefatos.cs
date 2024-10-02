using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrArtefatos : MonoBehaviour, IInteractable
{
    [SerializeField] scrPlayer player;
    public int indexObjeto;

    private void Update() {
        if (gameObject.tag=="Flor")
        {
            if (scrGlobal.vidaFlor<0)
            {
            gameObject.GetComponent<BoxCollider>().enabled=true;  
            Debug.Log("Flor interagivel");   
            }
        }
    }

   

    public void Usar()
    {
        
        Debug.Log(player.pegaMissao + "MISSÃO"); 
        //TODO: so dar itemativo se ele ainda nao foi pego ou obtido==0
        player.missoes[indexObjeto].tipo.ItemAtivo();
        if (gameObject.tag!="Flor")
        {
            Destroy(gameObject);
            if (gameObject.tag=="Cano")
            {
            player.efeitosSom.Play("cano");
            }
        }else 
        {
            //interação flor
            scrGlobal.flor=true;
            scrGlobal.vidaFlor=30f;
            gameObject.GetComponent<BoxCollider>().enabled=false; 

            Debug.Log("Pegou Flor");
        }
       
        Debug.Log("Pegou artefato");
    }
}
