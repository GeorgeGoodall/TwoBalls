using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHeads : MonoBehaviour
{

    public static TwoHeads current;

    public float ropeLength {get; private set;} = 5f;

    private Head head1;
    private Head head2;

    private float movementForceRadial = 1f;
    private float movementForceVertical = 1f;

    private Vector2 head1Start;
    private Vector2 head2Start;

    private RopeHeads rope;

    // Start is called before the first frame update
    void Start()
    {
        
        head1 = gameObject.transform.Find("head1").GetComponent<Head>();
        head2 = gameObject.transform.Find("head2").GetComponent<Head>();
        head1Start = head1.position();
        head2Start = head2.position();
        rope = gameObject.transform.Find("Rope").GetComponent<RopeHeads>();
        GameEvents.current.onDeath += reset;
        assignControllerEvents();
    }

    private void Awake() {
        current = this;
    }

    void assignControllerEvents(){
        
        GameEvents.current.onLeftPress += leftPress;
        GameEvents.current.onRightPress += rightPress;
        GameEvents.current.onAction1Press += actionOnePress;
        GameEvents.current.onAction2Press += actionTwoPress;
    }

    public void applyVerticalForceToLeftHead(float force){
        head1.addForce(Vector2.up * force * movementForceVertical, ForceMode2D.Impulse);
        LeftHeadAtStart = false;
    }

    public void applyVerticalForceToRightHead(float force){
        head2.addForce(Vector2.up * force * movementForceVertical, ForceMode2D.Impulse);
        rightHeadAtStart = false;
    }

    public void applyHorizontalForceToLeftHead(float force){
        head1.addForce(Vector2.right * force * movementForceVertical, ForceMode2D.Impulse);
        LeftHeadAtStart = false;
    }

    public void applyHorizontalForceToRightHead(float force){
        head2.addForce(Vector2.right * force * movementForceVertical, ForceMode2D.Impulse);
        rightHeadAtStart = false;
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

    public void leftPress(){
        LeftHeadAtStart = false;
        if(head2.locked){
            head1.addForce(getClockwiseVector(head1.position(),head2.position()) * movementForceRadial,ForceMode2D.Impulse);
        }
        if(head1.locked){
            head2.addForce(getClockwiseVector(head2.position(),head1.position()) * movementForceRadial,ForceMode2D.Impulse);
        }
    }

    public void rightPress(){
        rightHeadAtStart = false;
        if(head2.locked){
            head1.addForce(getAnticlockwiseVector(head1.position(),head2.position()) * movementForceRadial,ForceMode2D.Impulse);
        }
        if(head1.locked){
            head2.addForce(getAnticlockwiseVector(head2.position(),head1.position()) * movementForceRadial,ForceMode2D.Impulse);
        }
    }

    public void actionOnePress(){
        if(LeftHeadAtStart){
            LeftHeadAtStart = false;
        }
        head1.toggleBite();
    }

    public void actionTwoPress(){
        if(rightHeadAtStart){
            rightHeadAtStart = false;
        }
        head2.toggleBite();
    }

    public void setLeftGrab(bool grab){
        head1.setBite(grab);
    }

    public void setRightGrab(bool grab){
        head2.setBite(grab);
    }

    bool LeftHeadAtStart = true;
    bool rightHeadAtStart = true;

    bool calledStart = false;

    void movement(){
        // if(Input.GetKeyDown(KeyCode.Q)){
        //     GameEvents.current.action1Press();
        // }
        // if(Input.GetKeyDown(KeyCode.E)){
        //    GameEvents.current.action2Press();
        // }

        if(!calledStart && !rightHeadAtStart && !LeftHeadAtStart){
            calledStart = true;
            GameEvents.current.start();
        }
        
        if(Input.GetKey(KeyCode.LeftArrow)){
            GameEvents.current.leftPress();
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            GameEvents.current.rightPress();
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


    void limit(){
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();

        if(head1.transform.position.y < -10){
            GameEvents.current.death();
        }
    }

    void reset(){
        head1.gameObject.transform.position = head1Start;
        head2.gameObject.transform.position = head2Start;
        head1.reset();
        head2.reset();
        LeftHeadAtStart = true;
        rightHeadAtStart = true;
        calledStart = false;
    }
}
