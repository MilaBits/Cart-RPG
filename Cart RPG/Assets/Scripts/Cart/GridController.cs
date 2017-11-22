using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridController : MonoBehaviour
{

    public int NumberOfColumns = 10;
    public int NumberOfRows = 10;
    public float SeperationValueX = 0.0f; // Distance between each column
    public float SeperationValueZ = 0.0f; // Distance between each Row

    public GameObject GridItem;

    private Vector3 position; //position of the grid

    private float tempSepX = 0; // used to calculate the separation between each column
    private float tempSepZ = 0; // used to calculate the separation between each row

    public List<GameObject> gridTiles = new List<GameObject>(); //list of generated tiles

    private JsonDatabase jsonDatabase;
    public List<GameObject> modules = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        jsonDatabase = GameObject.Find("Game").GetComponent<JsonDatabase>();

        position = transform.position;
        createGrid();

        LoadCartModules();
    }

    void createGrid()
    {
        for (int i = 0; i < NumberOfColumns; i++)
        {  //Columns
            for (int j = 0; j < NumberOfRows; j++)
            { //Rows
                GameObject gridPiece = Instantiate(GridItem, new Vector3(i + tempSepX, 0, j + tempSepZ), Quaternion.identity);
                gridPiece.transform.SetParent(transform, false);
                tempSepZ += SeperationValueZ; //change the value of seperation between rows
                gridTiles.Add(gridPiece);
            }
            tempSepX += SeperationValueX; //change the value of seperation between columns
            tempSepZ = 0; //Reset the value of the seperation between the rows so it won‘t cumulate
        }
    }

    public void RemoveModule(GameObject module)
    {
        modules.RemoveAt(modules.FindIndex(o => o.transform == module.transform));
        jsonDatabase.UpdateModuleDatabase(modules);
        GameObject.Destroy(module);
    }

    public void AddModule(GameObject placedModule, int position)
    {
        //TODO: check if nothing intersects with new module
        //Bounds bounds = placedModule.GetComponent<Collider>().bounds;
        //foreach (GameObject module in modules)
        //{
        //    if (bounds.Intersects(module.GetComponent<Collider>().bounds))
        //    {
        //        Destroy(placedModule);
        //        Debug.Log("placedModule intersected with already existing module");
        //        return;
        //    }
        //}

        CartModule cartModule = placedModule.GetComponent<CartModule>();
        cartModule.Position = position;
        cartModule.Type = cartModule.FindModuleType();
        Debug.Log(cartModule.Type.ToString());
        if (placedModule.GetComponent<CartStorageModule>() != null)
        {
            placedModule.GetComponent<CartStorageModule>().StorageId = jsonDatabase.StorageDatabase.Count;
        }

        modules.Add(placedModule);
        jsonDatabase.UpdateModuleDatabase(modules);
    }

    public bool AddModule(ModuleData moduleData)
    {

        GameObject module = (GameObject)Resources.Load("Prefabs/Modules/cartModuleUnassigned");
        switch (moduleData.Type)
        {
            case ModuleType.Unassigned:
                Debug.Log("module in modules json file has an unassigned module type \"0\"");
                break;
            case ModuleType.Crate:
                module = (GameObject)Resources.Load("Prefabs/Modules/cartModuleCrate1x1");
                break;
            case ModuleType.CrateWide:
                module = (GameObject)Resources.Load("Prefabs/Modules/cartModuleCrate1x2");
                break;
            case ModuleType.Barrel:
                module = (GameObject)Resources.Load("Prefabs/Modules/cartModuleBarrel1x1");
                break;
            case ModuleType.Cage:
                module = (GameObject)Resources.Load("Prefabs/Modules/cartModuleCage2x2");
                break;
            case ModuleType.CageBig:
                module = (GameObject)Resources.Load("Prefabs/Modules/cartModuleCage3x3");
                break;
            default:
                Debug.Log("Invalid module type, skipping" + module.name);
                return false;
        }
        GameObject placedModule = Instantiate(module, transform);
        CartModule cartModule = placedModule.GetComponent<CartModule>();
        cartModule.Position = moduleData.Position;
        cartModule.Type = moduleData.Type;
        placedModule.transform.position = gridTiles[moduleData.Position].transform.position;
        placedModule.transform.Rotate(Vector3.forward, moduleData.Rotation);

        modules.Add(placedModule);

        if (module.GetComponent<CartStorageModule>() != null)
        {
            CartStorageModule storageModule = module.GetComponent<CartStorageModule>();
            storageModule.StorageId = moduleData.StorageId;
        }
        if (cartModule.Type == ModuleType.Unassigned)
        {
            return false;
        }
        return true;
    }

    private void LoadCartModules()
    {
        List<ModuleData> modulesToLoad = jsonDatabase.GetModules();

        for (var i = 0; i < modulesToLoad.Count; i++)
        {
            if (!AddModule(modulesToLoad[i]))
            {
                Debug.Log("Module " + i + " failed to load");
            }
        }
    }
}
