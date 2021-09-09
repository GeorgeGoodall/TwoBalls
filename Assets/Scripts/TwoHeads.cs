using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHeads : MonoBehaviour
{

    public static TwoHeads current;

    public Head head1 {get; private set;}
    public Head head2 {get; private set;}

    private float movementForceRadial = 2f;
    private float movementForceVertical = 6f;
    private float notGrabbedMultipleHorazontal = 0.05f;
    private float notGrabbedMultiplevertical = 0f;

    private Vector2 head1Start, head2Start;

    //public RopeHeads rope {get; private set;}

    public bool canMove = true;

    public float ropeLength = 4.7f;

    public Rope rope;

    private void Awake() {
        current = this;
        Rope rope = gameObject.GetComponent<Rope>();
    }

    public void moveLeftHeadTo(Vector2 position){
        if(head2.locked){
            head1.addForceTo(head2.position() + position * rope.segmentSize * rope.numLinks);
            LeftHeadAtStart = false;
        }else{
            head1.stopMovingToPosition();
        }
    }

    public void moveRightHeadTo(Vector2 position){
        if(head1.locked){
            head2.addForceTo(head1.position() + position * rope.segmentSize * rope.numLinks);
            rightHeadAtStart = false;
        }else{
            head2.stopMovingToPosition();
        }
    }

    private float addForceThreshold = 0.5f;
    public void applyInputLeft(Vector2 deltaJoystick){

        TwoHeads.current.setLeftGrab(false);
        TwoHeads.current.releaseLeftHead();

        if(head2.locked){
            if(deltaJoystick.magnitude > addForceThreshold){
                head1.addForce(Vector2.up * deltaJoystick.y * movementForceVertical, ForceMode2D.Impulse);
                head1.addForce(Vector2.right * deltaJoystick.x * movementForceVertical, ForceMode2D.Impulse);
                LeftHeadAtStart = false;
            }else{
                TwoHeads.current.moveLeftHeadTo(deltaJoystick / addForceThreshold);
            }
        }else{
            // head1.addForce(Vector2.up * deltaJoystick.y * movementForceVertical * notGrabbedMultiplevertical, ForceMode2D.Impulse);
            head1.addForce(Vector2.right * deltaJoystick.x * movementForceVertical * notGrabbedMultipleHorazontal, ForceMode2D.Impulse);
            LeftHeadAtStart = false;
        }
    }


    public void applyInputRight(Vector2 deltaJoystick){

        TwoHeads.current.setRightGrab(false);
        TwoHeads.current.releaseRightHead();

        if(head1.locked){
            if(deltaJoystick.magnitude > addForceThreshold){
                head2.addForce(Vector2.up * deltaJoystick.y * movementForceVertical, ForceMode2D.Impulse);
                head2.addForce(Vector2.right * deltaJoystick.x * movementForceVertical, ForceMode2D.Impulse);
                rightHeadAtStart = false;
            }else{
                TwoHeads.current.moveRightHeadTo(deltaJoystick / addForceThreshold);
            }
        }else{
            // head2.addForce(Vector2.up * deltaJoystick.y * movementForceVertical * notGrabbedMultiplevertical, ForceMode2D.Impulse);
            head2.addForce(Vector2.right * deltaJoystick.x * movementForceVertical * notGrabbedMultipleHorazontal, ForceMode2D.Impulse);
            rightHeadAtStart = false;
        }
    }

    public void releaseLeftHead(){
        head1.stopMovingToPosition();
    }

    public void releaseRightHead(){
        head2.stopMovingToPosition();
    }

   
    public void applyRadialForceToLeftHead(float force){
        LeftHeadAtStart = false;
        if(force > 0){
            head1.addForce(getAnticlockwiseVector(head1.position(),head2.position()) * movementForceRadial * force,ForceMode2D.Impulse);
        }else{
            head1.addForce(getClockwiseVector(head1.position(),head2.position()) * movementForceRadial * force * -1,ForceMode2D.Impulse);
        }
    }

    public void applyRadialForceToRightHead(float force){
        rightHeadAtStart = false;
        if(force > 0){
            head2.addForce(getAnticlockwiseVector(head2.position(),head1.position()) * movementForceRadial * force,ForceMode2D.Impulse);
        }else{
            head2.addForce(getClockwiseVector(head2.position(),head1.position()) * movementForceRadial * force * -1,ForceMode2D.Impulse);
        }
    }

    public void setLeftGrab(bool grab){
        if(canMove){
            head1.setBite(grab);
        }
    }

    public void setRightGrab(bool grab){
        if(canMove){
            head2.setBite(grab);
        }
    }

    

    public bool LeftHeadAtStart = true;
    public bool rightHeadAtStart = true;

    bool calledStart = false;

    void movement(){
        if(!calledStart && !rightHeadAtStart && !LeftHeadAtStart){
            calledStart = true;
            GameEvents.current.start();
        }
    }

    Vector2 getClockwiseVector(Vector2 a, Vector2 b){
        Vector2 delta = b - a;
        Vector2 clockwise = rotate(delta,135).normalized;
        return clockwise;
    }

    Vector2 getAnticlockwiseVector(Vector2 a, Vector2 b){
        Vector2 delta = b - a;
        Vector2 clockwise = rotate(delta,-135).normalized;
        return clockwise;
    }

    Vector2 rotate(Vector2 v, float degrees) {
         float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
         float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
         float tx = v.x;
         float ty = v.y;
         v.x = (cos * tx) - (sin * ty);
         v.y = (sin * tx) + (cos * ty);
         return v;
     }

    public void death(){
        canMove = false;
        head1.setBite(false);
        head2.setBite(false);
    }

    private float aboveScreenApplyDownForceOf = 64f;

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            movement();
        }
    }

    public void reset(){
        head1.gameObject.transform.position = BallSpawner.current.getBallSpawnPosition(true);
        head2.gameObject.transform.position = BallSpawner.current.getBallSpawnPosition(false);
        head1.reset();
        head2.reset();
        LeftHeadAtStart = true;
        rightHeadAtStart = true;
        calledStart = false;
        canMove = true;
    }

    public float height() => (head1.position().y + head2.position().y)/2;
    public float getHighestBall() => Mathf.Max(head1.position().y,head2.position().y);
    public float getLowestBall() => Mathf.Min(head1.position().y,head2.position().y);
    public float getCurrentBlock() => WallSpawner.current.getBlockHeightFromWorldPosition(getHighestBall());
    

    public void updateHeads(GameObject left, GameObject right){  
        head1 = left.GetComponent<Head>();
        head2 = right.GetComponent<Head>();
    }
}
