using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scrDialogManager : MonoBehaviour
{
    [SerializeField] scrPlayer player;

	//[SerializeField] bool temEscolha = false;
	//public bool mostraEscolha = false;
	//public GameObject escolhas;
	public GameObject caixaTexto;
	public Image actorImage;
	public TextMeshProUGUI actorNome;
	public TextMeshProUGUI textoFala;
	public RectTransform backgroundBox;

	Fala[] currentMessages;
	Actor[] currentActors;
	int activeMessage = 0;
	public static bool isActive = false;

	public void abreDialogos(Fala[] falas, Actor[] actors){

		currentMessages = falas;
		currentActors = actors;
		activeMessage = 0;
		isActive = true;

		//Debug.Log("Started conversation! Load!" + currentMessages.Length);
		mostraFala();

	}

	public void mostraFala(){

		Fala messageToDisplay = currentMessages[activeMessage];
		textoFala.text = messageToDisplay.fala;

		Actor actorToDisplay = currentActors[messageToDisplay.actorId];
		actorNome.text = actorToDisplay.name;
		actorImage.sprite = actorToDisplay.sprite;

	}

	public void proximaFala(){

		activeMessage++;
		if(activeMessage < currentMessages.Length){
            
			mostraFala();

		}else{

			Debug.Log("FIM DE PAPO.");
			activeMessage=0;
			isActive = false;
			player.fala=false;
			player.Resume();

		}

	}

	void Start(){

		caixaTexto.SetActive(false);
		isActive = false;
		//mostraEscolha = false;
		//escolhas.SetActive(false);

	}

	void Update(){

		if (!isActive) caixaTexto.SetActive(false);
		else caixaTexto.SetActive(true);

		if(player._passarInput && isActive == true){
			player.efeitosSom.Play("skip");
			proximaFala();
			player._passarInput=false;
		}

	}

	/*void escolha (){

		if (temEscolha){

			escolhas.SetActive(true);

		}

	}*/


}


