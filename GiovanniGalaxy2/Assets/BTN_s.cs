using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTN_s : MonoBehaviour
{
    public void Restart()
    {
        
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {

        Application.Quit();
    }
}
