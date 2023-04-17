using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableButton : MonoBehaviour
{
    public Button button;
    public float delay = 5f;

    void Start()
    {
        StartCoroutine(EnableButtonCoroutine());
    }

    IEnumerator EnableButtonCoroutine()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Enable the button
        button.gameObject.SetActive(true);
        button.image.enabled = true;
    }
}
