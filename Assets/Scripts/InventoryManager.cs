using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public Transform[] slots;
    public Transform canvasRoot; // ?? drag your Canvas here

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
        RectTransform rect = item.GetComponent<RectTransform>();

        Vector3 startPos = rect.position;
        Vector3 endPos = slot.position;

        float duration = 0.5f;
        float time = 0;

        // ?? STEP 1: move to top canvas so it's visible
        item.transform.SetParent(canvasRoot, true);

        while (time < duration)
        {
            rect.position = Vector3.Lerp(startPos, endPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // ?? STEP 2: snap into slot AFTER animation
        item.transform.SetParent(slot, false);
        rect.localPosition = Vector3.zero;
    }
}