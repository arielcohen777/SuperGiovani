using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelButton : MonoBehaviour
{
    public string loadingSceneName = "LoadingScene";
    public float loadingDelay = 5f;

    public void LoadGame()
    {
        StartCoroutine(LoadGameCoroutine());
    }

    IEnumerator LoadGameCoroutine()
    {
        // Load the loading scene
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(loadingSceneName);

        // Wait for the loading scene to finish loading
        while (!loadingScene.isDone)
        {
            yield return null;
        }

        // Wait for loadingDelay seconds
        yield return new WaitForSeconds(loadingDelay);

        // Load the next level
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // wrap around to the first scene
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
