using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using LitJson;

public class ItemDatabase : MonoBehaviour {

    private List<Item> itemDatabase = new List<Item>();
    private JsonData itemData = new JsonData();

    // Use this for initialization
    void Start() {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
        
    }

    public Item GetItemById(int id) {
        return itemDatabase.Find(i => i.Id == id);
    }

    private void ConstructItemDatabase() {
        for (int i = 0; i < itemData.Count; i++) {
            switch ((int)itemData[i]["type"]) {
                case (int)ItemType.Weapon:
                    itemDatabase.Add(new Weapon((int)itemData[i]["id"], (string)itemData[i]["title"], (string)itemData[i]["description"], (int)itemData[i]["value"], (ItemType)(int)itemData[i]["type"], 
                        (string)itemData[i]["slug"], (bool)itemData[i]["stackable"],
                        (WeaponType)(int)itemData[i]["weaponType"], (int)itemData[i]["damage"], (DamageType)(int)itemData[i]["damageType"]));
                    break;
                case ((int)ItemType.Armor):
                    itemDatabase.Add(new Armor((int)itemData[i]["id"], (string)itemData[i]["title"], (string)itemData[i]["description"], (int)itemData[i]["value"], (ItemType)(int)itemData[i]["type"], 
                        (string)itemData[i]["slug"], (bool)itemData[i]["stackable"],
                        (ArmorSlot)(int)itemData[i]["armorSlot"], (int)itemData[i]["bonusArmor"], (int)itemData[i]["bonusVitality"], (int)itemData[i]["bonusStrength"], (int)itemData[i]["bonusIntelligence"], (int)itemData[i]["bonusMovementSpeed"]));
                    break;
                case ((int)ItemType.Consumable):
                    itemDatabase.Add(new Consumable((int)itemData[i]["id"], (string)itemData[i]["title"], (string)itemData[i]["description"], (int)itemData[i]["value"], (ItemType)(int)itemData[i]["type"], 
                        (string)itemData[i]["slug"], (bool)itemData[i]["stackable"],
                        (int)itemData[i]["healthRecovery"], (int)itemData[i]["manaRecovery"]));
                    break;
                case ((int)ItemType.Junk):
                    itemDatabase.Add(new Item((int)itemData[i]["id"], (string)itemData[i]["title"], (string)itemData[i]["description"], (int)itemData[i]["value"], (ItemType)(int)itemData[i]["type"], 
                        (string)itemData[i]["slug"], (bool)itemData[i]["stackable"]));
                    break;
            }
        }
    }
}
