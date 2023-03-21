using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class InteractablesMovement : MonoBehaviour
{
    public float speed;
    public Vector3 rotation; 

    // Update is called once per frame
    void Update()
    {
        AddRotation();    
    }

    void AddRotation()
    {
        transform.Rotate(rotation * speed * Time.deltaTime);
    }
  
}
