using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeHeads : MonoBehaviour
{    
    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private float ropeSegLen = 0.125f;
    private int totalSegments;
    private float lineWidth = 0.1f;

    TwoHeads twoHeads;

    Head head1;
    Head head2;

    // Start is called before the first frame update
    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        
        head1 = gameObject.transform.parent.transform.Find("head1").GetComponent<Head>();
        head2 = gameObject.transform.parent.transform.Find("head2").GetComponent<Head>();

        twoHeads = gameObject.transform.parent.GetComponent<TwoHeads>();


        totalSegments = (int)(twoHeads.ropeLength / ropeSegLen);

        Vector2 ropeStartPoint = head1.position();

        for(int i = 0; i < totalSegments; i++){
            if(i == 0){
                this.ropeSegments.Add(new RopeSegment(ropeStartPoint,4,head1));
            }else if(i == totalSegments-1){
                this.ropeSegments.Add(new RopeSegment(ropeStartPoint,4,head2));
            }else{
                this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            }
            ropeStartPoint.y -= ropeSegLen;
        }
    }


    // Update is called once per frame
    void Update()
    {
        this.DrawRope();
    }

    void FixedUpdate()
    {
        simulate();    
    }

    private void simulate(){

        // SIMULATION
        Vector2 forceGravity = new Vector2(0f,-0.2f);

        for(int i = 0; i < this.totalSegments; i++){
            RopeSegment segment = this.ropeSegments[i];
            Vector2 velocity = segment.posNow - segment.posOld;
            segment.posOld = segment.posNow;
            segment.posNow += velocity;
            segment.posNow += forceGravity * Time.deltaTime;
            segment.posNow += segment.popForce() * Time.deltaTime;
            this.ropeSegments[i] = segment;
        }

        // CONSTRAINTS
        for(int i = 0; i < 50; i++){
            this.ApplyConstraints();
        }

    }

    private void ApplyConstraints(){
        RopeSegment firstSegment = this.ropeSegments[0];
        if(!head1.setPositionFromRope(firstSegment.posNow)){
            firstSegment.posNow = head1.position();
        }
        this.ropeSegments[0] = firstSegment;


        for(int i = 0; i < this.totalSegments-1; i++){
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i+1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if(dist > this.ropeSegLen){
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }else if(dist < this.ropeSegLen){
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;

            // if(i == 0){
            //     secondSeg.posNow += changeAmount;
            //     this.ropeSegments[i+1] = secondSeg;
            // }
            // else if(i == this.totalSegments-2){
            //     firstSeg.posNow -= changeAmount * 0.5f;
            //     this.ropeSegments[i] = firstSeg;
                
            // }
            // else{
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i+1] = secondSeg;
            // }
        }

        
        RopeSegment lastSegment = this.ropeSegments[this.totalSegments-1];
        if(!head2.setPositionFromRope(lastSegment.posNow)){
            lastSegment.posNow = head2.position();
        }
        this.ropeSegments[this.totalSegments-1] = lastSegment;
    }

    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this.totalSegments];
        for(int i = 0; i < this.totalSegments; i++){
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);

    }


    
}

public class RopeSegment{
    public Vector2 posNow;
    public Vector2 posOld;

    public float weight {get; private set;}

    public Head? attached;

    private Vector2 queuedForce = Vector2.zero;

    public RopeSegment(Vector2 pos, float _weight = 1f, Head? _attached = null){
        this.posNow = pos;
        this.posOld = pos;
        weight = _weight;
        attached = _attached;

        if(_attached != null){
            _attached.attachRopeSegment(this);
        }
    }

    public void queueForce(Vector2 force){
        queuedForce = force;
    }

    public Vector2 popForce(){
        Vector2 forceTmp = queuedForce;
        queuedForce = Vector2.zero;
        return forceTmp;

    }

    
}