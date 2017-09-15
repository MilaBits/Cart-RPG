using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine.UI;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField]
    private int _Id;
    [SerializeField]
    private string _Title;
    [SerializeField]
    private string _Description;
    [SerializeField]
    private Sprite _Sprite;
    [SerializeField]
    private int _Value;
    [SerializeField]
    private ItemType _ItemType;

    public Item(int id, string title, string description, int value, ItemType itemType) {

        this._Id = id;
        this._Title = title;
        this._Description = description;
        this._Value = value;
        this._ItemType = itemType;
    }

    public int Id {
        get { return _Id; }
        private set { _Id = value; }
    }
    public string Title {
        get { return _Title; }
        private set { _Title = value; }
    }
    public string Description {
        get { return _Description; }
        private set { _Description = value; }
    }
    public int Value {
        get { return _Value; }
        private set { _Value = value; }
    }
    public ItemType ItemType {
        get { return _ItemType; }
        private set { _ItemType = value; }
    }
    public Sprite Sprite {
        get { return _Sprite; }
        private set { _Sprite = value; }
    }
}

public enum ItemType { MeleeWeapon, RangedWeapon, MagicalWeapon, Misc, Consumable }
