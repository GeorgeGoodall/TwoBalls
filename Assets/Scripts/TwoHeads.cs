using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHeads : MonoBehaviour
{

    public static TwoHeads current;

    public Head head1 {get; private set;}
    public Head head2 {get; private set;}

    private float movementForceRadial = 2f;
    private float movementForceVertical = 12f;

    private Vector2 head1Start;
    private Vector2 head2Start;

    public RopeHeads rope {get; private set;}

    public bool canMove = true;
    public bool dead = false;

    private void Awake() {
        current = this;
    }

    public void applyVerticalForceToLeftHead(float force){
        if(head2.locked){
            head1.addForce(Vector2.up * force * movementForceVertical, ForceMode2D.Impulse);
            LeftHeadAtStart = false;
        }
    }

    public void applyVerticalForceToRightHead(float force){
        if(head1.locked){
            head2.addForce(Vector2.up * force * movementForceVertical, ForceMode2D.Impulse);
            rightHeadAtStart = false;
        }
    }

    public void applyHorizontalForceToLeftHead(float force){
        if(head2.locked){
            head1.addForce(Vector2.right * force * movementForceVertical, ForceMode2D.Impulse);
            LeftHeadAtStart = false;
        }
    }

    public void applyHorizontalForceToRightHead(float force){
        if(head1.locked){
            head2.addForce(Vector2.right * force * movementForceVertical, ForceMode2D.Impulse);
            rightHeadAtStart = false;
        }
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

    

    bool LeftHeadAtStart = true;
    bool rightHeadAtStart = true;

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

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            movement();
        }

        if((head1.transform.position.y < -Params.current.screenBounds.y-5 || head2.transform.position.y < -Params.current.screenBounds.y-5) && !dead){
            GameEvents.current.death();
            dead = true;
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
        dead = false;
        canMove = true;
    }

    public float height(){
        return (head1.position().y + head2.position().y)/2;
    }

    public float getHighestBall() => Mathf.Max(head1.position().y,head2.position().y);
    public float getCurrentBlock() => WallSpawner.current.getBlockHeightFromWorldPosition(getHighestBall());
    

    public void updateHeads(GameObject left, GameObject right){  
        head1 = left.GetComponent<Head>();
        head2 = right.GetComponent<Head>();
    }
}
