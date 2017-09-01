using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    public int maxNumberOfInstances;

    private Town town;

    // Use this for initialization
    void Start() {

        //debug: For debugging, only work for the well model
        //if (gameObject.name.Contains("Building")) {
        //    return;
        //}

        //Get a list of building positions
        town = GameObject.Find("Town").GetComponent<Town>();
        Vector3[] closestBuildings = GetClosestBuildings(town.buildingLocations);

        //debug: show coordinates of closest buildings
        //Debug.Log("1:" + closestBuildings[0] + " 2:" + closestBuildings[1]);
        //Debug.Log(gameObject.name + ": " + closestBuildings.Length);

        //Get the position between the two closest buildings
        Vector3 lookatPos = Vector3.zero;// = closestBuildings[0] + closestBuildings[1];
        try {
            lookatPos = Vector3.Lerp(closestBuildings[0], closestBuildings[1], 0.5f);

        } catch (Exception e) {
            Console.WriteLine(e);
            return;
        }

        //debug: put a line on closest buildings
        //Debug.DrawLine(closestBuildings[0], new Vector3(closestBuildings[0].x, closestBuildings[0].y + 15, closestBuildings[0].z), Color.green, 10);
        //Debug.DrawLine(closestBuildings[1], new Vector3(closestBuildings[1].x, closestBuildings[1].y + 15, closestBuildings[1].z), Color.blue, 10);

        //debug draw a line to the position in the middle of the two closest buildings
        //Debug.DrawLine(transform.position, lookatPos, Color.magenta, 10);
        //Debug.DrawLine(closestBuildings[0], closestBuildings[1], Color.red, 10);
        //Debug.DrawLine(lookatPos, new Vector3(lookatPos.x, lookatPos.y + 15, lookatPos.z), Color.red, 10);

        //Look at the position in the middle of the two closest buildings
        transform.LookAt(lookatPos);



    }

    Vector3[] GetClosestBuildings(List<Vector3> buildings) {
        List<Vector3> closestBuildings = new List<Vector3>();
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Vector3 buildingPosition in buildings) {
            if (buildingPosition == transform.position) {
                continue;
            }
            Vector3 directionToTarget = buildingPosition - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr) {
                closestDistanceSqr = dSqrToTarget;
                closestBuildings.Add(buildingPosition);
            }
        }
        closestBuildings.Reverse();
        return closestBuildings.ToArray();
    }

    // Update is called once per frame
    void Update() {

    }
}
