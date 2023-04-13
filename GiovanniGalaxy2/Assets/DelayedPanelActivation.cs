using UnityEngine;

public class DelayedPanelActivation : MonoBehaviour
{
    public float delay = 5f;  // The delay before the panel appears
    public GameObject panel; // The panel to activate

    private bool isActive = false;

    void Start()
    {
        panel.SetActive(false);
        Invoke("ActivatePanel", delay);
    }

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Return))
        {
            int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }
    }

    void ActivatePanel()
    {
        isActive = true;
        panel.SetActive(true);
    }
}
