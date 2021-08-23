using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Params : MonoBehaviour {

    public static Params current;
    public float initialWallFallSpeed = 1.5f;
    public Vector3 screenBounds;
    public float ropeLength {get; private set;} = 5f;

    public int lastScore = 0;
    public int bestScore = 0;
     
    void Start() {

        current = this;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //screenBounds = new Vector3(Camera.main.orthographicSize,Camera.main.orthographicSize*Camera.main.aspect,0);  
    }

    public void updateScreenBounds(){
        screenBounds = new Vector3(Camera.main.orthographicSize,Camera.main.orthographicSize*Camera.main.aspect,0);
    }
    public void updateScreenBounds(Vector3 newSize){
        screenBounds = newSize;
    }

   

}