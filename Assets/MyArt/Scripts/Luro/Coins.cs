using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Coins : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private Transform startParent;

    [SerializeField] private string dropZoneName;
    [SerializeField] public float value;
    private bool isInsideDropZone = false;
    public bool isInsideKasse = false;

    private KassenScript kassen; 

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
        startPosition = rectTransform.position;
        startParent = transform.parent;
        
        kassen = FindObjectOfType<KassenScript>();
        if (kassen == null)
        {
            Debug.LogError("KassenScript not found in the scene!");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        GameObject droppedOn;
        if (eventData.pointerEnter != null)
        {
            droppedOn = eventData.pointerEnter.gameObject;
        }
        else
        {
            droppedOn = null;
        }

        if (droppedOn != null && droppedOn.name == dropZoneName)
        {
            transform.SetParent(droppedOn.transform);
            isInsideDropZone = true;
            
            if (kassen != null && !isInsideKasse)
            {
                kassen.AddCoin(this); // Ensure the coin gets added
            }
            isInsideKasse = true;
            Debug.Log(isInsideDropZone);
            Debug.Log(isInsideKasse);
        }
        else
        {
            isInsideDropZone = false;
            if (isInsideKasse)
            {
                kassen.RemoveCoin(this);
            }
            
            
            isInsideKasse = false;
            transform.SetParent(startParent);
            ResetPosition();
            Debug.Log(isInsideDropZone);
            Debug.Log(isInsideKasse);
        }
    }

    public void ResetPosition()
    {
        rectTransform.position = startPosition;
    }

    public IEnumerator ShakeObject()
    {
        Vector3 originalPosition = transform.position;
        float duration = 0.3f;
        float magnitude = 10f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float xOffset = Random.Range(-1f, 1f) * magnitude;
            transform.position = originalPosition + new Vector3(xOffset, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }

}
