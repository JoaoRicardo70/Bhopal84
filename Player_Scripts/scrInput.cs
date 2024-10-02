using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrInput : MonoBehaviour
{
    [SerializeField] scrPlayer player;

    // Update is called once per frame
    void Update()
    {
if (!player.pausado)
{
    
Debug.Log("Mexivel");
if (!player.fala)
{
    

//movimento        
        player.x= Input.GetAxisRaw("Horizontal");
        player.z= Input.GetAxisRaw("Vertical");
       

//pulo
        if (Input.GetButtonDown("Jump"))
        {
            player._jumpInput=true;
        }

// //pause
//         if (Input.GetButtonDown("Cancel"))
//         {
//             //pause
//             player._pauseInput=true;
//             Application.Quit();
//             Debug.Log("Saiu");
//         }

//lanterna
        if(Input.GetButtonDown("Lanterna"))
        {
            player._lanternaInput=true;
        }        

//interagir
        if(Input.GetMouseButtonDown(1))
        {
            player._interacaoInput=true;
            Debug.Log("APERTOU");

            //interage
        }else
        {
            player._interacaoInput=false;
            Debug.Log("PAROU DE APERTAR");
            
        }
      
//Mapa
        if (Input.GetButtonDown("Mapa"))
        {
            player._mapaInput=true;
        }

//Missao
if (Input.GetButtonDown("Missao"))
{
    player._missaoInput=true;
}
if (Input.GetButtonUp("Missao"))
{
    player._missaoInput=false;
}

//Correr 
if (Input.GetButtonDown("Correr"))
{
    player._correrInput=true;
}
if (Input.GetButtonUp("Correr"))
{
    player._correrInput=false;
} 


}
//proxima fala
        if (Input.GetMouseButtonDown(0))
        {
            player._passarInput=true;
        }  

}      
}
}
