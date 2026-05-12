using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public int slotIndex; // 0,1,2 for Slot1, Slot2, Slot3

    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            // Snap the dragged button into this slot
            d.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            // Tell Level1Manager which action was placed
            Level1Manager manager = FindObjectOfType<Level1Manager>();
            manager.AddAction(eventData.pointerDrag.name, slotIndex);
        }
    }
}
