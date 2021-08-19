using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour{

    public static float speed = 1.5f;

    private float elapsedTime;

    private static float percentOfScreenToAddAdditional = 0f; // above 70% of the screen, the speed the blocks fall starts to increace

    private static float maxDeltaHeight = 0f;

    public static float currentSpeed(){
        return speed + getAdditionalSpeed();
    }

    public static float getAdditionalSpeed(){
        try{
            float currentHeight = 0;
            float speedAdditional = 0;
            if(TwoHeads.current.head1 != null && TwoHeads.current.head2 != null){
                currentHeight = Mathf.Max(TwoHeads.current.head1.position().y, TwoHeads.current.head2.position().y);

                float deltaHeight = (currentHeight - (Params.current.screenBounds.y * percentOfScreenToAddAdditional)) / (Params.current.screenBounds.y * (1f-percentOfScreenToAddAdditional));

                deltaHeight = Mathf.Min(1,deltaHeight);

                if(deltaHeight > 0){
                    speedAdditional = (-1/((0.75f*deltaHeight)-1))-1;
                }else{
                    speedAdditional = 0;
                }
                
            }
            return speedAdditional;
        }catch(System.Exception e){
            Debug.LogError(e);
            return 0;
        }
    }

    void LateUpdate()
    {
        transform.position += Vector3.down * (speed + getAdditionalSpeed()) * Time.deltaTime;
    }


}