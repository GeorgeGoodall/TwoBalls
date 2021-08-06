using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
   public static GameEvents current;

    private void Awake()
    {
        current = this;
    }
    
    public event Action onStart;

    public event Action onDeath;
    

    public void start(){
        if(onStart != null){
            onStart();
        }
    }

    public void death(){
        if(onDeath != null){
            onDeath();
        }
    }
}
