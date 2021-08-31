using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

    public Rigidbody2D ball1;
    public Rigidbody2D ball2;
    public GameObject[] prefabRopeSegs;
    public GameObject[] ropeSegments{get; private set;}
    public int numLinks = 30;

    private float ropeLimit = 180;

    public LineRenderer lr;

    
    TwoHeads twoHeads;

    public float segmentSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void initialise(){
        twoHeads = gameObject.transform.parent.GetComponent<TwoHeads>();

        ball1 = twoHeads.head1.GetComponent<Rigidbody2D>();
        ball2 = twoHeads.head2.GetComponent<Rigidbody2D>();
        
        Physics2D.IgnoreLayerCollision(7,7);
        Physics2D.IgnoreLayerCollision(7,8);
        Physics2D.IgnoreLayerCollision(8,8);
        GenerateRope();

        lr = gameObject.GetComponent<LineRenderer>();

        TwoHeads.current.rope = this;
    }

    void GenerateRope(){
        segmentSize = prefabRopeSegs[0].GetComponent<SpriteRenderer>().bounds.size.y;
        numLinks = (int)Mathf.Ceil(TwoHeads.current.ropeLength / segmentSize);

        Rigidbody2D prevBody = ball1;
        ropeSegments = new GameObject[numLinks];
        float ropeLength = 0;
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
                hj.connectedAnchor = new Vector2(0,spriteBottom*-1);
                ropeLength+=spriteBottom;
            }
            // JointAngleLimits2D limits = hj.limits;
            // limits.max = ropeLimit;
            // limits.min = -ropeLimit;
            // hj.limits = limits;

            prevBody = newSeg.GetComponent<Rigidbody2D>();

            ropeSegments[i] = newSeg;
        }
        //ball2.transform.position = transform.position;
        HingeJoint2D hjball = ball2.gameObject.GetComponent<HingeJoint2D>();
        if(hjball == null){
            hjball = ball2.gameObject.AddComponent<HingeJoint2D>();
        }
        hjball.autoConfigureConnectedAnchor = false;
        hjball.connectedAnchor = new Vector2(0,prevBody.gameObject.GetComponent<RopeSeg>().getSpriteBottom()*-1);
        hjball.connectedBody = prevBody;

        DistanceJoint2D dj = ball1.gameObject.AddComponent<DistanceJoint2D>();
        dj.autoConfigureDistance = false;
        dj.connectedBody = ball2;
        dj.maxDistanceOnly = true;
        dj.distance = segmentSize*numLinks; // this should be set programatically based on the rope length
    

    }

    public GameObject lastSegment(){
        return ropeSegments[ropeSegments.Length-1];
    }

    void Update()
    {
        Vector3[] positions = new Vector3[numLinks+1];

        for (int i = 0; i < numLinks; i++)
        {   
            positions[i] = ropeSegments[i].transform.position;
        }

        positions[numLinks] = ball2.position;

        lr.positionCount = positions.Length;

        lr.SetPositions(positions);
    }
    
}