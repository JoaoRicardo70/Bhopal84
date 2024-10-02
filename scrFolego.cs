using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scrFolego : MonoBehaviour
{
// private Image barraFolego;
[SerializeField] private GameObject pegaFolego;
private Image barraFolego;
//mascara
// [SerializeField] private Sprite gasMask, mask;
// private Image mascara;
private RawImage mascara;
[SerializeField] Texture gasMascara, arMascara;

[SerializeField] GameObject imgMascara;

//mapa
[SerializeField] private GameObject mapCidade;
private bool openMapa=false;
public float atualFolego;
public float maxFolego;
scrPlayer Playerscript;

public Gradient gradient;

//Missoes
[SerializeField] private GameObject missaoBarra; 
[SerializeField] private TMP_Text[] missoesAtivas= new TMP_Text[6];
[SerializeField] private LinkedList<string> descricaoLista = new LinkedList<string>();
[SerializeField] private LinkedList<TMP_Text> descricaoMissao = new LinkedList<TMP_Text>();

 int j=0;

//Alcancar
[SerializeField] private GameObject textoAlcancavel;


//Aumenta a barra total
public float width;
public float height;
public float razao;
[SerializeField] RectTransform tamanhoFolego;
// private float extraFolego; Aumenta a barra 

//Pegar cilindros
public TMP_Text cilindrosTxt;

//Itens

[SerializeField] GameObject iconFlor;
[SerializeField] GameObject iconChave;
[SerializeField] GameObject iconAlicate;

private float FlorMax;



//Pause
[SerializeField] GameObject menuPausa;
[SerializeField] GameObject telaMorte;


    // Start is called before the first frame update
    void Awake()
    {
        barraFolego=pegaFolego.GetComponent<Image>();
        // mascara=GetComponent<Image>();
        mascara=imgMascara.GetComponent<RawImage>();
        Playerscript= FindObjectOfType<scrPlayer>();
        barraFolego.color= gradient.Evaluate(1f);

        
        //Aumenta a barra total pra sinalizar que esta acabando
        width = tamanhoFolego.sizeDelta.x;
        height = tamanhoFolego.sizeDelta.y;
        razao= width/height;

        maxFolego= Playerscript.folego; 
        FlorMax = scrGlobal.vidaFlor;

        mapCidade.SetActive(false);
        missaoBarra.SetActive(false);
        menuPausa.SetActive(false);
        iconFlor.gameObject.SetActive(false);
        telaMorte.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        atualFolego= Playerscript.folego;

        //Aumenta a barra total
        // extraFolego= Playerscript.totalFolego;
        // if (maxFolego<extraFolego)
        // {
        //     maxFolego=extraFolego;
        //     width+=20;
        // }
        // tamanhoFolego.sizeDelta = new Vector2(width, tamanhoFolego.sizeDelta.y);
       
       //Aumenta numero de barras
       if(scrGlobal.cilindro>0) cilindrosTxt.text= "x" + scrGlobal.cilindro.ToString();
       else cilindrosTxt.text= "";
       
       
        //deixa a barra dinamica diminundo conforme seu folego atual
        barraFolego.fillAmount= atualFolego/maxFolego;
        //muda cor quando está baixo
        barraFolego.color= gradient.Evaluate(atualFolego/maxFolego);

        /*(if (atualFolego<maxFolego/4)
        {
            width+=5f;
            height+=5f;
        }*/
        tamanhoFolego.sizeDelta= new Vector2(width, height);

        //mascara filtro
        if(Playerscript.respirando)
        {
            // mascara.sprite=gasMask;
            mascara.texture=gasMascara;
            Debug.Log("com gas");
        }else
        {
            Debug.Log("sem gas");
            mascara.texture=arMascara;
        }

        Mapa();
        Alcanca();
        Pause();
        Itens();
        MexerFlor();
        //voceMorreu();
        // if (!Playerscript.fala)
        // {
        // textoAlcancavel.SetActive(Playerscript.alcancavel);
            
        // }else textoAlcancavel.SetActive(false);
        Tarefas();
        
    }

    void Mapa()
    {
        //TODO: Aanimação
        if (Playerscript._mapaInput)
        {
            if (openMapa==false)
            {
            Playerscript.efeitosSom.Play("maoMapa");
            mapCidade.SetActive(true);
            openMapa=true;
            }else
            {
            Playerscript.efeitosSom.Play("maoMapa");
            mapCidade.SetActive(false);
            openMapa=false;
            }
          Playerscript._mapaInput=false;
        }
    }
//!WORK IN PROGRESS
    void Tarefas()
    {
        missaoBarra.SetActive(Playerscript._missaoInput);
        // TMP_Text localMissao;
        //Se uma missao é ativa, ela é adicionada a  lista abaixo da mais recente e a formatação acompanha
        //Se a missão for completa, retira ela da lista e todos os que vem depois dele diminuem um espaço
        // j=Playerscript.atualMissao;
         
        
        if(Playerscript.missoes[Playerscript.pegaMissao.indexMissao].isActive)
        {
        //  localMissao=Playerscript.atualMissao;  
        string missaoDescricao=Playerscript.missoes[Playerscript.pegaMissao.indexMissao].descricao;
        
        if (!descricaoLista.Contains(missaoDescricao))
        {
            
        
        descricaoLista.AddLast(missaoDescricao);
        Debug.Log(descricaoLista.Last.Value.ToString());

       // Encontra o próximo slot disponível no array textosDasMissoes
        int index = Array.FindIndex(missoesAtivas, texto => texto.text == string.Empty);
        if (index >= 0)
        {
            missoesAtivas[index].text = missaoDescricao;
        }

           // Certifique-se de que missoesAtivas[j] é uma referência válida ao componente de texto
        // if (missoesAtivas.Length > j && missoesAtivas[j] != null)
        // {
        // missoesAtivas[j].text = descricaoMissao.Last.Value.ToString();
        // j++;
        // }   

        //  if (missoesAtivas.Length >= descricaoLista.Count)
        // {
        //     int i = 0;
        //     foreach (var descricao in descricaoLista)
        //     {
        //         if (i < missoesAtivas.Length && missoesAtivas[i] != null)
        //         {
        //             missoesAtivas[i].text = descricao.ToString();
        //             i++;
        //         }
        //     }
        // }
        }
        }

        if (Playerscript.missoes[Playerscript.pegaMissao.indexMissao].completo)
        {
        // descricaoLista.Remove(Playerscript.missoes[Playerscript.pegaMissao.indexMissao].descricao);

        string descricao = Playerscript.missoes[Playerscript.pegaMissao.indexMissao].descricao;

            if (descricaoLista.Contains(descricao))
            {
                descricaoLista.Remove(descricao);

            TMP_Text textoRemovido = Array.Find(missoesAtivas, texto => texto.text == descricao);
            if (textoRemovido != null)
            {
                textoRemovido.text = string.Empty;
            }

                // Encontra o índice do elemento no array missoesAtivas e remove-o
                // int index = Array.IndexOf(missoesAtivas, descricao);
                // if (index >= 0)
                // {
                // for (int i = index; i < missoesAtivas.Length - 1; i++)
                // {
                //     missoesAtivas[i] = missoesAtivas[i + 1];
                // }
                // Array.Resize(ref missoesAtivas, missoesAtivas.Length - 1);
                // }
    }
        }
        
        // else
        // {
        //     //se a missao for completa
        //     if (!Playerscript.missoes[Playerscript.atualMissao].isActive)
        //     {
        //         descricaoMissao[n].text=""; //ou deixa desativado
        //         foreach (var item in collection)
        //         {
                    
        //         }
        //     }
        // }
      
    }

    
    void Alcanca()
    {
         if (!Playerscript.fala)
        {
        textoAlcancavel.SetActive(Playerscript.alcancavel);
            
        }else textoAlcancavel.SetActive(false);
    }

    void Itens()
    {
        iconAlicate.SetActive(scrGlobal.alicate);
        iconFlor.SetActive(scrGlobal.flor);
        iconChave.SetActive(scrGlobal.chave);
    }

    void MexerFlor()
    {
        Slider florSlider= iconFlor.GetComponent<Slider>();
        florSlider.value=scrGlobal.vidaFlor/FlorMax;
        
    }

    void Pause()
    {
        menuPausa.SetActive(Playerscript.pausado);
    }

    /*void voceMorreu(){

        telaMorte.SetActive(Playerscript.morreu);

    }*/
}
