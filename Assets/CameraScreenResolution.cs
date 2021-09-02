using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenResolution : MonoBehaviour
{
    float lastWidth;
    float width;
    float currentWidth;
    float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currentWidth = 5;
        width = 5;
        lastWidth = 5;
    }


    public void setWidth(float newWidth){
        if(newWidth < 5){
            newWidth = 5;
        }

        newWidth += 1;

        width = newWidth;
        elapsedTime = 0f;
        Params.current.updateScreenBounds(new Vector3(width,width / Camera.main.aspect,0));
    }

    float elapsedTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(width != currentWidth){
            elapsedTime+=Time.deltaTime;

            currentWidth = lastWidth + (width - lastWidth) * (elapsedTime / transitionTime);
            if(elapsedTime >= transitionTime){
                lastWidth = width;
                currentWidth = width;
            }

        }
        float deltaOrthographicSize = (currentWidth / Camera.main.aspect) - Camera.main.orthographicSize;
        Camera.main.orthographicSize = currentWidth / Camera.main.aspect;

    }
}
