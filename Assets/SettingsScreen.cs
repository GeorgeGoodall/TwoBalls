using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsScreen : MonoBehaviour
{

    public TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        populateDropdown();
        dropdown.onValueChanged.AddListener(delegate {
            changeSong();
        });
    }

    void populateDropdown(){
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (AudioClip song in musicPlayer.current.songs)
        {
            options.Add(song.name);
        }
        dropdown.AddOptions(options);
    }

    void changeSong(){
        musicPlayer.current.playSong(dropdown.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
