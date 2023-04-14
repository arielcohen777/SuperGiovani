using UnityEngine;
using TMPro;
using System.Collections;

public class TextWriter : MonoBehaviour
{
    public string[] paragraphs = new string[] {
        " You ever have that feeling where you’re not sure if you’re awake or still dreaming?",
        "The Matrix is everywhere. It is all around us. Even now, in this very room",
        "You can see it when you look out your window or when you turn on your television",
        "You can feel it when you go to work... when you go to class... when you have to do school assignments. It is the world that has been pulled over your eyes to blind you from the truth.",
        "There's a difference between knowing the path and walking the path.",
        "Have you ever stood and stared at it, marveled at its beauty, its genius? Billions of people just living out their lives, oblivious.",
        "The answer is out there, and it's looking for you, and it will find you if you want it to." };

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
