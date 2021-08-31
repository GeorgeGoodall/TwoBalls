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

        
        public void setTwoBalls(GameObject ball1, GameObject ball2){
                Rigidbody2D ball1rb = ball1.GetComponent<Rigidbody2D>();
                Rigidbody2D ball2rb = ball2.GetComponent<Rigidbody2D>();
                setTwoBalls(ball1rb,ball2rb);
        }
        public void setTwoBalls(Rigidbody2D ball1, Rigidbody2D ball2){
                leftHeadRb = ball1;
                rightHeadRb = ball2;
        }
}
