using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellAudio : MonoBehaviour
{
    public AudioClip[] shellSFX; 
    private AudioSource audioSource;
    private ShellProjectile shellProjectile;
    void Start()
    {
        shellProjectile = GetComponentInParent<ShellProjectile>();
        audioSource = GetComponentInParent<AudioSource>();
    }
   
    private void OnTriggerEnter(Collider other)
    {
        //print(shellProjectile.rb.velocity.magnitude);
        if (other.CompareTag("Player") && shellProjectile.rb.velocity.magnitude > 15){
            audioSource.Play();
        }
        
        if (other.CompareTag("Player") && shellProjectile.rb.velocity.magnitude < 15)
        {
            audioSource.clip = shellSFX[1];
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
