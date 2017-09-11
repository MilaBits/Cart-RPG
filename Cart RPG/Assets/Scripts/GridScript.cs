using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

    public int NumberOfColumns = 10;
    public int NumberOfRows = 10;
    public float SeperationValueX = 0.0f; // Distance between each column
    public float SeperationValueZ = 0.0f; // Distance between each Row

    public GameObject TheGridItem; // this is the item u will use

    private Vector3 position; //position of the grid

    private float tempSepX = 0; // used to calculate the separation between each column
    private float tempSepZ = 0; // used to calculate the separation between each row

    public List<GameObject> gridTiles = new List<GameObject>(); //list of generated tiles

    void createGrid() {

        for (int i = 0; i < NumberOfColumns; i++) {  //Columns
            for (int j = 0; j < NumberOfRows; j++) { //Rows
                GameObject gridPiece = Instantiate(TheGridItem, new Vector3(i + tempSepX, 0, j + tempSepZ), Quaternion.identity);
                gridPiece.transform.SetParent(transform, false);
                tempSepZ += SeperationValueZ; //change the value of seperation between rows
                gridTiles.Add(gridPiece);
            }
            tempSepX += SeperationValueX; //change the value of seperation between columns
            tempSepZ = 0; //Reset the value of the seperation between the rows so it won‘t cumulate
        }
    }

    // Use this for initialization
    void Start() {
        position = transform.position;
        createGrid();
    }
}
