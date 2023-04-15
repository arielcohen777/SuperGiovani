using UnityEngine;
using System.Collections;
using TMPro;

public class StarWarsText : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float scrollSpeed = 1.0f;
    public float fadeSpeed = 1.0f;
    public float skewAngle = -12f; // new public field for skew angle
    private RectTransform rectTransform;
    private Color textColor;
    private bool playedAnimation = false;

    void Start()
    {
        rectTransform = textComponent.GetComponent<RectTransform>();
        textColor = textComponent.color;

        // Set shearing to create slant effect based on public field
        rectTransform.localEulerAngles = new Vector3(0f, 0f, skewAngle);
    }

    void Update()
    {
        if (!playedAnimation)
        {
            StartCoroutine(ScrollText());
            playedAnimation = true;
        }
    }

    IEnumerator ScrollText()
    {
        float startTime = Time.time;
        float canvasHeight = GetComponentInParent<Canvas>().pixelRect.height;
        float textHeight = textComponent.preferredHeight;
        float targetY = canvasHeight + textHeight;

        while (rectTransform.anchoredPosition.y < targetY)
        {
            rectTransform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
            yield return null;
        }

        while (textColor.a > 0.0f)
        {
            float t = (Time.time - startTime) * fadeSpeed;
            textColor.a = Mathf.Lerp(1.0f, 0.0f, t);
            textComponent.color = textColor;
            yield return null;
        }

        Destroy(gameObject);
    }
}
