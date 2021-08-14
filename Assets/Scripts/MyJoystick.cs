using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyJoystick : Joystick
{

    public bool pointerDown{get; private set;} = false;

    public override void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
        base.OnPointerUp(eventData);
    }
}