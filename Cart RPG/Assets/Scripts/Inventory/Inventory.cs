using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private GameObject slotPanel;
    private JsonDatabase _jsonDatabase;
    public GameObject UI;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public int slotAmount = 12;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        _jsonDatabase = GameObject.Find("Game").GetComponent<JsonDatabase>();
        //inventoryPanel = GameObject.Find("PlayerInventory");
        if (transform.Find("SlotContainer").gameObject == null)
            Debug.Log(name + ": no child object called SlotContainer");

        slotPanel = transform.Find("SlotContainer").gameObject;

        Init(slotAmount);
        //AddItem() here for testing
        //AddItem(0, 0);
        //AddItem(0, 4);
        //AddItem(1, 2);

        transform.gameObject.SetActive(false);

    }

    public void Init(int slotAmount)
    {
        this.slotAmount = slotAmount;

        if (slotPanel.transform.childCount > 0)
        {
            foreach (Transform child in slotPanel.transform)
            {
                Destroy(child.gameObject);
            }
        }

        items = new List<Item>();
        slots = new List<GameObject>();

        for (int i = 0; i < this.slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].transform.localScale = new Vector3(1, 1, 1);
            slots[i].GetComponent<InventorySlot>().id = i;
        }

    }

    public void AddItem(int id)
    {
        Item itemToAdd = _jsonDatabase.GetItemById(id);
        if (itemToAdd.Stackable && isInInventory(itemToAdd.Id))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id == -1)
                {

                    items[i] = itemToAdd;
                    GameObject itemObject = Instantiate(inventoryItem);

                    ItemData data = itemObject.GetComponent<ItemData>();
                    data.item = itemToAdd;
                    data.amount = 1;
                    data.slot = i;

                    itemObject.transform.SetParent(slots[i].transform);
                    itemObject.transform.localScale = new Vector3(1, 1, 1);
                    itemObject.transform.position = Vector2.zero;
                    itemObject.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObject.name = itemToAdd.Title;
                    break;
                }
            }
        }
    }
    public void AddItem(int id, int amount)
    {
        Item addItem = _jsonDatabase.GetItemById(id);
        List<Item> itemsToAdd = new List<Item>();
        for (int addIndex = 0; addIndex < amount; addIndex++)
        {
            itemsToAdd.Add(addItem);
        }

        foreach (Item itemToAdd in itemsToAdd)
        {
            if (itemToAdd.Stackable && isInInventory(itemToAdd.Id))
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Id == id)
                    {
                        ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                        data.amount++;
                        data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Id == -1)
                    {

                        items[i] = itemToAdd;
                        GameObject itemObject = Instantiate(inventoryItem);

                        ItemData data = itemObject.GetComponent<ItemData>();
                        data.item = itemToAdd;
                        data.amount = 1;
                        data.slot = i;

                        itemObject.transform.SetParent(slots[i].transform);
                        itemObject.transform.localScale = new Vector3(1, 1, 1);
                        itemObject.transform.position = Vector2.zero;
                        itemObject.GetComponent<Image>().sprite = itemToAdd.Sprite;
                        itemObject.name = itemToAdd.Title;
                        break;
                    }
                }
            }
        }
    }

    public List<int> getItemAmounts()
    {
        List<int> amounts = new List<int>();
        for (int i = 0; i < items.Count; i++)
        {
            try
            {
                amounts.Add(slots[i].transform.GetChild(0).GetComponent<ItemData>().amount);
            }
            catch
            {
                amounts.Add(0);
            }

        }
        return amounts;
    }

    public void LoadItemsFromDatabase(int id)
    {
        Init(slotAmount);
        Storage storage = _jsonDatabase.GetStorage(id);
        foreach (var item in storage.items)
        {
            AddItem(item.Key, item.Value);
        }
    }

    private bool isInInventory(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Id == id)
            {
                return true;
            }
        }
        return false;
    }
}
