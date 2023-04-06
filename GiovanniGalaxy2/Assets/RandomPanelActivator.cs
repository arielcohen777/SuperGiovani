using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomPanelActivator : MonoBehaviour
{
    public GameObject parentObject;
    public Button backgroundButton;

    void Start()
    {
        // Get all the child objects of the parent object
        GameObject[] childObjects = new GameObject[parentObject.transform.childCount];
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            childObjects[i] = parentObject.transform.GetChild(i).gameObject;
        }

        // Activate a random child object
        int randomIndex = Random.Range(0, childObjects.Length);
        childObjects[randomIndex].SetActive(true);


    }
}
