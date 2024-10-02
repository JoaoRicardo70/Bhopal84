using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrCollision : MonoBehaviour
{
   
    [SerializeField] scrPlayer player;

    private Collider[] pisante;
    [SerializeField] private GameObject[] barreiras= new GameObject[2];

    private bool ar=false;
    
    private void FixedUpdate()
    {
        player.respirando=Physics.CheckSphere(player.gasCheck.position,player.gasDistance,player.gasMask);   
        player.isGround=Physics.CheckSphere(player.groundCheck.position,player.groundDistance,player.groundMask);


        //pesado em performance
        // pisante= Physics.OverlapSphere(player.groundCheck.position,player.groundDistance,player.groundMask);
    }
    // private void Update() {
    //     foreach(var pisando in pisante)
    //     {
    //         if (pisando.gameObject.CompareTag("Madeira"))
    //         {
    //             Debug.Log("oqoqoq");
    //         }
    //     }
    // }
    void OnTriggerEnter(Collider other)
    {
        // if(other.tag=="Final")
        // {

        //   if (scrGlobal.karma>=5)
        //   {
        //         //nCenaACarregar = 2;
        //         tCenaACarregar = "FinalBom";
        //         SceneManager.LoadScene(tCenaACarregar);
        //         Debug.Log("Muda cena para final bom");

        //   }else{ 

        //         //nCenaACarregar = 1;
        //         tCenaACarregar = "FinalRuim";
        //         SceneManager.LoadScene(tCenaACarregar);
        //         Debug.Log("Muda cena para final ruim");
        //     }
        // }

        if (other.gameObject.CompareTag("Brisa"))
        {
            Debug.Log("som vento");
            player.efeitosSom.Play("brisa");
            Destroy(other.gameObject);
        }

          if (other.gameObject.CompareTag("Fabrica"))
        {
            Debug.Log("som fabrica assombrada");
            player.efeitosSom.Play("fabrica");
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Vazamento"))
        {
            if (!ar)
            {
            Debug.Log("som gas vazando");
            player.efeitosSom.Play("vazando");
            player.efeitosSom.Stop("arvores");
            }
            else
            {
                Debug.Log("som gas vazando");
                player.efeitosSom.Stop("vazando");
                player.efeitosSom.Play("arvores");
            }
            ar=!ar;
            Debug.Log("ar=" + ar);
        }         

          if (other.gameObject.CompareTag("Gas"))
        {
            Debug.Log("som no gas");
            player.efeitosSom.Play("noGas");
            player.efeitosSom.Stop("semGas");

        }

        if (other.gameObject.CompareTag("Luz"))
        {
            Debug.Log("som Sino");
            player.efeitosSom.Play("sino");
              other.enabled=false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Gas"))
        {
            Debug.Log("som sem gas");
            player.efeitosSom.Play("semGas");
            player.efeitosSom.Stop("noGas");
          

        }
    }

//     public void MudarCena()
//     {
//         StartCoroutine(LoadGame(SceneManager.GetActiveScene().buildIndex+1));
//     }

//     IEnumerator LoadGame(int levelIndex)
//    {
//       //animação painel vindo
//       transicao.SetTrigger("Intro");
//       yield return new WaitForSeconds(transTime);
//       SceneManager.LoadScene(levelIndex);

//    }

//    private void OnCollisionEnter(Collision other)
//    {
//         if (other.gameObject.CompareTag("Madeira"))
//         {
//             Debug.Log("som madeira");
//         }
//    }

    // void OnTriggerExit(Collider other)
    // {
    //     if(other.tag=="Gas")
    //     {
    //         if(player.folego>0)
    //         {
    //         Debug.Log("intoxicado");
    //         player.respirando=true;            
    //         }
    //     }
    // }

   
}
