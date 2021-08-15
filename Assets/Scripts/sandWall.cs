using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandWall : WallBase, IWall
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

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void crumbleDone(){
        if(headAttached){
            attachedHead.setBite(false);
            attachedHead.setCanGrab(false);
        }
        Destroy(gameObject);
    }

    public void crumbleStep(){
        SoundManager.current.play(SoundManager.AudioType.sandCrumble);
    }

    public void grab(Head head){
        SoundManager.current.play(SoundManager.AudioType.sandGrab);
        anim.SetTrigger("crumble");
        attachedHead = head;
        headAttached = true;
    }

    public void release(){
        headAttached = false;
    }
}
