using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// This state is active when the flashing effect is running
public class FlashingState : IFlashScreenState
{
    private readonly FlashScreen flashScreen;
    private readonly float duration;
    private readonly Color originalColor;
    private readonly Color flashColor;
    private float startTime;

    public FlashingState(FlashScreen flashScreen, float duration)
    {
        this.flashScreen = flashScreen;
        this.duration = duration;
        this.originalColor = flashScreen.GetComponent<Image>().color;
        this.flashColor = Color.red;
    }

    public void EnterState()
    {
        startTime = Time.time;
    }

    public void UpdateState()
    {
        // Check if the duration has passed
        if (Time.time >= startTime + duration)
        {
            // Stop flashing and return to IdleState
            flashScreen.StopFlashing();
            return;
        }

        // Flash the screen by changing the color
        if (flashScreen.GetComponent<Image>().color == originalColor)
        {
            flashScreen.GetComponent<Image>().color = flashColor;
        }
        else
        {
            flashScreen.GetComponent<Image>().color = originalColor;
        }
    }
}

