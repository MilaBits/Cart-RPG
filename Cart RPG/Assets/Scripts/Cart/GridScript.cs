using System;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
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

    private void LoadCartModules()
    {
        List<ModuleData> modulesToLoad = jsonDatabase.GetModules();
        GameObject module;

        foreach (ModuleData moduleData in modulesToLoad)
        {
            switch (moduleData.Type)
            {
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
                    throw new ArgumentOutOfRangeException();
            }
            GameObject placedModule = Instantiate(module, transform);

            placedModule.transform.position = gridTiles[moduleData.Position].transform.position;

            modules.Add(placedModule);

            if (module.GetComponent<CartStorageModule>() != null)
            {
                CartStorageModule storageModule = module.GetComponent<CartStorageModule>();
                storageModule.StorageId = moduleData.StorageId;
            }
        }
    }
}
