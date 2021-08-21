using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class videoPlayerController : MonoBehaviour
{
    public VideoPlayer player;

    private bool hasStartedPlaying = false;

    void Update()
    {
        if(!hasStartedPlaying && player.isPlaying){
            hasStartedPlaying = true;
        }


        if(hasStartedPlaying && !player.isPlaying){
            SceneManager.LoadScene("Main");
        }
    }
}
