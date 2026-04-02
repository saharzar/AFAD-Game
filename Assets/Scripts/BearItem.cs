using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class BearItem : MonoBehaviour, IPointerClickHandler
{
    public InventoryManager inventoryManager;

    private bool isCollected = false;

    private Transform originalParent;
    private Vector3 originalLocalPosition;

    void Start()
    {
        // Save correct UI position
        originalParent = transform.parent;
        originalLocalPosition = transform.localPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isCollected)
        {
            inventoryManager.AddItem(gameObject);
            isCollected = true;
        }
        else
        {
            StartCoroutine(MoveBack());
            isCollected = false;
        }
    }

    IEnumerator MoveBack()
    {
        RectTransform rect = GetComponent<RectTransform>();

        Vector3 startPos = rect.position;

        // Temporarily move under original parent BEFORE animating
        transform.SetParent(originalParent, false);

        Vector3 endPos = ((RectTransform)originalParent).TransformPoint(originalLocalPosition);

        float duration = 0.5f;
        float time = 0;

        while (time < duration)
        {
            rect.position = Vector3.Lerp(startPos, endPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // Snap perfectly
        transform.localPosition = originalLocalPosition;
    }
}