using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class scrFinal : MonoBehaviour, IInteractable
{

    public int nCenaACarregar;
    public string tCenaACarregar;
    public bool usaIntParaCarregarCena = true;
    public Animator transicao;
    public float transTime;

   public void Usar()
   {
        if (scrGlobal.karma>=1)
          {
                //nCenaACarregar = 2;
                tCenaACarregar = "FinalBom";
                SceneManager.LoadScene(tCenaACarregar);
                Debug.Log("Muda cena para final bom");

          }
          else{ 

                //nCenaACarregar = 1;
                tCenaACarregar = "FinalRuim";
                SceneManager.LoadScene(tCenaACarregar);
                Debug.Log("Muda cena para final ruim");
            }

   }

     public void MudarCena()
    {
        StartCoroutine(LoadGame(SceneManager.GetActiveScene().buildIndex+1));
    }

    IEnumerator LoadGame(int levelIndex)
   {
      //animação painel vindo
      transicao.SetTrigger("Intro");
      yield return new WaitForSeconds(transTime);
      SceneManager.LoadScene(levelIndex);

   }
}
