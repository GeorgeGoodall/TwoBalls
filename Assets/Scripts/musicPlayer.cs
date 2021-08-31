using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayer : MonoBehaviour
{

    public static musicPlayer current;

    public AudioClip song;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        audioSource.clip = song;
        audioSource.loop = true;
        audioSource.Play();

    }

    public void playSong(int index){
        
        audioSource.Stop();
        audioSource.clip = song;
        audioSource.Play();
      
    }
}
