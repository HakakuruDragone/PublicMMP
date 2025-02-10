using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public static WinScreen Instance;
    public GameObject winScreen;
    private int correctItems = 0;
    public int totalItemsRequired = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCorrectItem()
    {
        correctItems++;

        if (correctItems >= totalItemsRequired)
        {
            ActivateWinScreen();
        }
    }

    private void ActivateWinScreen()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }
    }
}