using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGiveQuest : MonoBehaviour
{
    // public scrQuest missao;

    //TODO: colocar aqui a lista de tarefas, adicionar quando missao iniciar
    public scrPlayer playerScript;

    public Collider objetivo; //so pra ficar mais facil de ler, mas é simplesmente o collider do artefato
    public int indexMissao; //cada missao deve ser atribuido um index unico ao ser iniciada
   
// ta dando pra pegar missao duas vezes ?
    public void IniciaMissao()
    {
      if(!playerScript.missoes[indexMissao].isActive)
      {
        playerScript.missoes[indexMissao].isActive=true;
        // missao.isActive=true
        // playerScript.missoes.Add(missao);
       
       //Ativa collider do artefato
        objetivo=playerScript.missoes[indexMissao].artefato.GetComponent<Collider>(); 
        objetivo.enabled= true;
        playerScript.atualMissao= indexMissao;
        //  "Artefato"; mudar tag de "porta" para "artefato" dependendo da função ?
        // indexMissao=playerScript.atualMissao;
        // playerScript.atualMissao++;
        Debug.Log("iniciou a missão");
      }

    }
}




