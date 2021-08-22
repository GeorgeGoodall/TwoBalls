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
            TwoBalls2.current.applyVerticalForceToLeftHead(leftJoystick.Vertical);
            TwoBalls2.current.applyHorizontalForceToLeftHead(leftJoystick.Horizontal);
        }

        if(rightJoystick.pointerDown){
            TwoBalls2.current.applyVerticalForceToRightHead(rightJoystick.Vertical);
            TwoBalls2.current.applyHorizontalForceToRightHead(rightJoystick.Horizontal);
        }
    }

    public void reset(){
        leftJoystick.OnPointerUp(null);
        rightJoystick.OnPointerUp(null);
    }
    
}
