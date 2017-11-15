using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartModuleGrid : MonoBehaviour {
    public Vector3 gridPosition;
    public float gridLength;
    public float gridWidth;
    public int segments;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        gridPosition = transform.position + gridPosition;
    }

    void LateUpdate()
    {
        Debug.DrawLine(
            new Vector3(gridPosition.x - gridWidth / 2, gridPosition.y, gridPosition.z - gridLength / 2),
            new Vector3(gridPosition.x - gridWidth / 2, gridPosition.y + 1, gridPosition.z - gridLength / 2), Color.red, 100);
        Debug.DrawLine(
            new Vector3(gridPosition.x + gridWidth / 2, gridPosition.y, gridPosition.z + gridLength / 2),
            new Vector3(gridPosition.x + gridWidth / 2, gridPosition.y + 1, gridPosition.z + gridLength / 2), Color.red, 100);
        Debug.DrawLine(
            new Vector3(gridPosition.x + gridWidth / 2, gridPosition.y, gridPosition.z - gridLength / 2),
            new Vector3(gridPosition.x + gridWidth / 2, gridPosition.y + 1, gridPosition.z - gridLength / 2), Color.red, 100);
        Debug.DrawLine(
            new Vector3(gridPosition.x - gridWidth / 2, gridPosition.y, gridPosition.z + gridLength / 2),
            new Vector3(gridPosition.x - gridWidth / 2, gridPosition.y + 1, gridPosition.z + gridLength / 2), Color.red, 100);

        Debug.DrawLine(
            new Vector3(gridPosition.x - gridWidth / 2, gridPosition.y, gridPosition.z - gridLength / 2),
            new Vector3(gridPosition.x + gridWidth / 2, gridPosition.y, gridPosition.z - gridLength / 2), Color.green, 100);
        Debug.DrawLine(
            new Vector3(gridPosition.x - gridWidth / 2, gridPosition.y, gridPosition.z - gridLength / 2),
            new Vector3(gridPosition.x - gridWidth / 2, gridPosition.y, gridPosition.z + gridLength / 2), Color.green, 100);
        Debug.DrawLine(
            new Vector3(gridPosition.x - gridWidth / 2, gridPosition.y, gridPosition.z + gridLength / 2),
            new Vector3(gridPosition.x + gridWidth / 2, gridPosition.y, gridPosition.z + gridLength / 2), Color.green, 100);
        Debug.DrawLine(
            new Vector3(gridPosition.x + gridWidth / 2, gridPosition.y, gridPosition.z + gridLength / 2),
            new Vector3(gridPosition.x + gridWidth / 2, gridPosition.y, gridPosition.z - gridLength / 2), Color.green, 100);
    }
}
