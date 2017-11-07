using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public int id;
    private Inventory inventory;

    // Use this for initialization
    void Start() {
        inventory = transform.parent.parent.GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData) {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (inventory.items[id].Id == -1)
        {
            inventory.items[droppedItem.slot] = new Item();
            inventory.items[id] = droppedItem.item;
            droppedItem.slot = id;
        }
        else if (droppedItem.slot != id)
        {
            Transform item = this.transform.GetChild(0);
            item.GetComponent<ItemData>().slot = droppedItem.slot;
            item.transform.SetParent(inventory.slots[droppedItem.slot].transform);
            item.transform.position = inventory.slots[droppedItem.slot].transform.position;

            droppedItem.slot = id;
            droppedItem.transform.SetParent(transform);
            droppedItem.transform.position = transform.position;
            Rect slotRect = transform.GetComponent<Rect>();
            droppedItem.GetComponent<Rect>().Set(0,0,slotRect.width, slotRect.height);

            inventory.items[droppedItem.slot] = item.GetComponent<ItemData>().item;
            inventory.items[id] = droppedItem.item;
        }
    }
}
