using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandWall : WallBase, IWall
{

    Animator anim;

    
    public GameObject wallShards;
    
    public float crumbleVolume;
    public float grabVolume;
    public float distroyVolume;

    float distructionTime = 4f;

    int crumbleCount = 0;

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
        release();
        Instantiate(wallShards,gameObject.transform.position,gameObject.transform.rotation);
        SoundManager.current.play(SoundManager.AudioType.sandBreak);
        Destroy(gameObject);
    }

    public void crumbleStep(){

        // 0.85
        // 0.7
        // 0.55
        // 0.5

        float pitch = 1f;
        if(crumbleCount <= 2){
            pitch = 1f - (0.15f*(crumbleCount+1));
        }else{
            pitch = 0.55f - 0.05f * (crumbleCount - 2);
        }

        SoundManager.current.play(SoundManager.AudioType.sandCrumble, pitch);
        crumbleCount++;
    }

    public void grab(Head head){
        SoundManager.current.play(SoundManager.AudioType.sandGrab);
        anim.SetTrigger("crumble");
        base.grab(head);
    }
}
