using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{

    public MyJoystick leftJoystick;
    public MyJoystick rightJoystick;

    public static JoystickControl current;

    // Start is called before the first frame update
    void Start()
    {
        leftJoystick = gameObject.transform.Find("Left Joystick").GetComponent<MyJoystick>();
        rightJoystick = gameObject.transform.Find("Right Joystick").GetComponent<MyJoystick>();

        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(leftJoystick.pointerDown){
            TwoHeads.current.setLeftGrab(false);

            if(leftJoystick.Direction.magnitude > 0.99f){
                TwoHeads.current.releaseLeftHead();
                TwoHeads.current.applyVerticalForceToLeftHead(leftJoystick.Vertical);
                TwoHeads.current.applyHorizontalForceToLeftHead(leftJoystick.Horizontal);
            }else{
                TwoHeads.current.moveLeftHeadTo(leftJoystick.Direction);
            }

            //TwoHeads.current.applyRadialForceToLeftHead(leftJoystick.Horizontal);
        }
        else{
            TwoHeads.current.setLeftGrab(true);
            TwoHeads.current.releaseLeftHead();
        }

        if(rightJoystick.pointerDown){
            TwoHeads.current.setRightGrab(false);

            if(rightJoystick.Direction.magnitude > 0.99f){
                TwoHeads.current.releaseRightHead();
                TwoHeads.current.applyVerticalForceToRightHead(rightJoystick.Vertical);
                TwoHeads.current.applyHorizontalForceToRightHead(rightJoystick.Horizontal);
            }else{
                TwoHeads.current.moveRightHeadTo(rightJoystick.Direction);
            }

            //TwoHeads.current.applyRadialForceToRightHead(rightJoystick.Horizontal);
        }
        else{
            TwoHeads.current.setRightGrab(true);
            TwoHeads.current.releaseRightHead();
        }
    }

    public void reset(){
        leftJoystick.OnPointerUp(null);
        rightJoystick.OnPointerUp(null);
    }
    
}
