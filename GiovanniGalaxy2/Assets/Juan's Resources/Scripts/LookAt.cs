using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private GameManager gm;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        SmoothRotation(0.2f, 1, gm.player.transform.position);
    }

    //only assign values of 1 o -1 for x to invert direction 
    //assing a value from 0 to 1 for t depending on how fast you want the object to turn
    private void SmoothRotation(float t, int x, Vector3 target)
    {
        direction = (target - transform.position).normalized * x;
        if (gameObject.CompareTag("Portal"))
        {             
            Quaternion lookRotation = Quaternion.LookRotation(direction);            
            Vector3 newEulerAngles = new Vector3(0f, lookRotation.eulerAngles.y, 0f);// rotates only on the y axis without tilting
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newEulerAngles), t);
        }
        else
        {          
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, t);
        }
    }     
}
