using UnityEngine;
using System.Collections;
using TMPro;

public class ActivateText : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    private bool textActivated = false;

    void Start()
    {
        // Disable text at start
        textComponent.gameObject.SetActive(false);

        // Start coroutine to activate text after 10 seconds
        StartCoroutine(ActivateTextAfterDelay());
    }

    void Update()
    {
        // Check if text is activated and ESCAPE key is pressed
        if (textActivated && Input.GetKeyDown(KeyCode.Escape))
        {
            // Quit the application
            Application.Quit();
        }
    }

    IEnumerator ActivateTextAfterDelay()
    {
        // Wait for 10 seconds
        yield return new WaitForSeconds(10f);

        // Activate text
        textComponent.gameObject.SetActive(true);
        textActivated = true;
    }
}
