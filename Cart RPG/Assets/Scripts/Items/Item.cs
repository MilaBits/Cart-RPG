using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine.UI;
using UnityEngine;

public class Item {
    [SerializeField]
    private int id;
    [SerializeField]
    private string title;
    [SerializeField]
    private string description;
    [SerializeField]
    private string spritePath;
    [SerializeField]
    private int itemValue;
    [SerializeField]
    private ItemType itemType;
    [SerializeField]
    private bool stackable;
    [SerializeField]
    private Sprite sprite;

    public Item(int id, string title, string description, int value, ItemType itemType, string spritePath, bool stackable) {

        this.id = id;
        this.title = title;
        this.description = description;
        this.itemValue = value;
        this.itemType = itemType;
        this.spritePath = spritePath;
        this.stackable = stackable;
        this.sprite = Resources.Load<Sprite>("Sprites/Items/" + spritePath);
    }
    public Item(string title, string description, int value, ItemType itemType, string spritePath, bool stackable) {
        this.title = title;
        this.description = description;
        this.itemValue = value;
        this.itemType = itemType;
        this.spritePath = spritePath;
        this.stackable = stackable;
    }

    public Item() {
        this.id = -1;
    }

    public int Id {
        get { return id; }
        private set { id = value; }
    }
    public string Title {
        get { return title; }
        private set { title = value; }
    }
    public string Description {
        get { return description; }
        private set { description = value; }
    }
    public int Value {
        get { return itemValue; }
        private set { itemValue = value; }
    }
    public ItemType ItemType {
        get { return itemType; }
        private set { itemType = value; }
    }
    public string SpritePath {
        get { return spritePath; }
        private set { spritePath = value; }
    }
    public bool Stackable {
        get { return stackable; }
        private set { stackable = value; }
    }
    public Sprite Sprite {
        get { return sprite; }
        private set { sprite = value; }
    }
}

public enum ItemType { Weapon, Armor, Consumable, Junk }
