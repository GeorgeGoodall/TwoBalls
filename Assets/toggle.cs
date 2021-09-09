using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Events;

public class toggle : MonoBehaviour
{
    bool toRight = false;
    Animator anim;

    public UnityEvent toRightCallback;
    public UnityEvent toLeftCallback;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        anim.SetBool("isRight",toRight);
    }

    public void toggleButton(){
        toRight = !toRight;
        anim.SetBool("isRight",toRight);

        if(toRight){
            toRightCallback.Invoke();
        }else{
            toLeftCallback.Invoke();
        }
    }

    public void toggleToLeft(){
        toRight = false;
        anim.SetBool("isRight",toRight);
        toLeftCallback.Invoke();
    }

    public void toggleToRight(){
        toRight = true;
        anim.SetBool("isRight",toRight);
        toRightCallback.Invoke();
    }

    
}
