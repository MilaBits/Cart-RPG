using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    private GameObject inventoryPanel;
    private GameObject slotPanel;
    private ItemDatabase itemDatabase;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public int slotAmount = 12;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    void Start() {
        itemDatabase = GameObject.Find("Game").GetComponent<ItemDatabase>();
        inventoryPanel = GameObject.Find("PlayerInventory");
        slotPanel = inventoryPanel.transform.Find("SlotContainer").gameObject;

        for (int i = 0; i < slotAmount; i++) {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
        }

        AddItem(0);
        AddItem(1);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);

        //Debug.Log(slots[0].transform.GetChild(0).GetComponent<ItemData>() + " itemData.item: " + slots[0].transform.GetChild(0).GetComponent<ItemData>().Item.Title);
        //Debug.Log(slots[1].transform.GetChild(0).GetComponent<ItemData>() + " itemData.item: " + slots[1].transform.GetChild(0).GetComponent<ItemData>().Item.Title);
    }

    public void AddItem(int id) {
        Item itemToAdd = itemDatabase.GetItemById(id);
        if (itemToAdd.Stackable && isInInventory(itemToAdd.Id)) {
            for (int i = 0; i < items.Count; i++) {
                if (items[i].Id == id) {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        } else {
            for (int i = 0; i < items.Count; i++) {
                if (items[i].Id == -1) {

                    items[i] = itemToAdd;
                    GameObject itemObject = Instantiate(inventoryItem);

                    itemObject.transform.SetParent(slots[i].transform);
                    itemObject.transform.position = Vector2.zero;
                    itemObject.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObject.name = itemToAdd.Title;

                    //itemObject.GetComponent<ItemData>().Item = itemToAdd;
                    itemObject.GetComponent<ItemData>().item = itemToAdd;
                    //Debug.Log(itemObject.GetComponent<ItemData>() + " itemData.item: " + itemObject.GetComponent<ItemData>().Item.Title);
                    break;
                }
            }
        }
    }

    private bool isInInventory(int id) {
        for (int i = 0; i < items.Count; i++) {
            if (items[i].Id == id) {
                return true;
            }
        }
        return false;
    }
}
