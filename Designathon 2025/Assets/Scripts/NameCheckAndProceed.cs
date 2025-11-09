using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;
using Firebase;
using Firebase.Analytics;
using Firebase.Firestore;
using TMPro;

public class NameCheckAndProceed : MonoBehaviour
{
    public TMP_InputField nameInputField; // The box where the user types
    public GameObject whiteBoxImage;      // The sprite box to show
    public TextMeshProUGUI errorMessageText;
    public GameObject personNameBox;
    public GameObject character;
    public GameObject nextButton;
    public GameObject nextButton2;
    public GameObject petNameInput;
    public Questionnaire questionnaire;

    // --- Method called by the Next Button ---
    public void OnNextButtonClicked()
    {
        if (whiteBoxImage == null)
        {
            Debug.LogError("White Box Image reference is missing. Check the Inspector!");
            return; // Stop execution
        }

        // Check 2: Check if the name input field is empty or contains only whitespace
        // We use .Trim() to remove any leading/trailing spaces before checking for emptiness.
        if (nameInputField != null && string.IsNullOrWhiteSpace(nameInputField.text))
        {
            Debug.LogWarning("Name field is empty. Please enter a name to proceed.");

            // Show an error message to the user
            if (errorMessageText != null) 
            {    
                errorMessageText.text = "Please enter a valid character name.";
            }

            return; // Stop execution if the name is empty
        }

        // If the checks pass, proceed:
        // 1. Activate the White Box Image, making it visible.
        whiteBoxImage.SetActive(true);
        errorMessageText.text = null; // Clear any previous error message
        nameInputField.text = null; // Clear input field if needed
        personNameBox.SetActive(false); // Hide the name input box
        character.SetActive(false);
        nextButton.SetActive(false);
        nextButton2.SetActive(true);
        petNameInput.SetActive(true);
        questionnaire.UpdateCharacterIcon(new QuestionData("Do you feel pain anywhere?",
                new List<string>{ "Yes, my head hurts", "Yes, my arm hurts", "Yes, my legs hurt", "Yes, my stomach hurts", "Yes, something hurts", "No, I feel fine everywhere" },
                allowsMultiple:true,
                head: Resources.Load<Sprite>("dizzy face"),
                body: Resources.Load<Sprite>("dizzy hoodie"),
                eyes: Resources.Load<Sprite>("dizzy eye"),
                hairVariants: new List<Sprite>
                {
                    Resources.Load<Sprite>("dizzy hair 7"),
                    Resources.Load<Sprite>("dizzy hair 6"),
                    Resources.Load<Sprite>("dizzy hair 5"),
                    Resources.Load<Sprite>("dizzy hair 4"),
                    Resources.Load<Sprite>("dizzy hair 3"),
                    Resources.Load<Sprite>("dizzy hair 2"),
                    Resources.Load<Sprite>("dizzy hair 1"),
                },
                template : Resources.Load<Sprite>("dizzy")));

        Debug.Log($"Name entered: {nameInputField.text}. White Box is now visible.");
    }
}