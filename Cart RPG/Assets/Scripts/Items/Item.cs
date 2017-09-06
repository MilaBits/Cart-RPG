using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class Item : MonoBehaviour {

    private string name;
    private string description;
    private int value;
    private ItemType itemType;

    public Item(string name, string description, int value, ItemType itemType) {

        this.name = name;
        this.description = description;
        this.value = value;
        this.itemType = itemType;
    }

    public string Name {
        get { return name; }
    }
    public string Description {
        get { return description; }
    }

    public int Value {
        get { return value; }
    }
    public ItemType ItemType {
        get { return itemType; }
    }
}

public enum ItemType { MeleeWeapon, RangedWeapon, MagicalWeapon, Misc, Consumable }
