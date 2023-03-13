using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashText : MonoBehaviour
{
    public float flashInterval = 1f;    // Time in seconds between text flashes
    public Color flashColor = Color.red;    // Color to flash the text

    private float timer;    // Timer to keep track of the flashing interval
    private TextMeshProUGUI textMeshPro;    // Reference to the TMP component

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        timer = flashInterval;
    }

    private void Update()
    {
        // Decrement the timer and check if it has reached 0
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            // Reset the timer and toggle the text color
            timer = flashInterval;
            textMeshPro.color = textMeshPro.color == flashColor ? Color.red : flashColor;
        }
    }
}

