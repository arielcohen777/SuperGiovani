using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    private GameManager gm;
    private Health healthRef;

    [SerializeField] private Image healthFillImage;
    [SerializeField] private Image armorFillImage;

    [SerializeField] private Slider armorSlider;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        gm = GameManager.Instance;
        healthRef = gm.player.GetComponent<Health>();
    }

    public void HealthSlider()
    {
        float fill = healthRef.health / healthRef.maxHealth;
        healthSlider.value = fill;
        if (healthSlider.value <= healthSlider.minValue)
            healthFillImage.enabled = false;
        else if (healthSlider.value > healthSlider.minValue && !healthFillImage.enabled)
            healthFillImage.enabled = true;
    }

    public void ArmorSlider()
    {
        float fill = healthRef.armor / healthRef.maxArmor;
        armorSlider.value = fill;
        if (armorSlider.value <= armorSlider.minValue)
            armorFillImage.enabled = false;
        else if (armorSlider.value > healthSlider.minValue && !armorFillImage.enabled)
            armorFillImage.enabled = true;
    }

}
