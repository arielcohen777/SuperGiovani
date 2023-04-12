using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
//using UnityEditor.VersionControl;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class Battery : MonoBehaviour
{
    private GameObject battery;
    private GameManager gm;
    private State state;
    private ParticleSystem chargeEffect;

    public Material [] batteryColor;
    private GameObject[] children;

    private bool inactive, charging, charged, inTrigger;

    private AudioSource audioSource;
    public AudioClip[] batterySFX;

    //Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
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
    public void Inactive()
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
    public void Charging()
    {         
        if (!charging)
        {
            audioSource.clip = batterySFX[0];
            audioSource.loop = true;
            audioSource.Play();

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
    public void Charged()
    {
        if (!charged)
        {
            audioSource.clip = batterySFX[1];
            audioSource.loop = false;
            audioSource.Play();

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
            audioSource.clip = batterySFX[2];             
            audioSource.Play();

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
