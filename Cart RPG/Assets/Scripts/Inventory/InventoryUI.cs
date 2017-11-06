using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    [SerializeField]
    private GameObject itemContainer;

    private GameObject defaultItemSlot;

    // Use this for initialization
    void Start() {
        defaultItemSlot = itemContainer.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update() {

    }

    void CloseInventory() {
        GetComponent<Canvas>().enabled = false;
        foreach (Transform itemSlot in itemContainer.transform) {
        }
    }

    public void LoadInventory(List<Item> items) {
        int i = 0;
        foreach (Transform itemSlot in itemContainer.transform) {
            if (items[i] != null) {
                itemSlot.GetComponent<Image>().sprite = (Sprite)Resources.Load(items[i].SpritePath);
                itemSlot.GetComponentInChildren<Text>().text = items[i].Title;
                i++;
                continue;
            }
            itemSlot.GetComponent<Image>().sprite = defaultItemSlot.GetComponent<Image>().sprite;
            itemSlot.GetComponentInChildren<Text>().text = defaultItemSlot.GetComponentInChildren<Text>().text;
        }
        //foreach (item in items) {

        //}
    }
}
