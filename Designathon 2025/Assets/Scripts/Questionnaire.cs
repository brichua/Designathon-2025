using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;
using Firebase;
using Firebase.Analytics;
using Firebase.Firestore;
using TMPro;

public class Questionnaire : MonoBehaviour
{
    public bool isYesSelected = false;
    public bool isNoSelected = false;
    public bool isMaybeSelected = false;

    private const string HexColorCode = "#CEF7D4";
    private Color selectedColor;

    private const string unSelectedHexColorCode = "#FFFFFF";
    private Color unselectedColor;

    public Image yesButtonImage;
    public Image noButtonImage;
    public Image maybeButtonImage;
    public void CheckboxClicked()
    {
        if (yesButtonImage != null)
        {
            unselectedColor = yesButtonImage.color;
        }
        if (ColorUtility.TryParseHtmlString(HexColorCode, out selectedColor))
        {
            Debug.Log("Custom color loaded successfully.");
        }
        else
        {
            // Fallback if the hex code is invalid
            selectedColor = Color.red;
            Debug.LogError("Failed to parse custom color, using RED as fallback.");
        }
    }

    // Resets all selections and sets the color of all buttons back to the unselected color
    private void ResetSelections()
    {
        isYesSelected = false;
        isNoSelected = false;
        isMaybeSelected = false;

        // Reset the background color of all buttons
        yesButtonImage.color = unselectedColor;
        noButtonImage.color = unselectedColor;
        maybeButtonImage.color = unselectedColor;
    }

    // --- Public Methods called by the Buttons ---
    public void SelectYes()
    {
        ResetSelections(); // Uncheck all others first

        isYesSelected = true;
        yesButtonImage.color = selectedColor; // Change the YES button's background color

        Debug.Log("YES selected. isYesSelected is TRUE.");
    }

    public void SelectNo()
    {
        ResetSelections(); // Uncheck all others first

        isNoSelected = true;
        noButtonImage.color = selectedColor; // Change the NO button's background color

        Debug.Log("NO selected. isNoSelected is TRUE.");
    }

    public void SelectMaybe()
    {
        ResetSelections(); // Uncheck all others first

        isMaybeSelected = true;
        maybeButtonImage.color = selectedColor; // Change the MAYBE button's background color

        Debug.Log("MAYBE selected. isMaybeSelected is TRUE.");
    }
}