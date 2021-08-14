using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Params : MonoBehaviour {

    public static Params current;
    public float initialWallFallSpeed = 1.5f;

    public Vector3 screenBounds;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
     
    void Start() {

        current = this;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));    
        
    }
}