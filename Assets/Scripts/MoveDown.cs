using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour{

    [HideInInspector]
    public static float speed = 0f;

    private float elapsedTime;

    private static float percentOfScreenToAddAdditional = 0.5f;

    private static float maxDeltaHeight = 0f;

    private static float additionalSpeedModifier = 8;

    private static float additionalScoreSpeedModifier = 5000f;

    private Rigidbody2D rb;
    public int row;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public static float currentSpeed(){
        return speed + getAdditionalSpeed() * additionalSpeedModifier + getAdditionalSpeedFromScore();
    }

    public static float getAdditionalSpeed(){
        try{
            float currentHeight = 0;
            float speedAdditional = 0;
            if(TwoHeads.current.head1 != null && TwoHeads.current.head2 != null){

                if(TwoHeads.current.LeftHeadAtStart || TwoHeads.current.rightHeadAtStart){
                    return 0;
                }

                currentHeight = TwoHeads.current.getHighestBall();

                float deltaHeight = (currentHeight - (Params.current.screenBounds.y * percentOfScreenToAddAdditional)) / (Params.current.screenBounds.y * (1f-percentOfScreenToAddAdditional));

                if(deltaHeight > 1){
                    speedAdditional = 1;
                }else if(deltaHeight > 0){
                    //deltaHeight = Mathf.Min(1,deltaHeight);
                    //speedAdditional = (-1/((0.85f*deltaHeight)-1))-1;
                    //speedAdditional = 4*Mathf.Pow(deltaHeight,2);

                    speedAdditional = deltaHeight;
                }else{
                    speedAdditional = 0;
                }
                
            }
            return speedAdditional;
        }catch(System.Exception e){
            //Debug.LogError(e);
            return 0;
        }
    }

    public static float getAdditionalSpeedForHeads(){
        try{
            float currentHeight = 0;
            float speedAdditional = 0;
            if(TwoHeads.current.head1 != null && TwoHeads.current.head2 != null){

                if(TwoHeads.current.LeftHeadAtStart || TwoHeads.current.rightHeadAtStart){
                    return 0;
                }

                currentHeight = TwoHeads.current.getLowestBall();

                // get height above the screen
                float deltaHeight = currentHeight - Params.current.screenBounds.y;

                if(deltaHeight > 0){
                    speedAdditional = deltaHeight;
                }else{
                    speedAdditional = 0;
                }
                
            }
            return speedAdditional;
        }catch(System.Exception e){
            //Debug.LogError(e);
            return 0;
        }
    }

    static float getAdditionalSpeedFromScore(){
        try{
            float additional = ScoreCounter.current.currentScore / additionalScoreSpeedModifier;
            return additional;
        }catch(System.Exception e){
            return 0;
        }
    }

    void LateUpdate()
    {
        rb.MovePosition(new Vector2(rb.position.x,WallSpawner.current.getBlockHeight(row)));
    }


}