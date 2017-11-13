using System.Collections.Generic;
using UnityEngine;

public class CartStorageModule : CartModule {

    [SerializeField] private int _Capacity;
    [SerializeField] private List<Item> _Items;

    public int Capacity
    {
        get { return _Capacity; }
        private set { _Capacity = value; }
    }
    public List<Item> Items {
        get { return _Items; }
        private set { _Items = value; }
    }

    // Use this for initialization
    void Start() {
        
    }

    public bool AddItem(Item i) {
        if (Items.Count < _Capacity) {
            Items.Add(i);
            return true;
        }
        return false;
    }

    public bool RemoveItem(int itemIndex) {
        if (_Items[itemIndex] != null) {
            _Items.RemoveAt(itemIndex);
            return true;
        }
        return false;
    }

    public void ShowInventory() {

    }
}
