using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public Text interText;    
    private State state;

    public ParticleSystem[] portalType;     
     

    private GameManager gm;

    public string mssgText = ""; 
    private bool inactive, active, firstS, secondS, thirdS, inTrigger;
    public int BatteriesAdded = 0;


     
    public GameObject portal;
    private ParticleSystem currentPortal;
    private ParticleSystem newPortal;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        state = State.inactive;
          

    }

    // Update is called once per frame
    void Update()
    {
        interText.text = mssgText;
        switch (state)
        {
            case State.inactive:
                Inactive();
                break;
            case State.active:
                Active();
                break;
            case State.fist_Stage:
                FirstStage();
                break;
            case State.second_Stage:
                SecondStage();
                break;
            case State.third_Stage:
                ThirdStage();
                break;
        }
        AddBattery();
    }

    private enum State
    {
        inactive,
        active,
        fist_Stage,
        second_Stage,
        third_Stage
    }

    private void Inactive()
    {
        if (!inactive)
        {
             
            inactive = true;
        }     
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && inTrigger)
                {
                    state = State.active;
                }
        }
       
    }
    private void Active()
    {
        if (!active)
        {
           currentPortal = InstantiatePortal(BatteriesAdded);
           currentPortal.transform.localScale = new Vector3(0.25f, 0.5f, 5f);
           active = true;
        }

        else if (BatteriesAdded == 1)
        {
            state = State.fist_Stage;
        }

    }
    private void FirstStage()
    {
        if (!firstS)
        {
            newPortal = SwapPortal(BatteriesAdded, currentPortal);
            newPortal.transform.localScale = new Vector3(0.4f, 0.7f, 10f);
            currentPortal = newPortal;
            firstS = true;
        }

        else if (BatteriesAdded == 2)
        {
            state = State.second_Stage;
        }

    }

    private void SecondStage()
    {
        if (!secondS)
        {
            newPortal = SwapPortal(BatteriesAdded, currentPortal);
            newPortal.transform.localScale = new Vector3(0.55f, 0.85f, 13f);
            currentPortal = newPortal;
            secondS = true;
        }
        else if (BatteriesAdded == 3)
        {
            state = State.third_Stage;
        }
    }

    private void ThirdStage()
    {
        if (!thirdS)
        {
            newPortal = SwapPortal(BatteriesAdded, currentPortal);
            newPortal.transform.localScale = new Vector3(1.2f, 1.2f, 15f);
            currentPortal = newPortal;
            thirdS = true;
        }
    }

    private ParticleSystem InstantiatePortal(int i)
    {
                
        ParticleSystem currentPs = Instantiate(portalType[i], portal.transform);

        return currentPs;
    }

    private ParticleSystem SwapPortal(int i, ParticleSystem currentPs)
    {
        if(currentPs != null)
        {
            Destroy(currentPs.gameObject);
        }
        ParticleSystem newPs = Instantiate(portalType[i], portal.transform);

        return newPs;
    }

    private void AddBattery()
    {
        if (Input.GetKeyDown(KeyCode.E) && gm.batteryCounter.batteryCtr > 0 && inTrigger)
        {
            gm.batteryCounter.Remove(gm.batteryCounter.items[0]);
            BatteriesAdded++;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && state == State.inactive){
            mssgText = "PRESS E TO ACTIVATE PORTAL";
            inTrigger = true;
            
        }
        if (other.CompareTag("Player") && state != State.inactive)
        {
            mssgText = "ADD BATTERIES" + " " + BatteriesAdded + "/3";
            inTrigger = true;          
        }       

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            mssgText = "";
            inTrigger = false;
        } 
    }
}
