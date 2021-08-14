using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWalls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float height;

    float verticalSpeed = 0f;

    // Update is called once per frame
    void LateUpdate()
    {
        // if two heads becomes higher than an ammount start moving the game up

        float currentHeight = (TwoHeads.current.head1.position().y + TwoHeads.current.head2.position().y)/2;



        if(currentHeight > 0 || verticalSpeed > 0){
            // lerp between 0 and 0.02 based on height
            verticalSpeed = Params.current.initialWallFallSpeed * Mathf.Pow(currentHeight/(Params.current.screenBounds.y * 0.7f),2);

            float forcedMinSpeed =  (Mathf.Abs(gameObject.transform.position.y) / (height - Params.current.screenBounds.y - Params.current.screenBounds.y)) * Params.current.initialWallFallSpeed;

            verticalSpeed = Mathf.Min(Mathf.Max(verticalSpeed,forcedMinSpeed),Params.current.initialWallFallSpeed) * Time.deltaTime;
        }
        if(verticalSpeed > 0){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-verticalSpeed,gameObject.transform.position.z);
        }

        if(gameObject.transform.position.y < -height + (Params.current.screenBounds.y * 2)){
            WallSpawner.current.running = true;
        }

        if(gameObject.transform.position.y < -height - (Params.current.screenBounds.y)){
            Destroy(gameObject);
        }
    }
}
