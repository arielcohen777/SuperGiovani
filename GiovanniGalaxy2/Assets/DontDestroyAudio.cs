using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{
    public AudioSource audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(audioSource);
    }
}