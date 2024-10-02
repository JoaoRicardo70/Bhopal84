using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

interface IInteractable
{
    public void Usar();
}

public class scrMovement : MonoBehaviour
{
    [SerializeField] scrPlayer player;

    private float corrida;
    public float velocorrida;

    private bool morto;

    private void FixedUpdate() {
        Mover();
    }


    // Botar todos os inputs aqui
    void Update()
    {
        // Mover();
        Pular();
        Lanterna();
        Respirar();
        Interagir();


      if (scrGlobal.vidaFlor<=0)
        {
           scrGlobal.flor=false;
           player.missoes[1].Falhou();
           //Fazer icone da flor sumir
           Debug.Log("Flor= " + scrGlobal.flor + ", morreu");
        }    
    }

    void Mover()
    {
        //correr bem basico
        if (player._correrInput)
        {
            // player.efeitosSom.OneShotPlay("passosTerra");
            corrida=velocorrida;
        }else corrida = 1;
        //mais devagar andando pros ladoss
        if(player.z<=0)
        {
            player.walkSpeed=(player.baseSpeed * corrida)/1.5f;

        }else player.walkSpeed=player.baseSpeed * corrida;


        Vector3 move= transform.right * player.x + transform.forward * player.z;
       
       //TODO: ARRUMAR PASSOS
        if (!move.Equals(Vector3.zero) && player.isGround && !player.fala)
        {
          player.passos.SetActive(true);
            
        }  else player.passos.SetActive(false);
        // player.efeitosSom.Stop("passosTerra");

      
        
        player.controller.Move(move.normalized * player.walkSpeed * Time.deltaTime);
    }

    void Pular()
    {
        
        //reseta aceleração do cair
         if (player.isGround && player.aceleramento.y<0)
        {
            player.aceleramento.y=-2f;
        }

        if (player._jumpInput)
        {
            if (player.isGround)
            {
                player.aceleramento.y=Mathf.Sqrt(player.alturaPulo * -2 * player.gravidade);  
            }
        }

        player.aceleramento.y += player.gravidade * Time.deltaTime;
        player.controller.Move(player.aceleramento * Time.deltaTime);
        player._jumpInput=false;
    }

    void Lanterna()
    {
        Transform luz= player.lanterna.transform;
        luz.position= player.cam.position + player.offsetLanterna;
        luz.rotation= Quaternion.Slerp(luz.rotation,player.cam.rotation,player.veloLanterna*Time.deltaTime);
        if (player._lanternaInput)
        {
             if(!player.onLanterna)
            {
	            Debug.Log("Ligou");
                player.efeitosSom.Play("ClickLanterna");
                player.lanterna.SetActive(true);
                player.onLanterna=true;
            }else
            {
	            Debug.Log("Desligou");
                player.efeitosSom.Play("ClickLanterna");
                player.lanterna.SetActive(false);
                player.onLanterna=false;
            }
            player._lanternaInput=false;
        }
    }

    void Respirar()
    {
        if(player.folego<=0)
        {
            if(scrGlobal.cilindro>0)
            {
            scrGlobal.cilindro--;
            player.folego=100f; //reseta folego
            Debug.Log("menos " + scrGlobal.cilindro + " cilindro");
            }else
            {
               
               //player.morreu = true;
               Debug.Log("F");
                SceneManager.LoadScene(0);
            } 
        }else StartCoroutine(Intoxicado()); //Se tiver folego continua sendo intoxicado
    }

   /* void morte(){

        if(morto){
            player.transform.position = new Vector3(91.33f, 2.3577f, 62.94f);
            player.folego = 100f;
            morto = false;
        } //Morre
    }*/

