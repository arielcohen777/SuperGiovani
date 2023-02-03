using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    private GameManager gm;
    private Health healthRef;
    public Image fillImage;
    private Slider slider;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        gm = GameManager.Instance;
        healthRef = gm.player.GetComponent<Health>();
    }

    /*void UpdateSliders()
    {
        if (slider.CompareTag("HealthUIBar"))
            HealthSlider();
        else if (slider.CompareTag("ArmorUIBar"))
            ArmorSlider();
    }*/

    public void HealthSlider()
    {
        float fill = healthRef.health / healthRef.maxHealth;
        slider.value = fill;
        ImageEnabling();
    }

    public void ArmorSlider()
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
