using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler
{
    public int id;
    private Inventory inventory;
    private Inventory originalInventory;

    // Use this for initialization
    void Start()
    {
        inventory = transform.parent.parent.GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        originalInventory = droppedItem.inventory;

        Debug.Log(originalInventory.name + " -> " + inventory.name);

        if (inventory.items[id].Id == -1)
        {
            //clear item's original slot
            originalInventory.items[droppedItem.slot] = new Item();

            //Give item new slot
            inventory.items[id] = droppedItem.item;
            droppedItem.slot = id;
        }
        else if (droppedItem.slot != id)
        {

            //Put item in slot
            Transform item = this.transform.GetChild(0);
            droppedItem.inventory = inventory;
            item.GetComponent<ItemData>().slot = droppedItem.slot;
            item.transform.SetParent(inventory.slots[droppedItem.slot].transform);
            item.transform.position = inventory.slots[droppedItem.slot].transform.position;


            droppedItem.slot = id;
            droppedItem.transform.SetParent(transform);
            droppedItem.transform.position = transform.position;
            RectTransform slotRect = transform.GetComponent<RectTransform>();
            droppedItem.GetComponent<RectTransform>().rect.Set(0, 0, slotRect.rect.width, slotRect.rect.height);

            inventory.items[droppedItem.slot] = item.GetComponent<ItemData>().item;
            inventory.items[id] = droppedItem.item;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ItemData draggedItem = eventData.pointerDrag.GetComponent<ItemData>();
            draggedItem.Target = inventory;
        }
    }
}
