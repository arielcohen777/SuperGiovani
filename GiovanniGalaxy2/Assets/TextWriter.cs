using UnityEngine;
using TMPro;
using System.Collections;

public class TextWriter : MonoBehaviour
{
    public string[] paragraphs = new string[] {
        "You have successfully navigated through the challenging levels and defeated the monsters. " +
        "Giovani finally finds his way back home " +
        "eager to reunite with his family and share the story of his incredible journey through the vast and perilous expanse of space." };

    public TextMeshProUGUI promptText;
    public float wordDelay = 0.5f;
    public float paragraphDelay = 2f;

    private int currentParagraphIndex = 0;
    private string[] currentParagraphWords;
    private Coroutine writeCoroutine;

    void Start()
    {
        // Split first paragraph into words
        currentParagraphWords = paragraphs[0].Split(' ');

        // Start writing coroutine
        writeCoroutine = StartCoroutine(WriteText());
    }

    IEnumerator WriteText()
    {
        // Loop through words in current paragraph
        int index = 0;
        while (true)
        {
            // Build current sentence
            string currentSentence = "";
            for (int i = 0; i <= index; i++)
            {
                currentSentence += currentParagraphWords[i] + " ";
            }

            // Update text component with current sentence
            promptText.SetText(currentSentence);

            // Wait for word delay
            yield return new WaitForSeconds(wordDelay);

            // Increment index and reset if past end of paragraph
            index++;
            if (index >= currentParagraphWords.Length)
            {
                // Wait for paragraph delay
                yield return new WaitForSeconds(paragraphDelay);

                // Move to next paragraph
                currentParagraphIndex++;
                if (currentParagraphIndex >= paragraphs.Length)
                {
                    currentParagraphIndex = 0;
                }
                currentParagraphWords = paragraphs[currentParagraphIndex].Split(' ');

                index = 0;
            }
        }
    }
}
