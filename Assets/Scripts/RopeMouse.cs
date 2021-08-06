using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMouse : MonoBehaviour
{    

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private float ropeSegLen = 0.25f;
    private int totalSegments = 40;
    private float lineWidth = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for(int i = 0; i < totalSegments; i++){
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
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
        Vector2 forceGravity = new Vector2(0f,-1f);

        for(int i = 0; i < this.totalSegments; i++){
            RopeSegment segment = this.ropeSegments[i];
            Vector2 velocity = segment.posNow - segment.posOld;
            segment.posOld = segment.posNow;
            segment.posNow += velocity;
            segment.posNow += forceGravity * Time.deltaTime;
            this.ropeSegments[i] = segment;
        }

        // CONSTRAINTS
        for(int i = 0; i < 50; i++){
            this.ApplyConstraints();
        }

    }

    private void ApplyConstraints(){
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

            if(i != 0){
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i+1] = secondSeg;
            }else{
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i+1] = secondSeg;
            }
        }
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


    public struct RopeSegment{
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos){
            this.posNow = pos;
            this.posOld = pos;
        }
    }

}
