using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{

    public TMP_Text score;
    public Button Try_Again_btn;
    public Button Main_Menu_btn;

    private TMP_Text Try_Again_txt;
    private TMP_Text Main_Menu_txt;

    float currentAlpha = 1f;
    float targetAlpha = 1f;

    float fadeTime = 0.1f;
    float elapsedTime = 0f;
    


    public static GameOverScreen current;

    // Start is called before the first frame update
    void Start()
    {
        current = this;

        Try_Again_txt = Try_Again_btn.gameObject.GetComponentInChildren<TMP_Text>();
        Main_Menu_txt = Main_Menu_btn.gameObject.GetComponentInChildren<TMP_Text>();

        Try_Again_btn.onClick.AddListener(tryAgain);
        Main_Menu_btn.onClick.AddListener(mainMenu);
        

        score.text = "Score:\n"+Params.current.lastScore;
    }

    private void OnEnable() {
        if(score != null && Params.current != null){
            score.text = "Score:\n"+Params.current.lastScore;
        }
    }

    public void fadeIn(float _fadeInTime){
        currentAlpha = 0f;
        targetAlpha = 1f;
        float fadeTime = _fadeInTime;
    }

    void setAlpha(){
        if(currentAlpha != targetAlpha){

            currentAlpha = targetAlpha * elapsedTime/fadeTime;

            if(elapsedTime>fadeTime){
                currentAlpha = targetAlpha;
            }

            score.color = new Color(score.color.r,score.color.g,score.color.b,currentAlpha);
            Try_Again_txt.color = new Color(Try_Again_txt.color.r,Try_Again_txt.color.g,Try_Again_txt.color.b,currentAlpha);
            Main_Menu_txt.color = new Color(Main_Menu_txt.color.r,Main_Menu_txt.color.g,Main_Menu_txt.color.b,currentAlpha);

        }
    }

    public void tryAgain(){
        StateManager.current.startGame();

    }

    public void mainMenu(){
        StateManager.current.openMainMenu();

    }

    void Update()
    {
        
    }


}
