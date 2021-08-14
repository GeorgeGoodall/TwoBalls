using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{

    public static CameraAnimation current;

    private Vector3 camStartPos = new Vector3(0,-20,-20);
    private Vector3 camEndPos = new Vector3(0,0,-20);
    private float animationTime = 4f;
    private float elapsedTime = 0f;
    bool playingAnim = false;

    private void Start() {
        camStartPos = new Vector3(0,-Params.current.screenBounds.y*2,-20);
    }

    private void Awake() {
        current=this;
    }
    
    public void playAnimation(){
        elapsedTime = 0f;
        playingAnim = true;
    }   

    void Update()
    {
        if(playingAnim){
            elapsedTime+=Time.deltaTime;

            float coeficient = - (1/((0.5f*elapsedTime/animationTime)+0.5f))+2;

            gameObject.transform.position = camStartPos + (camEndPos-camStartPos)*coeficient;
            if(elapsedTime >= animationTime){
                playingAnim = false;
                gameObject.transform.position = camEndPos;
            }
        }
    }
}
