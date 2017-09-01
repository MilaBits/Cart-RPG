using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour {

    public int buildingRadius;
    public Vector3[] BuildingCoords;
    public GameObject[] Buildings;
    public List<int> buildingLimit;


    // Use this for initialization
    void Start() {

        int buildingcount = 0;
        foreach (var limit in buildingLimit)
        {
            buildingcount += limit;
        }
        BuildingCoords =int[buildingcount];

        int buildingIndex = 0;
        foreach (var limit in buildingLimit) {
            for (int i = 0; i < limit; i++) {
                BuildingCoords[buildingIndex].x = Random.Range(-buildingRadius, buildingRadius);
                BuildingCoords[buildingIndex].z = Random.Range(-buildingRadius, buildingRadius);

                Instantiate(Buildings[buildingIndex], BuildingCoords[buildingIndex], Buildings[buildingIndex].transform.rotation);
            }

            buildingIndex++;
        }

        //foreach (var building in Buildings) {
        //    if (building != null) {
        //        BuildingCoords[buildingIndex].x = Random.Range(-buildingRadius, buildingRadius);
        //        BuildingCoords[buildingIndex].z = Random.Range(-buildingRadius, buildingRadius);

        //        Instantiate(building, BuildingCoords[buildingIndex], building.transform.rotation);

        //    }
        //    buildingIndex++;
        //}
    }

    // Update is called once per frame
    void Update() {

    }
}
