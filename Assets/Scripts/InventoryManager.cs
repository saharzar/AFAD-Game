using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public Transform[] slots;

    public void AddItem(GameObject item)
    {
        foreach (Transform slot in slots)
        {
            if (slot.childCount == 0)
            {
                StartCoroutine(MoveToSlot(item, slot));
                return;
            }
        }

        Debug.Log("Inventory Full!");
    }

    IEnumerator MoveToSlot(GameObject item, Transform slot)
    {
        RectTransform itemRect = item.GetComponent<RectTransform>();

        Vector3 startPos = itemRect.position;
        Vector3 endPos = slot.position;

        float duration = 0.5f; // ?? speed (increase = slower)
        float time = 0;

        while (time < duration)
        {
            itemRect.position = Vector3.Lerp(startPos, endPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // Snap to final position
        itemRect.position = endPos;

        // Parent AFTER animation
        item.transform.SetParent(slot, false);
        item.transform.localPosition = Vector3.zero;
    }
}