using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartModule : MonoBehaviour
{

    [SerializeField] private string _Name;
    [SerializeField] private string _Size;
    [SerializeField] private int _Id;

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

    // Use this for initialization
    void Start()
    {

    }

    public override string ToString()
    {
        return Name + " " + Size;
    }
}
