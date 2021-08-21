using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchButtons : MonoBehaviour
{

    bool leftButtonPress = false;
    bool rightButtonPress = false;
    public void leftButtonDown() => leftButtonPress = true;
    public void leftButtonUp() => leftButtonPress = false;
    public void rightButtonDown() => rightButtonPress = true;
    public void rightButtonUp() => rightButtonPress = false;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(leftButtonPress);
        if(leftButtonPress){
            GameEvents.current.leftPress();
        }

        if(rightButtonPress){
            GameEvents.current.rightPress();
        }
    }
}
