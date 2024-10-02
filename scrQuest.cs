using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class scrQuest 
{
    public bool isActive=false; //quando o player aceita a quest é ativada
    public GameObject artefato; //collider ativo quando inicia a missao, se for de buscar ou ativar ele é destruido
    //se for de falar, a luz da porta acende, o collider ativa e a zona de sol ativa quando for iniciada e voce completa depois que falou pela primeira vez e levou a informação pro questgiver, ele te da recompensa e faz o dialogo de cumprir missao
    public string titulo;

    public scrTypeQuest tipo; 

    public string descricao;

    public int portaMissao; 

    public bool completo=false;

    //Recompensa


//depois que completa nao pode fazer de novo
    public void Completa()
    {
        isActive=false;
        completo=true;
        tipo.obtido=0;
        tipo.recompensar();
        //! tocar som FEITO
        //remove missao da lista
    }
    //som que toca quando pega o item ou sla
    //npc
    //missao
    //objeto libera a interação com o objeto e cada objeto tem suas peculiaridasdes, player pega e deve entregar a missao de volta no npc
    //progresso novos dialogos e missoes que o npc vai dar apos voce completar
    //recompensa aumenta a honra/ da algum item que te ajud/libera alguma funcionalidade
    //tipo missao criar um script que mostra as pssibilidades

    //mostrar missoes ativas, itens pegos, e pra onde levar, fazer os itens terem colisoes ativas ou nao ou sla, desativar quando acabar

    public void Falhou()
    {
        tipo.obtido=0;
        // artefato.GetComponent<BoxCollider>().enabled=false;
    }
  
}
