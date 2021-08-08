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

    
    // ------------------------------------------------------
    // CONTROLS

    public event Action onLeftPress;
    public event Action onRightPress;
    public event Action onAction1Press;
    public event Action onAction2Press;

    public void leftPress(){
        if(onLeftPress != null){
            onLeftPress();
        }
    }

    public void rightPress(){
        if(onRightPress != null){
            onRightPress();
        }
    }

    public void action1Press(){
        if(onAction1Press != null){
            onAction1Press();
        }
    }

    public void action2Press(){
        if(onAction2Press != null){
            onAction2Press();
        }
    }

}
