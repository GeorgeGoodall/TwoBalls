using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHeads : MonoBehaviour
{

    public float ropeLength {get; private set;} = 3f;

    private Head head1;
    private Head head2;

    private float movementForce = 6f;

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
    }

    bool LeftHeadAtStart = true;
    bool rightHeadAtStart = true;

    bool calledStart = false;

    void movement(){
        if(Input.GetKeyDown(KeyCode.Q)){
            if(LeftHeadAtStart){
                LeftHeadAtStart = false;
            }
            head1.toggleBite();
        }
        if(Input.GetKeyDown(KeyCode.E)){
            if(rightHeadAtStart){
                rightHeadAtStart = false;
            }
            head2.toggleBite();
        }

        if(!calledStart && !rightHeadAtStart && !LeftHeadAtStart){
            calledStart = true;
            GameEvents.current.start();
        }
        
        if(Input.GetKey(KeyCode.LeftArrow)){
            if(head2.locked){
                head1.addForce(getClockwiseVector(head1.position(),head2.position()) * movementForce,ForceMode2D.Impulse);
            }
            if(head1.locked){
                head2.addForce(getClockwiseVector(head2.position(),head1.position()) * movementForce,ForceMode2D.Impulse);
            }
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            if(head2.locked){
                head1.addForce(getAnticlockwiseVector(head1.position(),head2.position()) * movementForce,ForceMode2D.Impulse);
            }
            if(head1.locked){
                head2.addForce(getAnticlockwiseVector(head2.position(),head1.position()) * movementForce,ForceMode2D.Impulse);
            }
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