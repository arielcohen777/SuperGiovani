using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    public Health healthRef;
    public Image fillImage;
    private Slider slider;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.CompareTag("HealthUIBar"))
            HealthSlider();
        else
            ArmorSlider();

    }

    private void HealthSlider()
    {
        float fill = healthRef.health / healthRef.maxHealth;
        slider.value = fill;
        ImageEnabling();
    }

    private void ArmorSlider()
    {
        float fill = healthRef.armor / healthRef.maxArmor;
        slider.value = fill;
        ImageEnabling();
    }

    private void ImageEnabling()
    {
        if (slider.value <= slider.minValue)
            fillImage.enabled = false;
        if (slider.value > slider.minValue && !fillImage.enabled)
            fillImage.enabled = true;

    }
}
