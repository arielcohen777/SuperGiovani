using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRandomSounds : MonoBehaviour
{
    public AudioClip[] sounds;
    [SerializeField] private float delayTime;
    [SerializeField] private int lastPlayedSoundIndex;

    private AudioSource source;

    bool canPlayAgain = true;
    static bool soundIsPlaying = false;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        if (canPlayAgain && !soundIsPlaying)
        {
            canPlayAgain = false;
            soundIsPlaying = true;

            int index;
            do
            {
                index = Random.Range(0, sounds.Length);
            } while (index == lastPlayedSoundIndex);

            lastPlayedSoundIndex = index;
            source.clip = sounds[index];
            if (source.clip != null)
                source.Play();
            StartCoroutine(CanPlayAgain());
        }
    }

    IEnumerator CanPlayAgain()
    {
        yield return new WaitForSeconds(delayTime);
        soundIsPlaying = false;
        canPlayAgain = true;
    }
}
