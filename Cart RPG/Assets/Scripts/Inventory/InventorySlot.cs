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
            originalInventory.items[droppedItem.slot] = new Item(); // clear droppeditem's original slot
            inventory.items[id] = droppedItem.item;                 // Give item new slot
            droppedItem.slot = id;                                  // Set droppeditem's slot to be this slot
        }
        else if (droppedItem.slot != id)
        {
            //TODO: Fix issue where when swapping items the dragged(droppedItem) item overrides the current item (after saving/reloading the inventory, id is saved wrongly in database.

            //Move current item to dropped item's old slot
            ItemData currentItem = this.transform.GetChild(0).gameObject.GetComponent<ItemData>();      // Item currently in this slot
            currentItem.inventory = droppedItem.inventory;                                              // Set current item's inventory to droppeditem's old inventory
            currentItem.slot = droppedItem.slot;                                                        // Set current item's slot to droppeditem's old slot
            currentItem.transform.SetParent(droppedItem.inventory.slots[droppedItem.slot].transform);   // Set current item's parent to droppeditem's old parent
            droppedItem.inventory.items[droppedItem.slot] = currentItem.item;                           // Put current item's item in droppeditem's old inventory's item list
                                     //TODO ^^^^^^^^ changed from id to droppedItem.slot fixed it HA


            //Put dropped item in this slot
            droppedItem.slot = id;                                  // Set droppeditem's slot to this slot's id
            droppedItem.transform.SetParent(transform);             // Set droppeditem's parent to this slot
            droppedItem.inventory = inventory;                      // Set droppeditem's inventory to be this slot's inventory
            inventory.items[droppedItem.slot] = droppedItem.item;   // Put droppeditem's item in this slot's inventory's item list
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

