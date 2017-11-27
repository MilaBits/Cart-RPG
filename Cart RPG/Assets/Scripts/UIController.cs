using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    GameObject hotBarContainer;

    // Use this for initialization
    void Start()
    {
        //UI Components
        PlayerWindow = transform.Find("PlayerWindow").gameObject;
        ObjectWindow = transform.Find("ObjectWindow").gameObject;
        BuildUI = transform.Find("BuildUI").gameObject;
        GameUI = transform.Find("GameUI").gameObject;

        hotBarContainer = GameObject.Find("HotbarItems");

        //Inventories
        JsonDatabase = GameObject.Find("Game").GetComponent<JsonDatabase>();
        PlayerInventory = PlayerWindow.GetComponent<Inventory>();
        ObjectInventory = ObjectWindow.GetComponent<Inventory>();


        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(Resources.Load<Texture2D>("Sprites/UI/cursorHand_beige"), new Vector2(1, 1), CursorMode.Auto);

    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && GameUI != null)
        {
            if (GameUI.activeSelf) Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
        }
    }

    public void HidePlayerInventory()
    {
        //save items
        JsonDatabase.SaveItemsToStorage(playerStorageId, PlayerInventory.items, PlayerInventory.getItemAmounts());
        //hide inventory
        PlayerWindow.SetActive(false);
        GameUI.SetActive(true);
        UpdateHotbarItems();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void HideObjectInventory()
    {
        //save items
        JsonDatabase.SaveItemsToStorage(objectStorageId, ObjectInventory.items, ObjectInventory.getItemAmounts());
        //hide inventory
        ObjectWindow.SetActive(false);
        GameUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowPlayerInventory()
    {
        //show inventory
        PlayerWindow.SetActive(true);
        //Load items
        PlayerInventory.LoadItemsFromDatabase(playerStorageId);
        GameUI.SetActive(false);
        BuildUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
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
        BuildUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Opened storage object with storageId: " + objectStorageId);
    }

    public void ToggleBuildUI()
    {
        if (BuildUI.activeSelf)
        {
            GameObject.Find("ModeImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/person");
        }
        else
        {
            GameObject.Find("ModeImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/hammer");
        }
        BuildUI.SetActive(!BuildUI.activeSelf);
    }

    public void UpdateHotbarItems()
    {
        foreach (Transform slot in hotBarContainer.transform)
        {
            if (slot.childCount > 0)
                Destroy(slot.GetChild(0).gameObject);
        }
        for (int i = 0; i < PlayerInventory.slots.Count; i++)
        {
            if (PlayerInventory.slots[i].transform.childCount > 0)
            {
                GameObject item = Instantiate(PlayerInventory.slots[i].transform.GetChild(0).gameObject);
                item.transform.SetParent(hotBarContainer.transform.GetChild(i));
                item.transform.localScale = new Vector3(1, 1, 1);
                item.transform.position = PlayerInventory.slots[i].transform.position;
                item.transform.localPosition = PlayerInventory.slots[i].transform.localPosition;
                RectTransform rectTransform = new RectTransform();
                item.GetComponent<RectTransform>().rect.Set(0, 0, 30, 30);
            }
        }
    }

    public void ToggleBuildUI(bool show)
    {
        BuildUI.SetActive(show);
        if (show)
        {
            GameObject.Find("ModeImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/hammer");
            return;
        }
        GameObject.Find("ModeImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/person");
    }
}