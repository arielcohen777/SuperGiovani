using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTN_s : MonoBehaviour
{
    public void LoadGame()
    {
 
        SceneManager.LoadScene(1);
        Debug.Log("Resume Game");
    }

    public void Quit()
    {

        Application.Quit();
    }
}
