using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandWall : MonoBehaviour, IWall
{

    Animator anim;

    Head attachedHead;
    bool headAttached = false;

    float distructionTime = 6f;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.speed = anim.runtimeAnimatorController.animationClips[0].length / distructionTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void crumbleDone(){
        if(headAttached){
            attachedHead.unbite();
        }
        Destroy(gameObject);
    }

    public void grab(Head head){
        anim.SetTrigger("crumble");
        attachedHead = head;
        headAttached = true;
    }

    public void release(){
        headAttached = false;
    }
}
