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
            TwoHeads.current.applyInputLeft(leftJoystick.Direction);
        }else{
            TwoHeads.current.setLeftGrab(true);
            TwoHeads.current.releaseLeftHead();
        }

        if(rightJoystick.pointerDown){
            TwoHeads.current.applyInputRight(rightJoystick.Direction);
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
