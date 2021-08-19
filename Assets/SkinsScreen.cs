using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsScreen : MonoBehaviour
{

    private Button backButton;
    public GameObject displayBalls;
    public GameObject skinsWrapper;

    // Start is called before the first frame update
    void Start()
    {
        backButton = GameObject.Find("BackButton").GetComponent<Button>();
        backButton.onClick.AddListener(back);
    }

    void back(){
        StateManager.current.openMainMenu();
    }
}