    IEnumerator Intoxicado()
    {
        yield return new WaitForSeconds(player.respiracao * Time.deltaTime);
        if(player.respirando & player.folego>0)
        {
            // player.efeitosSom.OneShotPlay("mascaraGas");
             player.folego-= player.toxina*Time.deltaTime;
             if (scrGlobal.flor && scrGlobal.vidaFlor>0)
             {
                scrGlobal.vidaFlor-=player.toxina* Time.deltaTime;
                Debug.Log("vida da flor= " + scrGlobal.vidaFlor);
             }
        }
        // else player.efeitosSom.Stop("mascaraGas");

        //recupera o folego
        /*else if(!player.respirando & player.folego<100)
        { 
            player.folego+=player.toxina * Time.deltaTime;
            Debug.Log(player.folego);
        }*/
    }

//TODO: Limpar essa coisa ai
//     void Interagir()
//     {
//        RaycastHit hitInfo;
//         Debug.DrawRay(player.mira.position,player.cam.forward * player.pegarAlcance,Color.red);

        

//     if(Physics.Raycast(player.mira.position, player.cam.TransformDirection(Vector3.forward), out hitInfo, player.pegarAlcance, player.layerIndex))
    
//     {
//         Debug.Log("Espionildo");

//         if (player._interacaoInput)
//         {
//             //player.cam.TransformDirection(Vector3.forward) Focar camera?? 


//             //se ir pelo caminho de todo interagivel ter
//             //! um codigo em si, tirar a layerIndex, pq so vai ativar se tiver script interagivel
            
//             //Ativa o raio somente quando pressiona interagir
//             // Ray hit= new Ray(player.mira.position, player.cam.forward);
//             // if (Physics.Raycast(hit, out RaycastHit hitInfo, player.pegarAlcance, player.layerIndex))
//             // {

// //aqui funfiona 
               

//                 if(hitInfo.collider.tag=="Item")
//                 {
//                  // totalFolego+=10;
//                     player.cilindros++;
//                     Debug.Log("pegou" + player.cilindros+ "cilindros");
//                     Destroy(hitInfo.collider.gameObject);
//                 } 
//                 //interage com as portas, fazer todas elas serem unicas em dialogo e missao, adicionar som
//                 else if (hitInfo.collider.tag=="Porta")
//                 {
//                     Debug.Log("toctoc");

//                     //tirar pegaMissao e dialogo do player, deixar como variavel local
//                     player.pegaMissao=hitInfo.collider.GetComponent<scrGiveQuest>();
//                     player.dialogo = hitInfo.collider.GetComponent<scrDialogoTrigger2>();

//             //!PROBLEMA: retorna nuloXX
//             if (player.missoes[ player.pegaMissao.indexMissao].isActive)
//             {
//             //se pegou artefato
//                 // if( player.missoes[ player.pegaMissao.indexMissao].artefato==null)
//                 // {
//                 //     player.missoes[player.pegaMissao.indexMissao].tipo.ItemEntregue();
//                 //     Debug.Log("Artefato entregue"); !BUSCAR
//             //ficou meio redundante, mas a gnt melhora

//                 //se a missao for do tipo Falar, ver se a missao do index da porta desejada esta ativa, se sim, ItemEntregue()
//                 if (player.missoes[player.missoes[player.pegaMissao.indexMissao].portaMissao].isActive)
//                 {
//                     player.missoes[player.pegaMissao.indexMissao].tipo.ItemEntregue();
//                     Debug.Log("whatsapp humano");
//                 }
            
//             //Se cumpriu a missao
//                 if(player.missoes[player.pegaMissao.indexMissao].tipo.MissaoCumprida())
//                     {
//                         player.karma++;
//                         player.missoes[player.pegaMissao.indexMissao].Completa();
//                         Debug.Log(player.missoes[player.pegaMissao.indexMissao].titulo + " completa");
//                         player.missoes[player.pegaMissao.indexMissao]=null;
//                     }
//                 // } !Buscar            
//             }else
//             { 
//                 player.pegaMissao.IniciaMissao();
//                 player.dialogo.começaDialogos();
//             }
//             }    
//             else if (hitInfo.collider.tag=="Artefato")
//             {
//             //scrItem vai ter a peculiridade que vai ser ativa quando o item for pego, mudando dependendo do tipo dele
//             player.missoes[player.pegaMissao.indexMissao].tipo.ItemAtivo();
//             Destroy(hitInfo.collider.gameObject);
//             Debug.Log("Pegou artefato");
//             }
//             // } 
// //aqui funciona


//         player._interacaoInput=false;
//         }
//     }
//     }

void Interagir()
{
    RaycastHit hitInfo;
    Debug.DrawRay(player.mira.position,player.cam.forward * player.pegarAlcance,Color.red);

    if(Physics.Raycast(player.mira.position, player.cam.TransformDirection(Vector3.forward), out hitInfo, player.pegarAlcance, player.layerIndex))
    
    {
        player.alcancavel=true;
        Debug.Log("Espionildo" + player.alcancavel);

        if (player._interacaoInput)
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interagivel))
            {
                interagivel.Usar();
            }
            player._interacaoInput=false;
        }
    }else player.alcancavel=false;

}
}
