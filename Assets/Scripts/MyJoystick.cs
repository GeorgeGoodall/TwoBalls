using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyJoystick : Joystick
{

    public bool pointerDown{get; private set;} = false;
    
    protected override void Start()
    {
        base.Start();
        // background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        // background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
        // background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}