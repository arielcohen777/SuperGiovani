using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementShip : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, Time.deltaTime * speed, 0);
    }
}