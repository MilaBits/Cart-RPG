using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //UI Components
    public GameObject PlayerWindow { get; private set; }
    public GameObject ObjectWindow { get; private set; }
    public GameObject BuildUI { get; private set; }
    public GameObject GameUI { get; private set; }

    //Inventories
    private int playerStorageId = 0;
    private int objectStorageId = 0;
    public JsonDatabase JsonDatabase { get; private set; }
    private Inventory PlayerInventory;
    private Inventory ObjectInventory;


    // Use this for initialization
    void Start()
    {
        //UI Components
        PlayerWindow = transform.Find("PlayerWindow").gameObject;
        ObjectWindow = transform.Find("ObjectWindow").gameObject;
        BuildUI = transform.Find("BuildUI").gameObject;
        GameUI = transform.Find("GameUI").gameObject;

        //Inventories
        JsonDatabase = GameObject.Find("Game").GetComponent<JsonDatabase>();
        PlayerInventory = PlayerWindow.GetComponent<Inventory>();
        ObjectInventory = ObjectWindow.GetComponent<Inventory>();

    }

    public void HidePlayerInventory()
    {
        //save items
        JsonDatabase.SaveItemsToStorage(playerStorageId, PlayerInventory.items, PlayerInventory.getItemAmounts());
        //hide inventory
        PlayerWindow.SetActive(false);
        GameUI.SetActive(true);
    }

    public void HideObjectInventory()
    {
        //save items
        JsonDatabase.SaveItemsToStorage(objectStorageId, ObjectInventory.items, ObjectInventory.getItemAmounts());
        //hide inventory
        ObjectWindow.SetActive(false);
        GameUI.SetActive(true);
    }

    public void ShowPlayerInventory()
    {
        //show inventory
        PlayerWindow.SetActive(true);
        //Load items
        PlayerInventory.LoadItemsFromDatabase(playerStorageId);
        GameUI.SetActive(false);
    }

    public void ShowObjectInventory(CartStorageModule objectStorage)
    {
        this.objectStorageId = objectStorage.StorageId;
        //show inventory
        ObjectWindow.SetActive(true);
        //Load items
        ObjectInventory.Init(objectStorage.Capacity);
        ObjectInventory.LoadItemsFromDatabase(objectStorageId);
        GameUI.SetActive(false);
    }
}