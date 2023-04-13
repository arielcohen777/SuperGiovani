using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource musicSource;
    public AudioClip[] playList;
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        InvokeRepeating("ChangeSong", 0f, 0.5f);
    }
    private void ChangeSong()
    {
        
        if (!musicSource.isPlaying)
        {
            musicSource.clip = playList[i];
            musicSource.Play();
            i++;
        }
        if (i == playList.Length) i = 0;
    }
}
