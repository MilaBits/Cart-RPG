using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

class HotbarController : MonoBehaviour
{
    private GameObject GameUI;
    private UIController uiController;
    private GameObject itemContainer;
    private GameObject selectedItem;

    private Color defaultColor;
    private Color highlightColor;
    private ModulePlacer modulePlacer;

    private int itemCount;
    private int itemIndex;

    private Transform previousItem;
    private GameObject HeldItem;

    public HotbarMode hotbarMode;

    void Start()
    {
        uiController = GameObject.Find("UI").GetComponent<UIController>();
        itemContainer = GameObject.Find("HotbarItems");
        selectedItem = GameObject.Find("SelectedItem");
        modulePlacer = GameObject.Find("Player").GetComponent<ModulePlacer>();

        defaultColor = itemContainer.transform.GetChild(0).GetComponent<Image>().color;
        highlightColor = new Color(1f, 0.875f, 0.612f);
        previousItem = itemContainer.transform.GetChild(0);
        HeldItem = GameObject.Find("Hand");

        Init();
    }

    void Update()
    {
        if (hotbarMode == HotbarMode.BuildMode)
        {
            itemCount = modulePlacer.Modules.Length;
        }
        else
        {
            itemCount = uiController.PlayerInventory.items.Count(i => i.Id != -1);
        }

        updateCurrentItem();
    }

    private void Init()
    {
        itemIndex = 0;
        UpdateHighlight();
        UpdateText();
        UpdateHand(itemIndex);
    }
    private void UpdateHand(int i)
    {
        if (hotbarMode == HotbarMode.BuildMode)
        {
            HeldItem.GetComponent<MeshFilter>().mesh = modulePlacer.Modules[i].GetComponent<MeshFilter>().sharedMesh;
            HeldItem.GetComponent<MeshRenderer>().materials = modulePlacer.Modules[i].GetComponent<MeshRenderer>().sharedMaterials;
            return;
        }
        if (hotbarMode == HotbarMode.InventoryMode)
        {
            string path = uiController.PlayerInventory.items[i].SpritePath;
            if (Resources.Load<Mesh>("Models/Items/" + path) == null)
            {
                path = "NoModel";
            }
            HeldItem.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("Models/Items/" + path);
            HeldItem.GetComponent<MeshRenderer>().materials = Resources.Load<MeshRenderer>("Models/Items/" + path).sharedMaterials;
        }
    }

    void updateCurrentItem()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            int max = itemCount - 1;
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (itemIndex < max)
                {
                    itemIndex++;
                }
                else
                {
                    itemIndex = 0;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (itemIndex > 0)
                {
                    itemIndex--;
                }
                else
                {
                    itemIndex = max;
                }
            }

            UpdateHighlight();
            UpdateText();
            UpdateHand(itemIndex);
        }
    }

    private void UpdateHighlight()
    {
        // Update UI to show what item is picked
        previousItem.GetComponent<Image>().color = defaultColor;
        itemContainer.transform.GetChild(itemIndex).GetComponent<Image>().color = highlightColor;
        previousItem = itemContainer.transform.GetChild(itemIndex);
    }

    private void UpdateText()
    {
        if (itemContainer.transform.GetChild(itemIndex).childCount < 1)
        {
            selectedItem.transform.GetChild(0).GetComponent<Text>().text = "    ";
            return;
        }

        if (hotbarMode == HotbarMode.BuildMode)
        {
            selectedItem.transform.GetChild(0).GetComponent<Text>().text = modulePlacer.Modules[itemIndex].GetComponent<CartModule>().Name;
        }
        else
        {
            selectedItem.transform.GetChild(0).GetComponent<Text>().text = uiController.PlayerInventory.items[itemIndex].Title;
        }
    }
}
public enum HotbarMode { BuildMode, InventoryMode }
