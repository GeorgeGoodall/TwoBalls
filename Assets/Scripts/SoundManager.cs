using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager current;
    public AudioSource musicSrc;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;

    public AudioClip music;
    public AudioClip grabbableGrab;
    public AudioClip sandGrab;
    public AudioClip sandCrumble;
    public AudioClip sandBreak;
    public AudioClip deathGrab;
    public AudioClip fallOffScreen;
    public AudioClip newHighScore;
    public AudioClip scoreMilestone;

    private AudioClip[] audioClips;

    public enum AudioType
    {
        grabbableGrab,
        sandGrab,
        sandCrumble,
        sandBreak,
        deathGrab,
        fallOffScreen,
        newHighScore,
        scoreMilestone
    }

    
    void Start()
    {
        current = this;    
        audioClips = new AudioClip[]{
            grabbableGrab,
            sandGrab,
            sandCrumble,
            sandBreak,
            deathGrab,
            fallOffScreen,
            newHighScore,
            scoreMilestone
        };
    }

    public void play(AudioType audioType){
        playAudio(audioClips[(int)audioType]);
    }

    void playAudio(AudioClip audio){
        if(!audio1.isPlaying){
            audio1.PlayOneShot(audio);
        }else if(!audio2.isPlaying){
            audio2.PlayOneShot(audio);
        }else if(!audio3.isPlaying){
            audio3.PlayOneShot(audio);
        }else{
            audio1.PlayOneShot(audio);
        }
    }
}
