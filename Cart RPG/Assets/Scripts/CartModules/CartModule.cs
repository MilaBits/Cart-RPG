using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartModule : MonoBehaviour {

    [SerializeField] private string _Name;
    [SerializeField] private string _Size;

    public string Name {
        get { return _Name; }
        private set { _Name = value; }
    }
    public string Size {
        get { return _Size; }
        private set { _Size = value; }
    }
    
    // Use this for initialization
    void Start() {

    }

    public override string ToString() {
        return Name + " " + Size;
    }
}
