using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager current;

    [Header("Audio Sources")]
    public AudioSource musicSrc;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;

    [Header("Audio Clips")]
    [Space]
    public AudioClip music;
    public float music_volume;
    [Space]
    public AudioClip grabbableGrab;
    public float grabbableGrab_volume;
    [Space]
    public AudioClip sandGrab;
    public float sandGrab_volume;
    [Space]
    public AudioClip sandCrumble;
    public float sandCrumble_volume;
    [Space]
    public AudioClip sandBreak;
    public float sandBreak_volume;
    [Space]
    public AudioClip deathGrab;
    public float deathGrab_volume;
    [Space]
    public AudioClip fallOffScreen;
    public float fallOffScreen_volume;
    [Space]
    public AudioClip newHighScore;
    public float newHighScore_volume;
    [Space]
    public AudioClip scoreMilestone;
    public float scoreMilestone_volume;
    [Space]


    [HideInInspector]
    public bool gameMuted = false;

    private AudioClip[] audioClips;
    private float[] audioVolumes;

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

        audioVolumes = new float[]{
            grabbableGrab_volume,
            sandGrab_volume,
            sandCrumble_volume,
            sandBreak_volume,
            deathGrab_volume,
            fallOffScreen_volume,
            newHighScore_volume,
            scoreMilestone_volume
        };

        musicSrc.clip = music;
        musicSrc.loop = true;
        musicSrc.volume = music_volume;
        playMusic();
    }

    public void playMusic(){
        musicSrc.Play();
    }

    public void stopMusic(){
        musicSrc.Pause();
    }

    public void play(AudioType audioType){
        playAudio(audioClips[(int)audioType], audioVolumes[(int)audioType]);
    }

    public void play(AudioType audioType, float pitch){
        playAudio(audioClips[(int)audioType], audioVolumes[(int)audioType], pitch);
    }

    void playAudio(AudioClip audio, float volume = 1f, float pitch = 1f){
        if(!gameMuted){
            if(!audio1.isPlaying){
                audio1.pitch = pitch;
                audio1.volume = volume;
                audio1.PlayOneShot(audio);
            }else if(!audio2.isPlaying){
                audio2.volume = volume;
                audio1.pitch = pitch;
                audio2.PlayOneShot(audio);
            }else if(!audio3.isPlaying){
                audio3.volume = volume;
                audio1.pitch = pitch;
                audio3.PlayOneShot(audio);
            }else{
                audio1.volume = volume;
                audio1.pitch = pitch;
                audio1.PlayOneShot(audio);
            }
        }
    }
}
