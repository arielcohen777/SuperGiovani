using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.VersionControl;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using UnityEngine.InputSystem;

public class Battery : MonoBehaviour
{
    private GameObject battery;
    private GameManager gm;
    private State state;
    private ParticleSystem chargeEffect;

    public Material [] batteryColor;
    private GameObject[] children;

    private bool inactive, charging, charged, inTrigger;

    // Start is called before the first frame update
    void Start()
    {
        battery = gameObject;
        gm = GameManager.Instance;
        state = State.inactive;
        chargeEffect = gameObject.GetComponentInChildren<ParticleSystem>();
        chargeEffect.Pause();
        CountChildren();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.inactive: 
                Inactive();
                break;
            case State.charging:
                Charging();
                break;
            case State.charged:
                Charged();
                break;
        }
    }

    private enum State
    {
        inactive,
        charging,
        charged
    }
    private void Inactive()
    {
        if (!inactive)
        {
            children[0].GetComponentInChildren<MeshRenderer>().material = batteryColor[0];
            inactive = true;
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.E) && inTrigger)
            {
                state = State.charging;
            }
        }
          
    }
    private void Charging()
    {         
        if (!charging)
        {
            gm.batteryCounter.timeValue = 20;
            children[0].GetComponentInChildren<MeshRenderer>().material = batteryColor[1];            
            chargeEffect.Play();
            charging = true;
        }

        else if (gm.batteryCounter.timeValue == 0)
        {
            state = State.charged;
        }   
    }
    private void Charged()
    {
        if (!charged)
        {          
            children[0].GetComponentInChildren<MeshRenderer>().material = batteryColor[2];            
            chargeEffect.gameObject.SetActive(false);
            gm.batteryCounter.mssgText = "Ready!";
            charged = true;
        }
    }
  
    private void CountChildren()
    {
        children = new GameObject[battery.transform.childCount];
        for (int i = 0; i < battery.transform.childCount; i++)
        {
            children[i] = battery.transform.GetChild(i).gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (BatteryCounter.isCharging)
        {
            if (other.CompareTag("Player") && state == State.inactive || state == State.charging)// && BatteryCounter.isCharging)
            {
                gm.batteryCounter.mssgText = "CHARGING...";
            }
        }

        else
        {
            if (other.CompareTag("Player") && state == State.inactive) 
            {
                gm.batteryCounter.mssgText = "PRESS E TO START CHARGING";
                inTrigger = true;               
            }           
        }

        if (other.CompareTag("Player") && state == State.charged)
        {
            gm.batteryCounter.Add(this);
            battery.SetActive(false);
            gm.batteryCounter.mssgText = "";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.batteryCounter.mssgText = "";
            inTrigger = false;
        }
    }


}
