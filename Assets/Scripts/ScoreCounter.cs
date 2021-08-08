using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{

    Text currentScoreText;
    Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        currentScoreText = gameObject.transform.Find("Current Score").gameObject.GetComponent<Text>();
        highScoreText = gameObject.transform.Find("High Score").gameObject.GetComponent<Text>();

        GameEvents.current.onStart += this.begin;
        GameEvents.current.onDeath += this.reset;
    }

    void begin(){
        running = true;
    }

    void reset(){
        currentScore = 0;
        running = false;
        elapsedTime = 0;
        currentScoreText.text = "Score: " + currentScore;
    }

    bool running = false;
    int currentScore;
    int hightScore;

    // Update is called once per frame

    float elapsedTime = 0f;

    void Update()
    {
        if(running){
            elapsedTime+=Time.deltaTime;
            if(elapsedTime >= 0.1f){
                elapsedTime = 0f;
                currentScore += 1;
                currentScoreText.text = "Score: " + currentScore;
            }

            if(currentScore > hightScore){
                hightScore = currentScore;
                highScoreText.text = "High Score: " + hightScore;
            }
        }
    }
}
