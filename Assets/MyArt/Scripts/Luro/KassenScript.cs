using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KassenScript : MonoBehaviour, IDropHandler
{
    private Image dropZoneImage;
    private Color originalColor;
    public TMP_Text ammount_text;
    public float ammount;
    public GameObject KassenspielWinScreen;

    private void Awake()
    {
        dropZoneImage = GetComponent<Image>();
        if (dropZoneImage != null)
        {
            originalColor = dropZoneImage.color;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject fallenGelassen = eventData.pointerDrag;
        if (fallenGelassen == null) return;
        
        Coins coin = fallenGelassen.GetComponent<Coins>();
        if (coin == null || coin.transform.parent == transform) return;

        if (dropZoneImage != null)
        {
            dropZoneImage.color = Color.green;
            Invoke(nameof(ResetColor), 0.5f);
        }
    }

    public void RemoveCoin(Coins coin)
    {
        if (coin == null || coin.transform.parent != transform) return;
        ammount -= coin.value;
        updateAmmount();
    }

    public void AddCoin(Coins coin)
    {
        if (coin == null) return;
        ammount += coin.value;
        updateAmmount();
    }

    private void updateAmmount()
    {
        if (ammount_text != null)
        {
            ammount_text.text = ammount.ToString("F2");
        }

        if (Math.Abs(ammount - 5.0) < 0.02)
        {
            ActivateWinScreen();
        }
    }

    private void ResetColor()
    {
        if (dropZoneImage != null)
        {
            dropZoneImage.color = originalColor;
        }
    }

    private void ActivateWinScreen()
    {
        if (KassenspielWinScreen != null )
        {
            KassenspielWinScreen.SetActive(true);
        }
    }
}