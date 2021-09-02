using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsScreen : MonoBehaviour
{

    bool musicMuted = false;
    bool gameMuted = false;

    [Header("images")]
    public Image musicImage;
    public Image gameImage;
    [Header("sprites")]
    public Sprite notMutedIcon;
    public Sprite mutedIcon;


    void Start()
    {
        if(musicMuted){
            musicImage.sprite = mutedIcon;
        }else{
            musicImage.sprite = notMutedIcon;
        }

        if(gameMuted){
            gameImage.sprite = mutedIcon;
        }else{
            gameImage.sprite = notMutedIcon;
        }
    }

    public void toggleMusicMute(){
        musicMuted = !musicMuted;

        if(musicMuted){
            musicImage.sprite = mutedIcon;
            SoundManager.current.stopMusic();
        }else{
            musicImage.sprite = notMutedIcon;
            SoundManager.current.playMusic();
        }
    }

    public void toggleGameMute(){
        gameMuted = !gameMuted;

        SoundManager.current.gameMuted = gameMuted;

        if(gameMuted){
            gameImage.sprite = mutedIcon;
        }else{
            gameImage.sprite = notMutedIcon;
        }
    }


    
}
