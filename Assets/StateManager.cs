using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    

    // three states
    //  - Main menu
    //  - playing game
    //  - death screen

    public static StateManager current;

    public GameObject mainMenuUI;
    public GameObject GameUI;
    public GameObject DeathUI;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        // mainMenuUI = GameObject.Find("MainMenu");
        // GameUI = GameObject.Find("GameScreen");
        // DeathUI = GameObject.Find("DeathScreen");
        mainMenuUI.SetActive(true);
        GameUI.SetActive(false);
        DeathUI.SetActive(false);

        GameEvents.current.onDeath += showDeathAfter1Second;
    }

    public void showDeathAfter1Second(){
        Invoke("showGameoverScreen",1);
    }

    public void startGame(){
        WallSpawner.current.reset();
        mainMenuUI.SetActive(false);
        GameUI.SetActive(true);
        DeathUI.SetActive(false);
        CameraAnimation.current.playAnimation();
        WallSpawner.current.spawnStartBlocks();
        TwoHeads.current.reset();
    }

    public void showGameoverScreen(){
        mainMenuUI.SetActive(false);
        GameUI.SetActive(false);
        DeathUI.SetActive(true);
        //GameOverScreen.current.fadeIn(0.2f);
    }

    public void openMainMenu(){
        mainMenuUI.SetActive(true);
        GameUI.SetActive(false);
        DeathUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
