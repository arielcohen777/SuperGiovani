using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlatforms : MonoBehaviour
{
   // [SerializeField] string playerTag = "Player";
    [SerializeField] float disappearTime = 3;
    Animator anim;

    [SerializeField] bool canReset;
    [SerializeField] float resetTime;

    private BoxCollider[] boxcol; 

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("DisappearTime", 1 / disappearTime);
        boxcol = GetComponents<BoxCollider>(); 
    }

    /*  private void OnCollisionEnter(Collision collision)
      {
          if (collision)
              anim.SetBool("Trigger", true); 
      }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            anim.SetBool("Trigger", true); 
    }

    public void TriggerReset() 
    {
        if (canReset)
        {
            StartCoroutine(Reset());
            boxcol[1].enabled = false;
        }

    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        anim.SetBool("Trigger", false);
        boxcol[1].enabled = true;
    }
}
