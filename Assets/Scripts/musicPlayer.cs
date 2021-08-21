using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayer : MonoBehaviour
{

    public static musicPlayer current;

    public AudioClip[] songs;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        songs = Resources.LoadAll<AudioClip>("Music");
        audioSource.clip = songs[0];
        audioSource.loop = true;
        audioSource.Play();

    }

    public void playSong(int index){
        
        audioSource.Stop();
        audioSource.clip = songs[index];
        audioSource.Play();
      
    }
}
