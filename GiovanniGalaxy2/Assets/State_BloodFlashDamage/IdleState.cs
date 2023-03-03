using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This state is active when the flashing effect is not running
public class IdleState : IFlashScreenState
{
    private readonly FlashScreen flashScreen;

    public IdleState(FlashScreen flashScreen)
    {
        this.flashScreen = flashScreen;
    }

    public void EnterState()
    {
        // Do nothing
    }

    public void UpdateState()
    {
        // Do nothing
    }
}
