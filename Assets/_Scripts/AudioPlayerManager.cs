using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerManager : MonoBehaviour
{
    private static AudioPlayerManager instance = null;
    private AudioSource music;

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return; 
        Destroy(gameObject);
    }

    void Start()
    {
        music = GetComponent<AudioSource>();
        music.Play();
    }
}
