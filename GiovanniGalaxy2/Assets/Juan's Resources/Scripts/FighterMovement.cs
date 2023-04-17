using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMovement : MonoBehaviour
{
    public Transform [] waypoints;
    private Transform shipTransform;
    private int i  = 0;

    private AudioSource audioSource;
    
    public float moveSpeed = 10f;   

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        shipTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        WaypointPath();   
    }

    private void WaypointPath()
    {
        if(Vector3.Distance(shipTransform.position, waypoints[i].position) < 1f)
        {
            i++;
             
            if (i >= waypoints.Length)
            {
                i = 0;
            }
        }

        transform.Translate((waypoints[i].position - shipTransform.position).normalized * moveSpeed * Time.deltaTime);
        //print((waypoints[i].position - shipTransform.position).magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
    }



}
