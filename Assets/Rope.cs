using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

    public Rigidbody2D ball1;
    public Rigidbody2D ball2;
    public GameObject[] prefabRopeSegs;
    public int numLinks = 30;

    private float ropeLimit = 180;

    TwoBalls2 twoHeads;

    Rigidbody2D head1;
    Rigidbody2D head2;

    float segmentSize;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(7,7);
        Physics2D.IgnoreLayerCollision(7,8);
        Physics2D.IgnoreLayerCollision(8,8);
        GenerateRope();

        twoHeads = gameObject.transform.parent.GetComponent<TwoBalls2>();

        head1 = twoHeads.leftHeadRb;
        head2 = twoHeads.rightHeadRb;

        segmentSize = prefabRopeSegs[0].GetComponent<SpriteRenderer>().bounds.size.y / prefabRopeSegs[0].transform.lossyScale.y;
        
    }

    void GenerateRope(){
        Rigidbody2D prevBody = ball1;
        for (int i = 0; i < numLinks; i++)
        {
            int index = Random.Range(0,prefabRopeSegs.Length);
            GameObject newSeg = Instantiate(prefabRopeSegs[index]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBody;
            if(i > 0){
                float spriteBottom = prevBody.GetComponent<SpriteRenderer>().bounds.size.y / prevBody.transform.lossyScale.y;
                segmentSize = spriteBottom;
                hj.connectedAnchor = new Vector2(0,spriteBottom*-1);
            }
            // JointAngleLimits2D limits = hj.limits;
            // limits.max = ropeLimit;
            // limits.min = -ropeLimit;
            // hj.limits = limits;

            prevBody = newSeg.GetComponent<Rigidbody2D>();
        }
        ball2.transform.position = transform.position;
        HingeJoint2D hjball = ball2.GetComponent<HingeJoint2D>();
        hjball.connectedAnchor = new Vector2(0,prevBody.gameObject.GetComponent<RopeSeg>().getSpriteBottom()*-1);
        hjball.connectedBody = prevBody;
    }
}