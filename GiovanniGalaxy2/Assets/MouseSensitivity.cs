using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    public Slider slider;
    public float sensitivity = 100f;

    void Start()
    {
        slider.value = sensitivity;
    }

    public void UpdateSensitivity(float value)
    {
        sensitivity = value;
    }

    void Update()
    {
        // Get the mouse movement from Input.GetAxis
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Rotate the camera based on the mouse movement
        transform.Rotate(-mouseY, mouseX, 0f);
    }
}
