using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{

    public MyJoystick leftJoystick;
    public MyJoystick rightJoystick;

    public static JoystickControl current;

    private Vector2 leftLastPosition = Vector2.zero;
    private Vector2 rightLastPosition = Vector2.zero;

    private float addForceThreshold = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        leftJoystick = gameObject.transform.Find("Left Joystick").GetComponent<MyJoystick>();
        rightJoystick = gameObject.transform.Find("Right Joystick").GetComponent<MyJoystick>();

        current = this;
    }

      void Update()
    {
        if(leftJoystick.pointerDown){
            Debug.Log("LeftDown");
            TwoHeads.current.setLeftGrab(false);

            //Vector2 deltaJoyLeft = leftJoystick.Direction - leftLastPosition;
            Vector2 deltaJoyLeft = leftJoystick.Direction;

            if(deltaJoyLeft.magnitude > addForceThreshold){
                TwoHeads.current.releaseLeftHead();
                TwoHeads.current.applyVerticalForceToLeftHead(leftJoystick.Vertical);
                TwoHeads.current.applyHorizontalForceToLeftHead(leftJoystick.Horizontal);
            }else{
                TwoHeads.current.moveLeftHeadTo(leftJoystick.Direction / addForceThreshold);
            }


            //TwoHeads.current.applyRadialForceToLeftHead(leftJoystick.Horizontal);
        }
        else{
            TwoHeads.current.setLeftGrab(true);
            TwoHeads.current.releaseLeftHead();
        }

        if(rightJoystick.pointerDown){
            Debug.Log("RightDown");
            //Vector2 deltaJoyRight = rightJoystick.Direction - rightLastPosition;
            Vector2 deltaJoyRight = rightJoystick.Direction;
            TwoHeads.current.setRightGrab(false);

            if(deltaJoyRight.magnitude > addForceThreshold){
                TwoHeads.current.releaseRightHead();
                TwoHeads.current.applyVerticalForceToRightHead(rightJoystick.Vertical);
                TwoHeads.current.applyHorizontalForceToRightHead(rightJoystick.Horizontal);
            }else{
                TwoHeads.current.moveRightHeadTo(rightJoystick.Direction / addForceThreshold);
            }


            //TwoHeads.current.applyRadialForceToRightHead(rightJoystick.Horizontal);
        }
        else{
            TwoHeads.current.setRightGrab(true);
            TwoHeads.current.releaseRightHead();
        }
        
        leftLastPosition = leftJoystick.Direction;
        rightLastPosition = rightJoystick.Direction;
    }

    public void reset(){
        leftJoystick.OnPointerUp(null);
        rightJoystick.OnPointerUp(null);
    }
    
}
