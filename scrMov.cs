using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMov : MonoBehaviour
{
       //https://dot.net/core-sdk-vscode
    //TODO: condição para desativar o cursor lock caso precise no jogo 
    //TODO: Event system pra parar de refenciar tudo
    //Verificar se andar na diagonal faz o player ir mais rapido X
    //smooth para movimentação da lanterna X
   //aumentar velocidade da respiracao X
   //ajustar valores a escala do jogador X
   //mira não segue camera fielmente X
    //camera mais realista, embaçar ao virar??
   //separar scripts em input, colisão, ação e um geral que conecta tudo;

    public scrDialogoTrigger2 dialogo;
    [Header("Movimento")]

    [HideInInspector]public CharacterController controller;
    public float baseSpeed=10f;
    float walkSpeed=0f;
    
   
    
    //pulo
    [Header("Pulo")]
    public float gravidade= -9.81f;
    Vector3 aceleramento;

    public float alturaPulo=3f;

    public Transform groundCheck;
    public float groundDistance=0.3f;
    public LayerMask groundMask;
    public bool isGround; //!Usar isGrounded do charcontrol?

//oxigenio
    [Header("Oxigenio")]
    public float folego;
    // public float totalFolego;
    public float respiracao; //a cada quanto tempo o jogador é afetado pelo gas
    public float toxina=0.20f;
    public bool respirando=true;
//lanterna
    [Header("Lanterna")]
    [SerializeField] GameObject lanterna;
    private bool onLanterna=false;

    private Vector3 offsetLanterna;

    [SerializeField] private float veloLanterna=3.0f;

    [Header("Interagir")]
//pegar item
    [SerializeField] internal Transform mira;
    [SerializeField] internal Transform cam;
    [SerializeField] public LayerMask layerIndex;
    public float pegarAlcance=1f;
    public float cilindros=0;

    [Header("Tarefa")]
//missoes
    public List<scrQuest> missoes= new List<scrQuest>(10); //Colocar todas as quests aqui dentro e atiar cada uma
    public scrGiveQuest pegaMissao;
    public int atualMissao=0;//igual ao numero de missoes que tem na lista missoes
    public int karma=0;    

    
    

    void Start()
    {
        controller=GetComponent<CharacterController>();
        //totalFolego=100f;
        folego=100f;
        lanterna.SetActive(onLanterna);
        offsetLanterna= lanterna.transform.position-cam.position;
    }

    // void FixedUpdate()
    // {
      
    // }

    // Update is called once per frame
    void Update()
    {
       //movimento
        float x= Input.GetAxis("Horizontal");
        float z= Input.GetAxis("Vertical");

        //diminuir a velocidade se anda para frente
        //DONE:  arrumar a diagonal mais rapido, NORMALIZE
        if (z<=0)
        {
            walkSpeed=baseSpeed/1.5f;
        }else walkSpeed=baseSpeed;


        //movimento local que leva em conta onde o jogador está virado com a câmera
        Vector3 move= transform.right * x + transform.forward * z;
        //metodo Move 432do character controller que usa um vector3 para realizar o movimento, corrige com delta time
        controller.Move(move.normalized * walkSpeed * Time.deltaTime);
        
        //gravidade

        //ground check
        isGround=Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);
        //reseta cair
        // controller.isground()
        if (isGround && aceleramento.y<0)
        {
            aceleramento.y=-2f;

        }
        //Pulo, tentar colocar dentro do isground em cima 
         if(Input.GetButtonDown("Jump") && isGround)
        {
            aceleramento.y=Mathf.Sqrt(alturaPulo * -2 * gravidade);
        }
        //gravidade,valor de velocidade que aumenta com o tempo
        aceleramento.y+= gravidade * Time.deltaTime;
        //tempo ao quadrado pela formula de queda livre
        controller.Move(aceleramento * Time.deltaTime);


        Lanterna();


        //cilindro, tirar do update
        if(folego<=0)
        {
            if(cilindros>0)
            {
            cilindros--;
            folego=100f;
            Debug.Log("menos " + cilindros + " cilindro");
            }else Debug.Log("F");
        }else StartCoroutine(DamageFromGas());

        //sair do jogo
        if(Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
            Debug.Log("Saiu do jogo");
        }
        
        Interagir();

    
        
    }
