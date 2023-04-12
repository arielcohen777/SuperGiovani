using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRandomSounds : MonoBehaviour
{
    public string[] sounds;
    [SerializeField] private float delayTime;
    private int lastPlayedString;

    [SerializeField] private AudioSource source;

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
            } while (index == lastPlayedString);

            lastPlayedString = index;
            string toPlay = sounds[index];
            Debug.Log(toPlay);
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
