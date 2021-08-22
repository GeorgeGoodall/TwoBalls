using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoBalls2 : MonoBehaviour
{

    public Rigidbody2D leftHeadRb, rightHeadRb;
    float movementForceVertical = 5f;

    public static TwoBalls2 current;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    public void applyVerticalForceToLeftHead(float force){
        
            leftHeadRb.AddForce(Vector2.up * force * movementForceVertical, ForceMode2D.Impulse);
        
    }

    public void applyVerticalForceToRightHead(float force){
        
            rightHeadRb.AddForce(Vector2.up * force * movementForceVertical, ForceMode2D.Impulse);
        
    }

    public void applyHorizontalForceToLeftHead(float force){
        
            leftHeadRb.AddForce(Vector2.right * force * movementForceVertical, ForceMode2D.Impulse);
        
    }

    public void applyHorizontalForceToRightHead(float force){
        
            rightHeadRb.AddForce(Vector2.right * force * movementForceVertical, ForceMode2D.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
