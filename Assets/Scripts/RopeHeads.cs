using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeHeads : MonoBehaviour
{    
    public LineRenderer lineRenderer {get; private set;}
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private float ropeSegLen = 0.15f;
    private int totalSegments;
    private float lineWidth = 0.1f;

    TwoHeads twoHeads;

    Head head1;
    Head head2;

    public float getRopeLength() => ropeSegLen * ropeSegments.Count;

    // Start is called before the first frame update
    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();

        lineRenderer.useWorldSpace = true;
        
        initialize();
    }

    public void initialize(){
        

        twoHeads = gameObject.transform.parent.GetComponent<TwoHeads>();

        head1 = twoHeads.head1;
        head2 = twoHeads.head2;
        
        totalSegments = (int)(Params.current.ropeLength / ropeSegLen);

        Vector2 ropeStartPoint = head1.position();

        this.ropeSegments.Clear();

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

        // apply forces to head
        applyRopeForcesToHead();

    }

    private void ApplyConstraints(){
        RopeSegment firstSegment = this.ropeSegments[0];

        float dist = (head1.position() - firstSegment.posNow).magnitude;
        float error = Mathf.Abs(dist - this.ropeSegLen);
        Vector2 changeDir = Vector2.zero;

        if(dist > this.ropeSegLen){
            changeDir = (head1.position() - firstSegment.posNow).normalized;
        }else if(dist < this.ropeSegLen){
            changeDir = (firstSegment.posNow - head1.position()).normalized;
        }

        Vector2 changeAmount = changeDir * error;

        
        head1.transform.position -= new Vector3(changeAmount.x * 0.5f,changeAmount.y * 0.5f,0);
        firstSegment.posNow += changeAmount * 0.5f;
        this.ropeSegments[0] = firstSegment;


        for(int i = 0; i < this.totalSegments-1; i++){
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i+1];

            dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            error = Mathf.Abs(dist - this.ropeSegLen);
            changeDir = Vector2.zero;

            if(dist > this.ropeSegLen){
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }else if(dist < this.ropeSegLen){
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            changeAmount = changeDir * error;

         
            firstSeg.posNow -= changeAmount * 0.5f;
            this.ropeSegments[i] = firstSeg;
            secondSeg.posNow += changeAmount * 0.5f;
            this.ropeSegments[i+1] = secondSeg;
            
        }

        RopeSegment lastSegment = this.ropeSegments[this.ropeSegments.Count-1];

        dist = (head2.position() - lastSegment.posNow).magnitude;
        error = Mathf.Abs(dist - this.ropeSegLen);
        changeDir = Vector2.zero;

        if(dist > this.ropeSegLen){
            changeDir = (head2.position() - lastSegment.posNow).normalized;
        }else if(dist < this.ropeSegLen){
            changeDir = (lastSegment.posNow - head2.position()).normalized;
        }

        changeAmount = changeDir * error;

        
        head1.transform.position -= new Vector3(changeAmount.x * 0.5f,changeAmount.y * 0.5f,0);
        lastSegment.posNow += changeAmount * 0.5f;
        this.ropeSegments[this.ropeSegments.Count-1] = lastSegment;
    }


    private void applyRopeForcesToHead(){
        if((this.ropeSegments[0].posNow - this.ropeSegments[this.ropeSegments.Count].posNow).magnitude > this.ropeSegLen*this.ropeSegments.Count){

        }
    }
    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this.totalSegments];
        for(int i = 0; i < this.totalSegments; i++){
            ropePositions[i] = new Vector3(this.ropeSegments[i].posNow.x,this.ropeSegments[i].posNow.y,1);
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

    private List<Vector2> queuedForces = new List<Vector2>();

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
        queuedForces.Add(force);
    }

    public Vector2 popForce(){
        Vector2 forceTmp = Vector2.zero;
        foreach (Vector2 force in queuedForces)
        {
            forceTmp+=force;
        }
        queuedForces.Clear();
        return forceTmp;

    }

    
}