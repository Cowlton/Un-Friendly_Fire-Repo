using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSc : MonoBehaviour
{
    
    public AudioSource source;
    public AudioClip[] clips;
    // Start is called before the first frame update
    void Start()
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }

    void Update()
    {
        if (!source.isPlaying)
        {
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
        }
    }
}
