using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScreen : MonoBehaviour
{
    public void FlashRed(float duration)
    {
        StartCoroutine(FlashRedCoroutine(duration));
    }

    private IEnumerator FlashRedCoroutine(float duration)
    {
        Color originalColor = GetComponent<Image>().color;
        Color flashColor = Color.red;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            GetComponent<Image>().color = flashColor;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Image>().color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

}

/*This method creates a coroutine that flashes the Image element red for a
specified duration. The coroutine changes the color of the Image element to red for
0.1 seconds, then back to its original color for 0.1 seconds, and repeats this 
process until the specified duration has passed.*/
