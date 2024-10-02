using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDialogoTrigger2 : MonoBehaviour
{
    public Fala [] falas;
    public Actor[] actors;

    public Fala [] fala2;
    public Actor[] actor2;
    //TODO: travar o player com um jeito melhor no futuro
    [SerializeField] scrPlayer player;

    public bool estado=false;


    public void começaDialogos(){
        
        scrDialogManager.isActive = true;
        FindObjectOfType<scrDialogManager>().abreDialogos(falas, actors); 
        player.Falando();
        player.fala=true;
        estado=true;


//TODO: adicionar missao
        if (player.missoes[player.pegaMissao.indexMissao].tipo.tipoMissao==TipoMissao.Escolha)
        // player.missoes[player.pegaMissao.indexMissao].tipo.TipoMissao.Falar)
        {
            Debug.Log("oaskdak");
        }

    }

    public void cumpriuDialogos()
    {
        scrDialogManager.isActive=true;
        FindObjectOfType<scrDialogManager>().abreDialogos(fala2, actor2); 
        player.Falando();
        player.fala=true;
        estado=true;
    }

}

[System.Serializable]
public class Fala{

    public int actorId;
    public string fala;

}

[System.Serializable]
public class Actor{

    public string name;
    public Sprite sprite;

}
