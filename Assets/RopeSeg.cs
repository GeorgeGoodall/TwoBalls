using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSeg : MonoBehaviour
{

    public GameObject above, below;

    // Start is called before the first frame update
    void Start()
    {
        above = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeSeg aboveSegment = above.GetComponent<RopeSeg>();
        if(above != null){
            aboveSegment.below = gameObject;
        }
        //     float spriteBottom = above.GetComponent<SpriteRenderer>().bounds.size.y;
        //     GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0,spriteBottom*-1);
        // }else{
        //     GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0,0);
        // }
    }

    public float getSpriteBottom(){ 
        return GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
