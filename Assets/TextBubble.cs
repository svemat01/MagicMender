using UnityEngine;
using TMPro;
using System.Collections;
using System;
using UnityEngine.XR;

public class TextBubble : MonoBehaviour
{
    public TextMeshProUGUI textBubble;
    public string[] messages; // Array of messages for each text bubble
    public float letterSpeed = 1f; // Speed at which letters are displayed
    public float delayBetweenTextBubbles = 1f; // Delay between text bubbles

    private int currentMessageIndex = 0;
    private string currentMessage;
    private bool isTyping = false;

    private void OnEnable()
    {
        if (currentMessageIndex < messages.Length)
        {
            ShowTextBubble();
        }
    }

    private void Update()
    {
        // Check if the text is still typing
        if (isTyping)
        {
            // Skip typing when the player clicks
            if (Input.GetMouseButtonDown(0))
            {
                SkipTyping();
            }
            return;
        }

        // Check if there are more text bubbles
        if (currentMessageIndex < messages.Length - 1  && (Input.GetMouseButtonDown(0)))
        {
            // Wait for a delay before showing the next text bubble
         NextTextBubble();
        }
        else
        {
            // No more messages, close the panel on left-click
            if (Input.GetMouseButtonDown(0))
            {
              
                ShowTutorial.instance.toggleTutorial();

                currentMessageIndex= 1;
                 
            }
        }
    }

    void ShowTextBubble()
    {
        if (currentMessageIndex < messages.Length)
        {
            // Reset text
            textBubble.text = "";
            currentMessage = messages[currentMessageIndex];


            StartCoroutine(TypeText());

            Debug.Log("Showing text bubble " + currentMessageIndex);
        }
    }

    IEnumerator TypeText()
    {
        isTyping = true;

        foreach (char letter in currentMessage)
        {
            textBubble.text += letter;
            yield return new WaitForSeconds(letterSpeed);
        }

        isTyping = false;
    }

    void SkipTyping()
    {
        // Skip the typing animation and show the full text
        StopAllCoroutines();
        textBubble.text = currentMessage;
        isTyping = false;
    }

    void NextTextBubble()
    {
       // yield return new WaitForSeconds(delayBetweenTextBubbles);
        currentMessageIndex++;
        ShowTextBubble();
    }
}
