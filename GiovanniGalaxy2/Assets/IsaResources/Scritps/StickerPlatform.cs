using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerPlatform : MonoBehaviour
{
    //{
    //private Rigidbody rb;
    //CharacterController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // playerController = other.GetComponent<CharacterController>();
            other.transform.parent = transform;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;


        }
    }


}

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.transform.SetParent(transform); 
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.transform.SetParent(null);
    //    }
    //}
//}
