using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Town : MonoBehaviour {

    //The number of units away from the center of the town buildings can be placed
    public int townRadius = 25;

    //A list of transforms where buildings will be placed
    public List<Vector3> buildingLocations;

    //A list of buildings that can be placed in the town
    public GameObject[] Buildings;
    
    //A list of building positions by angle from the centerpoint
    private List<float> buildingAngles = new List<float>();

    //Minimum amount of degrees between buildings
    public float buildingSpacing = 10;

    // Use this for initialization
    void Start() {

        var center = transform.position;
        for (int i = 0; i < Buildings.Length; i++) {
            var pos = RandomCircle(center, townRadius);
            var rot = Quaternion.FromToRotation(Vector3.back, center - pos); // rotate building towards center
            GameObject building = Instantiate(Buildings[i], pos, rot);
            //building.transform.LookAt(pos - center);
            //buildingLocations.Add(building.transform.position);
            
            Debug.DrawLine(center, pos, Color.green, 10);
        }

        #region stupid old code 
        //foreach (var building in Buildings) {
        //    var buildingInfo = building.GetComponent<Building>();
        //    for (int i = 0; i < buildingInfo.maxNumberOfInstances; i++) {
        //        buildingLocations.Add(new Vector3(Random.Range(-buildingRadius, buildingRadius), 0,
        //            Random.Range(-buildingRadius, buildingRadius)));

        //        GameObject newBuilding = Instantiate(building, buildingLocations.Last(), building.transform.rotation);

        //    }
        //}
        #endregion
    }

    Vector3 RandomCircle(Vector3 center, float radius) {

        var angle = Random.value * 360; // create random angle between 0 to 360 degrees 

        foreach (var buildingAngle in buildingAngles) { // keep creating new angles untill one is spaced far enough away from other buildings
            while (angle >= buildingAngle - buildingSpacing && angle <= buildingAngle + buildingSpacing) {
                angle = Random.value * 360;
                Debug.Log("angle too close, retrying!");
            }
        }
        buildingAngles.Add(angle);

        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.y = center.y;
        Debug.Log(pos);
        return pos;
    }

    // Update is called once per frame
    void Update() {

    }
}