using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayer : MonoBehaviour
{

//TODO: deixar algumas variaveis dentro do respectivo script ao inves de pegar daqui
//TODO: animar mapa, animar barra aumentar folego quando menor, 
//todo: 2travar mouse quando fala, 3interagir melhor/missao melhor, 6camerashaker, lista de missoes, recompensas com itens
//TODO: 4boxcollider no caminhão que ta zoado o collider, 5aumentar cerca, arrumar collider dos gases
//TODO: mudar a mascara de sprite pra textura pq ta tudo sujo
//TODO: fazer o dialogo nao ta todo embaçado
//TODO: Audio tocar quando o objeto for ativado e desativado
//TODO: colocar todos os inputs como zero quando o jogo pausar
//TODO: hierarquia das missoes, missao de escolha, missao de falar com o segurança e a familia
//TODO: tipo missao ativar e buscar sao redundantes
//Scripts
    [SerializeField] internal scrInput input;
    [SerializeField] internal scrCollision col;
    [SerializeField] internal scrMovement mov;

    [SerializeField] internal scrAudio efeitosSom;

    internal scrDialogoTrigger2 dialogo;
    internal scrGiveQuest pegaMissao;

    [SerializeField] private scrCamera camScript;

//Inputs
    internal float x;
    internal float z;
    internal bool _jumpInput;
    internal bool _lanternaInput;
    internal bool _interacaoInput;
    internal bool _mapaInput;
    internal bool _missaoInput;
    internal bool _correrInput;

    internal bool _passarInput;

//Movimento
    [Header("Movimento")]

    [HideInInspector]internal CharacterController controller;
    [SerializeField] internal float baseSpeed=10f;
    internal float walkSpeed=0f;

    internal bool andando;
  
//Pulo   
    [Header("Pulo")]
    [SerializeField] internal float gravidade= -9.81f;
    internal Vector3 aceleramento;

    [SerializeField] internal float alturaPulo=3f;

    [SerializeField] internal Transform groundCheck;
    [SerializeField] internal float groundDistance=0.3f;
    [SerializeField] internal LayerMask groundMask;
    [SerializeField] internal bool isGround; //!Usar isGrounded do charcontrol?
//Respiração
    [Header("Respiração")]
    [SerializeField] internal float folego;
    // public float totalFolego;
    [SerializeField] internal float respiracao; //a cada quanto tempo o jogador é afetado pelo gas
    [SerializeField] internal float toxina=0.20f;
    [SerializeField] internal bool respirando=true;
    [SerializeField] internal Transform gasCheck;
    [SerializeField] internal float gasDistance=0.3f;
    [SerializeField] internal LayerMask gasMask;
//Lanterna
    [Header("Lanterna")]
    [SerializeField] internal GameObject lanterna;
    internal bool onLanterna=false;

    internal Vector3 offsetLanterna;

    [SerializeField] internal float veloLanterna=3.0f;
//Interagir
    [Header("Interagir")]

    [SerializeField] internal Transform mira;
    [SerializeField] internal Transform cam;
    [SerializeField] internal LayerMask layerIndex;
    [SerializeField] internal float pegarAlcance=1f;
    internal bool alcancavel=false;
    [SerializeField] internal float cilindros=0;
//Missões
    [Header("Tarefa")]

    [SerializeField] internal List<scrQuest> missoes= new List<scrQuest>(10); //Colocar todas as quests aqui dentro e atiar cada uma
    [SerializeField] internal int atualMissao=0;//igual ao numero de missoes que tem na lista missoes
    // [SerializeField] internal int karma=0; 

//Artefatos
//Pause
    //internal bool morreu;
    internal bool pausado;
    internal bool fala;
    [SerializeField]bool cursorLock = true;



    //Testes
    public int verficaKarma = scrGlobal.karma;

//Som
[SerializeField] internal GameObject passos;
    

//Start    
    void Start()
    {
        controller= GetComponent<CharacterController>();
        folego=100f;
        lanterna.SetActive(onLanterna);
        offsetLanterna= lanterna.transform.position-cam.position;
        pausado=false;
        //morreu = false;
        
       //cursor
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible=false;
        
    }

    private void Update() 
    {
        verficaKarma = scrGlobal.karma;
        scrGlobal.karma = verficaKarma;

     if (Input.GetButtonDown("Pause"))
    {
        if (pausado)
        {
            Resume();
        }
        else Pause();
    }
    
    if(cursorLock)
    {
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible=false;
        Debug.Log("locked");
    }
    else
    {
        Cursor.lockState= CursorLockMode.None;
        Cursor.visible=true;
        Debug.Log("none");
    }

        /*if (morreu){

            Falando();
            cursorLock = false;

        }*/

    }


    public void Pause()
    {
        Debug.Log("travou");
        Time.timeScale= 0f;
        camScript.currentMouseDelta=new Vector2(0,0); //TODO: melhor travar camera com componente camera
        pausado=true;
        cursorLock=false;
        //adicionar tela de pause que aparece
    }

    public void Resume()
    {
        if (!fala)
        {
        Time.timeScale=1f;    
        }
        pausado = false;
        cursorLock=true;
        //adicionar tela de pause que some
    }

    public void Falando()
    {
       Time.timeScale= 0f;
        camScript.currentMouseDelta=new Vector2(0,0); 
    }

    /*public void respawn (){

        gameObject.transform.position = new Vector3(91.33f, 2.3577f, 62.94f);
        folego = 100f;
        Resume();
        morreu = false;

    }*/
}
