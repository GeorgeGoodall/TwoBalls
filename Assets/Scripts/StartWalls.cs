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
        float currentHeight = 0;
        if(TwoHeads.current.head1 != null && TwoHeads.current.head2 != null){
            currentHeight = (TwoHeads.current.head1.position().y + TwoHeads.current.head2.position().y)/2;   
        }



        if(currentHeight > 0 || verticalSpeed > 0){
            verticalSpeed = MoveDown.currentSpeed();
            float forcedMinSpeed =  (Mathf.Abs(gameObject.transform.position.y) / (height - Params.current.screenBounds.y - Params.current.screenBounds.y)) * (MoveDown.speed);
            if(forcedMinSpeed < MoveDown.speed){
                verticalSpeed = Mathf.Max(verticalSpeed,forcedMinSpeed) * Time.deltaTime;
            }else{
                verticalSpeed = verticalSpeed * Time.deltaTime;
            }
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
