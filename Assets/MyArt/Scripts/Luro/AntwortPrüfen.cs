using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckAnswer : MonoBehaviour
{
    public TMP_InputField TMP_InputField;  
    public TMP_Text feedbackText;      
    public string correctAnswer = "Oida grantler"; 

    public void CheckInput()
    {
        if (TMP_InputField.text.Trim().ToLower() == correctAnswer.ToLower()) 
        {
            feedbackText.text = "Richtig!";
            feedbackText.color = Color.green;
        }
        else
        {
            feedbackText.text = "Falsch! Versuche es erneut.";
            feedbackText.color = Color.red;
        }
    }
}
