using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScreen : MonoBehaviour
{
    private IFlashScreenState currentState;

    private void Awake()
    {
        // Set the initial state to IdleState
        currentState = new IdleState(this);
    }

    // Call this function to start the flashing effect
    public void FlashRed(float duration)
    {
        // Set the current state to FlashingState
        currentState = new FlashingState(this, duration);
        currentState.EnterState();
    }

    // Call this function to stop the flashing effect
    public void StopFlashing()
    {
        // Set the current state to IdleState
        currentState = new IdleState(this);
        currentState.EnterState();
    }

    // Update is called once per frame
    private void Update()
    {
        // Update the current state
        currentState.UpdateState();
    }
}