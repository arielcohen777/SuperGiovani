using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /**
     * 
     * Inside player setting in update you have to check if !PauseMenu.isPause then play game, move animations etc..
     * 
     **/
    public GameObject pauseMenu;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    
    public void PauseGame()
    {
    
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
 
        isPaused = true;
        Debug.Log("Paused Game");

    }
    public void ResumeGame() 
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        isPaused = false;
        Debug.Log("Resume Game");

    }
    public void goToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Main Menu ");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game Quit ");
    }

    public void Restart()
    {
        Debug.Log("Load Level ");
        SceneManager.LoadScene("Play Test Scene");
        
    }
}
