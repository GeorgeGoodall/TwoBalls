using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBall : MonoBehaviour
{

    public Image LeftBall;
    public Image RightBall;
    public UnityEngine.UI.Extensions.UILineRenderer ropeRenderer;
    public Image UnlockCover;


    private bool Locked = false;

   
    public void setBalls(Sprite ball){
        LeftBall.sprite = ball;
        RightBall.sprite = ball;
    }

    public void setBalls(Sprite leftBall, Sprite rightBall){
        LeftBall.sprite = leftBall;
        RightBall.sprite = rightBall;
    }

    public void setBalls(Sprite ball, bool colored){
        LeftBall.sprite = ball;
        RightBall.sprite = ball;

        setBallColours(colored);
    }

    public void setBalls(Sprite leftBall, Sprite rightBall, bool colored){
        LeftBall.sprite = leftBall;
        RightBall.sprite = rightBall;

        setBallColours(colored);
    }

    void setBallColours(bool colored){
        if(colored){
            LeftBall.color = new Color(250f/255f,145f/255f,145f/255f);
            RightBall.color = new Color(145f/255f,246f/255f,250f/255f);
        }else{
            LeftBall.color = new Color(0,0,0);
            RightBall.color = new Color(0,0,0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
