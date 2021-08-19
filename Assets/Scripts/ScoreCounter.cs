using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour
{

    TMP_Text currentScoreText;
    TMP_Text highScoreText;

    

    // Start is called before the first frame update
    void Start()
    {
        currentScoreText = gameObject.transform.Find("Current Score").gameObject.GetComponent<TMP_Text>();
        highScoreText = gameObject.transform.Find("High Score").gameObject.GetComponent<TMP_Text>();

        GameEvents.current.onStart += this.begin;
        GameEvents.current.onDeath += this.reset;
    }

    void begin(){
        running = true;
    }

    void reset(){
        currentScore = 0;
        running = false;
        elapsedDistance = 0;
        currentScoreText.text = "Score: " + currentScore;
    }

    bool running = false;
    int currentScore;
    int hightScore;

    // Update is called once per frame
    float elapsedDistance = 0f;

    void Update()
    {

        //WallSpawner.current.setSpeed(Mathf.Min(Params.initialWallFallSpeed+currentScore*0.002f,8f));

        if(running){
            elapsedDistance+=Time.deltaTime*MoveDown.currentSpeed();
            if(elapsedDistance >= 0.1f){
                elapsedDistance = 0f;
                currentScore += 1;
                currentScoreText.text = "Score: " + currentScore;
                Params.current.lastScore = currentScore;
            }

            

            if(currentScore > hightScore){
                hightScore = currentScore;
                highScoreText.text = "Best: " + hightScore;
                Params.current.bestScore = hightScore;
            }
        }
    }
}
