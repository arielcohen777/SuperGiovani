using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BlinkingPrompt : MonoBehaviour
{
    public float startDelay = 5f; // The delay before the prompt appears
    public KeyCode continueKey = KeyCode.Return; // The key to press to continue

    public TextMeshProUGUI promptText;
    private bool isActive = false;

    void Start()
    {
        promptText = GetComponent<TextMeshProUGUI>();
        promptText.alpha = 0f;
        StartCoroutine(ActivatePrompt());
    }

    void Update()
    {
        if (isActive && Input.GetKeyDown(continueKey))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
    IEnumerator ActivatePrompt()
    {
        Debug.Log("ActivatePrompt coroutine started");

        yield return new WaitForSeconds(startDelay);

        isActive = true;
        promptText.alpha = 1f;
        promptText.gameObject.SetActive(true);

        while (isActive)
        {
            yield return null;
        }
    }
}