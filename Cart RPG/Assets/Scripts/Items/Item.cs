using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine.UI;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField] private string _Name;
    [SerializeField] private string _Description;
    [SerializeField] private Image _Image;
    [SerializeField] private int _Value;
    [SerializeField] private ItemType _ItemType;

    public Item(string name, string description, int value, ItemType itemType) {

        this._Name = name;
        this._Description = description;
        this._Value = value;
        this._ItemType = itemType;
    }

    public string Name {
        get { return _Name; }
    }
    public string Description {
        get { return _Description; }
    }
    public int Value {
        get { return _Value; }
    }
    public ItemType ItemType {
        get { return _ItemType; }
    }
    public Image Image {
        get { return _Image; }
    }
}

public enum ItemType { MeleeWeapon, RangedWeapon, MagicalWeapon, Misc, Consumable }
