using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinsScreen : MonoBehaviour
{

    private Button backButton;
    public TMP_Text pannelButtonText;
    public GameObject displayBalls;
    public GameObject ropePannel;
    public GameObject ballPannel;
    public GameObject skinsWrapper;

    private bool ballPannelOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        backButton = GameObject.Find("BackButton").GetComponent<Button>();
        backButton.onClick.AddListener(back);

        if(ballPannelOpen){
            ropePannel.SetActive(false);
            ballPannel.SetActive(true);
            pannelButtonText.text = "Change Rope";
        }else{
            ropePannel.SetActive(true);
            ballPannel.SetActive(false);
            pannelButtonText.text = "Change Balls";
        }
    }

    void back(){
        StateManager.current.openMainMenu();
    }

    public void openRopePannel(){
        ropePannel.SetActive(true);
        ballPannel.SetActive(false);
    }
    public void openBallPannel(){
        ropePannel.SetActive(false);
        ballPannel.SetActive(true);   
    }
}
