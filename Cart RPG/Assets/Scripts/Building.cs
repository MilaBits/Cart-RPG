using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    public int maxNumberOfInstances;

    private Town town;
    
    void Start() {
        if (GameObject.Find("Town") == null) {
            return;
        }

        //Get a list of building positions
        town = GameObject.Find("Town").GetComponent<Town>();
        Vector3[] closestBuildings = GetClosestBuildings(town.buildingLocations);

        Vector3 lookatPos = Vector3.zero;
        try {
            lookatPos = Vector3.Lerp(closestBuildings[0], closestBuildings[1], 0.5f);

        } catch (Exception e) {
            Console.WriteLine(e);
            return;
        }
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
}
