using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayer : MonoBehaviour
{

    public AudioClip[] songs;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        songs = Resources.LoadAll<AudioClip>("Music");
        audioSource.clip = songs[0];
        audioSource.loop = true;
        audioSource.Play();

    }
}
