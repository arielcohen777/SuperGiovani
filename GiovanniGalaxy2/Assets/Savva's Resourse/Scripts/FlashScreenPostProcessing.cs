using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FlashScreenPostProcessing : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private Vignette vignette;

    private float targetIntensity;
    private float currentIntensity;
    public float flashSpeed = 1f;

    public float flashDuration = 2f;

    private void Start()
    {
        postProcessVolume.profile.TryGetSettings(out vignette);
        currentIntensity = vignette.intensity.value;
        targetIntensity = currentIntensity;
        vignette.color.value = Color.red;
    }

    private void Update()
    {
        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, flashSpeed * Time.deltaTime);
        vignette.intensity.value = currentIntensity;
    }

    public void FlashRed(float intensity)
    {
        targetIntensity += intensity;
        if (targetIntensity > 1f)
        {
            targetIntensity = 1f;
        }
        StartCoroutine(ReduceIntensityAfterDelay(flashDuration));
    }

    IEnumerator ReduceIntensityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        targetIntensity = 0f;
    }

    public void StopFlashing()
    {
        targetIntensity = 0f;
    }
}
