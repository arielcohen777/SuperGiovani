using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTN_s : MonoBehaviour
{


    public void KeyPress()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
        
            Debug.Log("Restart");
            //SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
       
            Debug.Log("Quit");
            // Application.Quit();
        }
    }
    public void Restart()
    {
        //SceneManager.LoadScene(1);
        Debug.Log("Restart");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        //Application.Quit();
    }
    public void LoadGame()
    {
        // Load the next scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    
    public void Test() {
        Debug.Log("Test DeathBtn"); 
    }
    
}
