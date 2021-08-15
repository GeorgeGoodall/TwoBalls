using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{

    private TMP_Text bestScoreText;
    private Button playButton;
    private Button skinsButton;
    private Button settingsButton;
    private Button closeButton;

    // Start is called before the first frame update
    void Start()
    {
        bestScoreText = gameObject.transform.Find("Score").GetComponent<TMP_Text>();
        playButton = GameObject.Find("Play").GetComponent<Button>();
        skinsButton = GameObject.Find("Skins").GetComponent<Button>();
        settingsButton = GameObject.Find("Settings").GetComponent<Button>();
        closeButton = GameObject.Find("CloseButton").GetComponent<Button>();

        if(Params.current.bestScore > 0){
            bestScoreText.text = "Current Best:\n"+Params.current.bestScore;
        }else{
            bestScoreText.text = "";
        }

        assignButtonFunctions();
    }

    void OnEnable()
    {
        if(bestScoreText != null){
            if(Params.current.bestScore > 0){
                bestScoreText.text = "Current Best:\n"+Params.current.bestScore;
            }else{
                bestScoreText.text = "";
            }
        }
    }

    void assignButtonFunctions(){
        playButton.onClick.AddListener(playButtonClick);
        skinsButton.onClick.AddListener(skinButtonClick);
        closeButton.onClick.AddListener(closeApplication);
    }

    void closeApplication() => Application.Quit();
    void playButtonClick() => StateManager.current.startGame();
    void skinButtonClick() => StateManager.current.openSkins();

    // Update is called once per frame
    void Update()
    {
        
    }
}
