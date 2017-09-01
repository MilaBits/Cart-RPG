using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Town : MonoBehaviour {

    //The number of units away from the center of the town buildings can be placed
    public int buildingRadius;
    //A list of locations where buildings will be placed
    public List<Vector3> buildingLocations;
    //A list of buildings that can be placed in the town
    public GameObject[] Buildings;

    // Use this for initialization
    void Start() {

        foreach (var building in Buildings) {
            var buildingInfo = building.GetComponent<Building>();
            for (int i = 0; i < buildingInfo.maxNumberOfInstances; i++) {
                buildingLocations.Add(new Vector3(Random.Range(-buildingRadius, buildingRadius), 0,
                    Random.Range(-buildingRadius, buildingRadius)));

                Instantiate(building, buildingLocations.Last(), building.transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update() {


    }
}