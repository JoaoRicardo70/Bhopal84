// using System.Collections;
// using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class scrAudio : MonoBehaviour
{

    public scrSons[] som;

    // Start is called before the first frame update
    void Awake()
    {
       foreach (scrSons s in som)
       {
            s.source= gameObject.AddComponent<AudioSource>();
            s.source.clip= s.clip;
            s.source.volume= s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
       }
    }

    //sons constantes
    private void Start() 
    {
        Play("arvores");
    }

   public void Play(string name)
   {
        scrSons s= Array.Find(som, barulho => barulho.name == name);
        if (s == null)
        {
            Debug.LogWarning("Som: "+ name + " não encontrado!");
            return;
        }
        s.source.Play();
   }

   public void Stop(string name)
   {
        scrSons s= Array.Find(som, barulho => barulho.name == name);
        if (s == null)
        {
            Debug.LogWarning("Som: "+ name + " não encontrado!");
            return;
        }
        s.source.Stop();
   }

   public void OneShotPlay(string name)
   {
     scrSons s= Array.Find(som, barulho => barulho.name == name);
        if (s == null)
        {
            Debug.LogWarning("Som: "+ name + " não encontrado!");
            return;
        }
      s.source.PlayOneShot(s.source.clip);  
   }
}
