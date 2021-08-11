using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickControl : MonoBehaviour
{

    public MyJoystick leftJoystick;
    public MyJoystick rightJoystick;

    // Start is called before the first frame update
    void Start()
    {
        leftJoystick = gameObject.transform.Find("Left Joystick").GetComponent<MyJoystick>();
        rightJoystick = gameObject.transform.Find("Right Joystick").GetComponent<MyJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        if(leftJoystick.pointerDown){
            TwoHeads.current.setLeftGrab(false);
            // TwoHeads.current.applyRadialForceToLeftHead(leftJoystick.Horizontal);
            TwoHeads.current.applyVerticalForceToLeftHead(leftJoystick.Vertical);
            TwoHeads.current.applyHorizontalForceToLeftHead(leftJoystick.Horizontal);
            Debug.Log(leftJoystick.Horizontal);
        }
        else{
            TwoHeads.current.setLeftGrab(true);
        }

        if(rightJoystick.pointerDown){
            TwoHeads.current.setRightGrab(false);
            // TwoHeads.current.applyRadialForceToRightHead(rightJoystick.Horizontal);
            TwoHeads.current.applyVerticalForceToRightHead(rightJoystick.Vertical);
            TwoHeads.current.applyHorizontalForceToRightHead(rightJoystick.Horizontal);
        }
        else{
            TwoHeads.current.setRightGrab(true);
        }
        // Update is called once per frame
    }
    
}
