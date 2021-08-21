using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : WallBase, IWall
{

 
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    public void grab(Head head){
        head.death();
        TwoHeads.current.death();
    }

    public void release(){
        
    }
}
