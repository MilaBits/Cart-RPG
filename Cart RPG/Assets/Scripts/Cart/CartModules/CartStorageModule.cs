using System.Collections.Generic;
using UnityEngine;

public class CartStorageModule : CartModule
{

    [SerializeField] private int _Capacity;
    [SerializeField] private int _StorageId;

    public int Capacity
    {
        get { return _Capacity; }
        private set { _Capacity = value; }
    }

    public int StorageId
    {
        get { return _StorageId; }
        set { _StorageId = value; }
    }

    public CartStorageModule(string name, string size, int id, ModuleType type, int capacity) : base(name, size, id, type)
    {
        _Capacity = capacity;
    }
}
