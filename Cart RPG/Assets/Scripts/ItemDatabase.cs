using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class ItemDatabase : MonoBehaviour {

    private List<Item> itemDatabase = new List<Item>();
    private JsonData itemData = new JsonData();

    // Use this for initialization
	void Start ()
	{
	    itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));

	}
	
}
