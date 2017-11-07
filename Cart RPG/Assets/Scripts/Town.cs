using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Town : MonoBehaviour {

    public GameObject wallPiece;    // model to use for outer town wall
    public int wallPieceCount;      // number of wall pieces
    public float wallRadius;        // radius of the outer town wall
    private int maxBuildingOffset;  // maximum offset for buildings so they don't all have the exact same distance between them

    public int townRadius = 25;             // The number of units away from the center of the town buildings can be placed
    public List<Vector3> buildingLocations; // list of transforms where buildings will be placed
    public GameObject[] Buildings;          // list of buildings that can be placed in the town

    // Use this for initialization
    void Start() {

        GenerateBuildings();
        GenerateWall();

    }

    //Vector3 RandomCircle(Vector3 center, float radius) {

    //    var angle = Random.value * 360; // create random angle between 0 to 360 degrees 

    //    foreach (var buildingAngle in buildingAngles) { // keep creating new angles untill one is spaced far enough away from other buildings
    //        while (angle >= buildingAngle - buildingSpacing && angle <= buildingAngle + buildingSpacing) {
    //            angle = Random.value * 360;
    //            Debug.Log("angle too close, retrying!");
    //        }
    //    }
    //    buildingAngles.Add(angle);

    //    Vector3 pos;
    //    pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
    //    pos.z = center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
    //    pos.y = center.y;
    //    Debug.Log(pos);
    //    return pos;
    //}

    Vector3[] fullCircle(Vector3 center, float radius, int points, int maxOffset) {
        List<Vector3> positions = new List<Vector3>();
        Vector3 pos;
        var stepSize = 360 / points;

        var angle = 0;

        for (int i = 0; i < points; i++) {
            angle += (stepSize + Random.Range(-maxOffset, maxOffset));
            pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            pos.z = center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            pos.y = center.y;
            positions.Add(pos);
        }

        return positions.ToArray();
    }

    void GenerateBuildings() {

        maxBuildingOffset = 360 / (Buildings.Length * 2);
        var center = transform.position;
        int buildingIndex = 0;
        var shuffledBuildings = Buildings.OrderBy(x => Random.value).ToList();
        foreach (var buildingPoint in fullCircle(center, townRadius, Buildings.Length, maxBuildingOffset)) {
            Instantiate(shuffledBuildings[buildingIndex], buildingPoint, Quaternion.FromToRotation(Vector3.forward, center - buildingPoint));
            buildingIndex++;
            Debug.DrawLine(center, buildingPoint, Color.red, 10);
        }
    }

    void GenerateWall() {
        var center = transform.position;
        foreach (var wallPoint in fullCircle(center, wallRadius, wallPieceCount, 0)) {
            Instantiate(wallPiece, wallPoint, Quaternion.FromToRotation(Vector3.forward, center - wallPoint));
        }
    }

    // Update is called once per frame
    void Update() {

    }
}