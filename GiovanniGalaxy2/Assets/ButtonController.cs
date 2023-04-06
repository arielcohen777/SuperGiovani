using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(FlashText());
    }

    IEnumerator FlashText()
    {
        while (true)
        {
            text.color = Color.black;
            yield return new WaitForSeconds(0.5f);
            text.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void LoadNextLevel()
    {
        // Load the next scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

