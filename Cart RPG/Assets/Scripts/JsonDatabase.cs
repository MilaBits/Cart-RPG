using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using UnityEngine;
using LitJson;

public class JsonDatabase : MonoBehaviour
{

    private List<Item> itemDatabase = new List<Item>();
    private List<Storage> storageDatabase = new List<Storage>();
    private List<ModuleData> moduleDatabase = new List<ModuleData>();

    private JsonData itemData = new JsonData();
    private JsonData storageData = new JsonData();
    private JsonData moduleData = new JsonData();

    private string itemPath = "/StreamingAssets/Items.json";
    private string storagePath = "/StreamingAssets/Inventories.json";
    private string modulePath = "/StreamingAssets/CartModules.json";
    private string moduleTestPath = "/StreamingAssets/CartModulesTest.json";

    // Use this for initialization
    void Start()
    {
        ConstructItemDatabase();
        ConstructStorageDatabase();
        ConstructModuleDatabase();
    }

    #region ItemDatabase
    private void ConstructItemDatabase()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + itemPath));
        itemDatabase.Clear();
        for (int i = 0; i < itemData.Count; i++)
        {
            switch ((int)itemData[i]["type"])
            {
                case (int)ItemType.Weapon:
                    itemDatabase.Add(new Weapon((int)itemData[i]["id"], (string)itemData[i]["title"], (string)itemData[i]["description"], (int)itemData[i]["value"], (ItemType)(int)itemData[i]["type"],
                        (string)itemData[i]["slug"], (bool)itemData[i]["stackable"],
                        (WeaponType)(int)itemData[i]["weaponType"], (int)itemData[i]["damage"], (DamageType)(int)itemData[i]["damageType"]));
                    break;
                case (int)ItemType.Armor:
                    itemDatabase.Add(new Armor((int)itemData[i]["id"], (string)itemData[i]["title"], (string)itemData[i]["description"], (int)itemData[i]["value"], (ItemType)(int)itemData[i]["type"],
                        (string)itemData[i]["slug"], (bool)itemData[i]["stackable"],
                        (ArmorSlot)(int)itemData[i]["armorSlot"], (int)itemData[i]["bonusArmor"], (int)itemData[i]["bonusVitality"], (int)itemData[i]["bonusStrength"], (int)itemData[i]["bonusIntelligence"], (int)itemData[i]["bonusMovementSpeed"]));
                    break;
                case (int)ItemType.Consumable:
                    itemDatabase.Add(new Consumable((int)itemData[i]["id"], (string)itemData[i]["title"], (string)itemData[i]["description"], (int)itemData[i]["value"], (ItemType)(int)itemData[i]["type"],
                        (string)itemData[i]["slug"], (bool)itemData[i]["stackable"],
                        (int)itemData[i]["healthRecovery"], (int)itemData[i]["manaRecovery"]));
                    break;
                case (int)ItemType.Junk:
                    itemDatabase.Add(new Item((int)itemData[i]["id"], (string)itemData[i]["title"], (string)itemData[i]["description"], (int)itemData[i]["value"], (ItemType)(int)itemData[i]["type"],
                        (string)itemData[i]["slug"], (bool)itemData[i]["stackable"]));
                    break;
            }
        }
    }
    public Item GetItemById(int id)
    {
        return itemDatabase.Find(i => i.Id == id);
    }

    #endregion

    #region StorageDatabase
    private void ConstructStorageDatabase()
    {
        storageData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + storagePath));
        storageDatabase.Clear();
        for (int i = 0; i < storageData.Count; i++)
        {
            List<KeyValuePair<int, int>> items = new List<KeyValuePair<int, int>>();
            for (int j = 0; j < storageData[i]["items"].Count; j++)
            {
                items.Add(new KeyValuePair<int, int>((int)storageData[i]["items"][j]["id"], (int)storageData[i]["items"][j]["amount"]));
            }
            storageDatabase.Add(new Storage((int)storageData[i]["id"], items));
        }
    }
    public Storage GetStorage(int id)
    {
        ConstructStorageDatabase();
        for (int i = 0; i < storageDatabase.Count; i++)
        {
            if (storageDatabase[i].id == id)
                return storageDatabase[i];
        }
        return null;
    }

    private string WriteStorageJson(int storageId, bool IsLast)
    {
        Storage storage = GetStorage(storageId);
        string json = string.Empty;

        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);

        writer.WriteObjectStart();
        writer.WritePropertyName("id");
        writer.Write(storageId);
        writer.WritePropertyName("items");
        writer.WriteArrayStart();
        if (storage != null)
        {
            foreach (var item in storage.items)
            {
                writer.WriteObjectStart();
                writer.WritePropertyName("id");
                writer.Write(item.Key);
                writer.WritePropertyName("amount");
                writer.Write(item.Value);
                writer.WriteObjectEnd();
            }
        }
        else
        {
            Debug.Log("No storage with an id of " + storageId + " exists in the storage database, unable storage to update");
        }
        writer.WriteArrayEnd();
        writer.WriteObjectEnd();
        if (!IsLast)
        {
            sb.Append(',');
        }
        json = sb.ToString();
        json.Contains("]}]");
        json.Remove(json.LastIndexOf(','), 1);
        return json;
    }

    public void SaveItemsToStorage(int storageId, List<Item> items, List<int> itemAmounts)
    {
        List<KeyValuePair<int, int>> storageItems = new List<KeyValuePair<int, int>>();
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Id != -1)
            {
                storageItems.Add(new KeyValuePair<int, int>(items[i].Id, itemAmounts[i]));
            }
        }

        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);

        writer.WriteArrayStart();

        //inventories befor the one to edit
        if (storageId > 0)
        {
            for (int i = 0; i < storageId; i++)
            {
                sb.Append(WriteStorageJson(i, false));
            }
        }

        //inventory to edit
        writer.WriteObjectStart();
        writer.WritePropertyName("id");
        writer.Write(storageId);
        writer.WritePropertyName("items");
        writer.WriteArrayStart();
        foreach (var item in storageItems)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("id");
            writer.Write(item.Key);
            writer.WritePropertyName("amount");
            writer.Write(item.Value);
            writer.WriteObjectEnd();
        }
        writer.WriteArrayEnd();
        writer.WriteObjectEnd();
        if (storageId != storageDatabase.Count - 1)
            sb.Append(',');

        //inventories after the one to edit
        if (storageDatabase.Count > storageId)
        {
            for (int i = storageId + 1; i < storageDatabase.Count; i++)
            {
                if (i != storageDatabase.Count - 1)
                {
                    sb.Append(WriteStorageJson(i, false));
                }
                else
                {
                    sb.Append(WriteStorageJson(i, true));
                }
            }
        }
        writer.WriteArrayEnd();

        File.WriteAllText(Application.dataPath + storagePath, sb.ToString());
    }
    #endregion

    #region ModuleDatabase
    private void ConstructModuleDatabase()
    {
        moduleData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + modulePath));
        moduleDatabase.Clear();

        for (int i = 0; i < moduleData[0]["modules"].Count; i++)
        {
            moduleDatabase.Add(new ModuleData((int)moduleData[0]["modules"][i]["storageId"], (ModuleType)(int)moduleData[0]["modules"][i]["type"], (int)moduleData[0]["modules"][i]["position"], (int)moduleData[0]["modules"][i]["rotation"]));
        }
    }

    public List<ModuleData> GetModules()
    {
        return moduleDatabase;
    }

    public void UpdateModuleDatabase(List<GameObject> modules)
    {
        ConstructModuleDatabase();
        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);

        writer.WriteArrayStart();
        writer.WriteObjectStart();
        writer.WritePropertyName("idCount");
        writer.Write(modules.Count);
        writer.WritePropertyName("modules");
        writer.WriteArrayStart();
        foreach (GameObject module in modules)
        {
            CartModule cartModule = module.GetComponent<CartModule>();
            writer.WriteObjectStart();
            writer.WritePropertyName("position");
            writer.Write(cartModule.Position);
            writer.WritePropertyName("type");
            writer.Write((int)cartModule.Type);
            writer.WritePropertyName("rotation");
            writer.Write((int)Mathf.Round(module.transform.localRotation.eulerAngles.y));
            writer.WritePropertyName("storageId");
            if (cartModule is CartStorageModule)
            {
                writer.Write(((CartStorageModule)cartModule).StorageId);
            }
            else
            {
                writer.Write(-1);
            }
            writer.WriteObjectEnd();
        }
        writer.WriteArrayEnd();
        writer.WriteObjectEnd();
        writer.WriteArrayEnd();

        File.WriteAllText(Application.dataPath + modulePath, sb.ToString());
    }

    #endregion
}
