using UnityEngine;

public class GridTile : MonoBehaviour {

    
    public bool IsFree { get; private set; }

    // Update is called once per frame
    void Update() {

        //Check if a module is above the tile
        Ray ray = new Ray(transform.position, Vector3.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Cart Module") {
            IsFree = false;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        } else {
            IsFree = true;
            Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Color.green);
        }
    }
}
