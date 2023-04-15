using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomPanelActivator : MonoBehaviour
{
    public GameObject parentObject;
    public KeyCode previousKey = KeyCode.Q;
    public KeyCode nextKey = KeyCode.E;

    private GameObject[] childObjects;
    private int currentIndex;
    private bool isLoadingScene = false;

    void Start()
    {
        // Check if the current scene is the loading scene
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "LoadingScene 1")
        {
            isLoadingScene = true;
        }

        // Get all the child objects of the parent object
        childObjects = new GameObject[parentObject.transform.childCount];
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            childObjects[i] = parentObject.transform.GetChild(i).gameObject;
        }

        // Activate a random child object
        ActivateRandomChildObject();
    }

    void Update()
    {
        // Check for keyboard input in the loading scene
        if (isLoadingScene && Input.GetKeyDown(previousKey))
        {
            // Deactivate the current child object
            childObjects[currentIndex].SetActive(false);

            // Calculate the index of the previous child object
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = childObjects.Length - 1;
            }

            // Activate the previous child object
            ActivateRandomChildObject();
        }
        else if (isLoadingScene && Input.GetKeyDown(nextKey))
        {
            // Deactivate the current child object
            childObjects[currentIndex].SetActive(false);

            // Calculate the index of the next child object
            currentIndex++;
            if (currentIndex >= childObjects.Length)
            {
                currentIndex = 0;
            }

            // Activate the next child object
            ActivateRandomChildObject();
        }
    }

    private void ActivateRandomChildObject()
    {
        // Get a random index that is different from the current index
        int randomIndex = Random.Range(0, childObjects.Length);
        while (randomIndex == currentIndex)
        {
            randomIndex = Random.Range(0, childObjects.Length);
        }

        // Activate the random child object
        childObjects[randomIndex].SetActive(true);

        // Update the current index
        currentIndex = randomIndex;
    }
}