//lanterna
    void Lanterna()
    {
        lanterna.transform.position= cam.position+offsetLanterna;
        lanterna.transform.rotation= Quaternion.Slerp(lanterna.transform.rotation,cam.rotation,veloLanterna*Time.deltaTime);
        if(Input.GetButtonDown("Lanterna"))
        {
            if(!onLanterna)
            {
	            Debug.Log("Ligou");
                lanterna.SetActive(true);
                onLanterna=true;
            }else
            {
	            Debug.Log("Desligou");
                lanterna.SetActive(false);
                onLanterna=false;
            }
        }
    }    
//colisoes
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Gas")
        {
            if(folego>0)
            {
            Debug.Log("intoxicado");
            respirando=false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag=="Gas")
        {
            if(folego>0)
            {
            Debug.Log("intoxicado");
            respirando=true;            
            }
        }
    }
//respiracao  
    IEnumerator DamageFromGas()
    {
        yield return new WaitForSeconds(respiracao);
        if(!respirando & folego>0)
        {
             folego-= toxina*Time.deltaTime;
        }
        //recupera o folego
        else if(respirando & folego<100)
        { 
            folego+=toxina * Time.deltaTime;
            Debug.Log(folego);
        }
    }
    
//pegar
//TODO: pegar item é inconsistente, tentar separar input e fisica em uptade e fixedupdate
    private void Interagir()
    {
        RaycastHit hit;
        Debug.DrawRay(mira.position,cam.forward*pegarAlcance,Color.red);
        if(Physics.Raycast(mira.position, cam.TransformDirection(Vector3.forward), out hit, pegarAlcance, layerIndex)
        && Input.GetMouseButtonDown(1))
        {
        //pega cilindro
        if(hit.collider.tag=="Item")
        {
        // totalFolego+=10;
        cilindros++;
        Debug.Log("pegou" +cilindros+ "cilindros");
        Destroy(hit.collider.gameObject);
        } 
        //interage com as portas, fazer todas elas serem unicas em dialogo e missao, adicionar som
        else if (hit.collider.tag=="Porta")
        {
            Debug.Log("toctoc");
            // pegaMissao.IniciaMissaoBusca();
            pegaMissao=hit.collider.GetComponent<scrGiveQuest>();
            dialogo = hit.collider.GetComponent<scrDialogoTrigger2>();

            //!PROBLEMA: retorna nulo
            if (missoes[pegaMissao.indexMissao].isActive)
            {
            if(missoes[pegaMissao.indexMissao].artefato==null)
            {
                missoes[pegaMissao.indexMissao].tipo.ItemEntregue();
                Debug.Log("Artefato entregue");
            //ficou meio redundante, mas a gnt melhora
            
            if(missoes[pegaMissao.indexMissao].tipo.MissaoCumprida())
            {
                karma++;
                missoes[pegaMissao.indexMissao].Completa();
                missoes.RemoveAt(pegaMissao.indexMissao);
                Debug.Log(missoes[pegaMissao.indexMissao].titulo + " completa");
            }
            }            
        }else pegaMissao.IniciaMissao();

        dialogo.começaDialogos();
        }    
        else if (hit.collider.tag=="Artefato")
        {
            //scrItem vai ter a peculiridade que vai ser ativa quando o item for pego, mudando dependendo do tipo dele
            Destroy(hit.collider.gameObject);
            Debug.Log("Pegou o ursinho");
        }
        }
    }
    }
    //player deslizando
    
 //limitar velocidade quando fizer correr

