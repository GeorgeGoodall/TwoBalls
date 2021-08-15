using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableWall : WallBase, IWall
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    public void grab(Head head){
        SoundManager.current.play(SoundManager.AudioType.grabbableGrab);
    }

    public void release(){

    }
}
