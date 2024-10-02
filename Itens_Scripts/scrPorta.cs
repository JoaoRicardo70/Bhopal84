using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPorta : MonoBehaviour, IInteractable
{
    [SerializeField] scrPlayer player;

  


    // [SerializeField] scrAudio sEffect;
    [SerializeField] private Collider colisaoPorta;

    [SerializeField] private GameObject luz;
    [SerializeField] private bool enfermeira=false;

    private bool localCompleto=false;
    

//TODO: criar variavel pra nao precisar escrever tanto de novo
   private void Update() {

//so enfermeira, trocar colisaoPorta por completo
    if (enfermeira && localCompleto)
    // colisaoPorta.enabled==false) //!Apagar
    {
        colisaoPorta.enabled=scrGlobal.flor;
        Debug.Log("Colisao porta e luz=" + scrGlobal.flor);
    }
    

    if(colisaoPorta.enabled)
    {
        luz.SetActive(true);
    }else luz.SetActive(false);
   }
   

    public void Usar()
    {
        Debug.Log("Toc toc");
        // sEffect=FindObjectOfType<scrAudio>();
        player.efeitosSom.Play("TocToc");



        player.pegaMissao= GetComponent<scrGiveQuest>();
        // player.dialogo= dialogo1;

        player.dialogo= GetComponent<scrDialogoTrigger2>();

        if(enfermeira && scrGlobal.flor)
        {
            player.missoes[1].tipo.ItemAtivo();
            scrGlobal.flor=false;
        }

        if (player.missoes[player.pegaMissao.indexMissao].isActive)
        {
                 Debug.Log(player.pegaMissao + "MISSÃO"); 
       

            // if (player.missoes[player.missoes[player.pegaMissao.indexMissao].portaMissao].isActive)
            // {
            //     player.missoes[player.pegaMissao.indexMissao].tipo.ItemEntregue();
            //     Debug.Log("whatsapp humano");
            // }

            //Se cumpriu a missao
            if(player.missoes[player.pegaMissao.indexMissao].tipo.MissaoCumprida())
            {
                // scrGlobal.karma++;
                // player.dialogo=dialogo2;
                // player.dialogo.começaDialogos();

                player.dialogo.cumpriuDialogos();

                player.missoes[player.pegaMissao.indexMissao].Completa();
                Debug.Log(player.missoes[player.pegaMissao.indexMissao].titulo + " completa" + " /Karma=" + scrGlobal.karma);
                // player.missoes[player.pegaMissao.indexMissao]=null;
                localCompleto=true;
                colisaoPorta.enabled= false;
                
                
                //! Teste barra de missão
                player.atualMissao--;

            }
                // } !Buscar            
            }else
            { 
                if (!player.missoes[player.pegaMissao.indexMissao].completo)
                {
                    // player.dialogo= dialogo1;
                    player.dialogo.começaDialogos();
                    player.pegaMissao.IniciaMissao();
                    Debug.Log(player.pegaMissao + "MISSÃO"); 
                }
            }

            //requisito de missao de fala 
            //* da pra melhorar fazendo todos os objetos dentro do gameobject artefato, para missao buscar ele verifica se o artefato nao foi destruido e para falar verifica se a missao do componente scrGiveQuest do artefato esta ativada e aumenta os obtidos, mas sem tempo agr fazer depois se pa
            //* ou para missao falar, se a pegaMissao de agora for igual ao index do artefato da missao de falar, item entregue
            if (player.missoes[player.missoes[player.pegaMissao.indexMissao].portaMissao].isActive)
            {
                player.missoes[player.missoes[player.pegaMissao.indexMissao].portaMissao].tipo.ItemEntregue();
                Debug.Log("whatsapp humano");
            }
    }
}    


