using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartModule : MonoBehaviour
{

    [SerializeField] private string _Name;
    [SerializeField] private string _Size;
    [SerializeField] private int _Id;
    [SerializeField] private int _Position;
    [SerializeField] private ModuleType _type;


    public string Name
    {
        get { return _Name; }
        private set { _Name = value; }
    }
    public string Size
    {
        get { return _Size; }
        private set { _Size = value; }
    }
    public int Id
    {
        get { return _Id; }
        private set { _Id = value; }
    }
    public int Position
    {
        get { return _Id; }
        set { _Id = value; }
    }

    public ModuleType Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public override string ToString()
    {
        return Name + " " + Size;
    }

    public CartModule(string name, string size, int id, ModuleType type)
    {
        _Name = name;
        _Size = size;
        _Id = id;
        _type = type;
    }

    public ModuleType FindModuleType()
    {
        string name = gameObject.name.ToLower();
        if (name.Contains("crate1x1"))
            return ModuleType.Crate;
        if (name.Contains("crate1x2"))
            return ModuleType.CrateWide;
        if (name.Contains("barrel1x1"))
            return ModuleType.Barrel;
        if (name.Contains("cage2x2"))
            return ModuleType.Cage;
        if (name.Contains("cage3x3"))
            return ModuleType.CageBig;
        return ModuleType.Unassigned;
    }
}
