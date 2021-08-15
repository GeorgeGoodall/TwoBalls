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
    public GameObject SkinsPage;

    public enum Page
    {
        MAIN_MENU,
        GAME_UI,
        DEATH_UI,
        SKINS_PAGE
    }

    private GameObject[] pageObjects;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        // mainMenuUI = GameObject.Find("MainMenu");
        // GameUI = GameObject.Find("GameScreen");
        // DeathUI = GameObject.Find("DeathScreen");

        pageObjects = new GameObject[]{
            mainMenuUI,
            GameUI,
            DeathUI,
            SkinsPage
        };
        hideAll();
        mainMenuUI.SetActive(true);


        GameEvents.current.onDeath += showDeathAfter1Second;
    }

    void openPage(Page page){
        hideAll();
        pageObjects[(int)page].SetActive(true);
    }

    void hideAll(){
        foreach (GameObject page in pageObjects)
        {
            page.SetActive(false);
        }
    }

    public void showDeathAfter1Second(){
        Invoke("showGameoverScreen",1);
    }

    public void startGame(){
        WallSpawner.current.reset();
        CameraAnimation.current.playAnimation();
        WallSpawner.current.spawnStartBlocks();
        TwoHeads.current.reset();
        openPage(Page.GAME_UI);
    }

    public void showGameoverScreen(){
        openPage(Page.DEATH_UI);
        //GameOverScreen.current.fadeIn(0.2f);
    }

    public void openMainMenu(){
        openPage(Page.MAIN_MENU);
    }

    public void openSkins(){
        openPage(Page.SKINS_PAGE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
