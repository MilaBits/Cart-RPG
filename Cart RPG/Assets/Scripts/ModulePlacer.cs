using UnityEngine;
using UnityEngine.UI;

public class ModulePlacer : MonoBehaviour {
    [SerializeField]
    private Canvas buildUi;

    [SerializeField]
    private Camera camera;

    private Text text;

    public float maxBuildRange;
    [SerializeField]
    private int ModuleIndex = 0;
    public GameObject[] Modules = new GameObject[0];
    private GameObject ghostModule;

    private Material NormalMaterial;
    [SerializeField]
    private Material ghostMaterial;

    [SerializeField]
    private GameObject grid;
    private GridScript gridScript;

    [SerializeField]
    private int moduleRotation = 0;

    // Use this for initialization
    void Start() {
        text = buildUi.GetComponentsInChildren<Text>()[1];
        gridScript = grid.GetComponent<GridScript>();
    }

    // Update is called once per frame
    void Update() {
        updateCurrentItem();

        // Get the point the player is looking at
        RaycastHit hit;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        if (Physics.Raycast(ray, out hit)) {
            //Debug.Log(hit.transform.name);
            Debug.DrawLine(camera.transform.position, hit.point, Color.red);
        }

        // Get the closest tile to the point the player is looking at
        Transform closestTile = getClosestTile(hit.point, gridScript.gridTiles.ToArray());

        if (ghostModule != null) {
            // Move preview of object that is to be placed to the closest grid point to where the player is aiming
            ghostModule.transform.parent = closestTile.parent;
            ghostModule.transform.position = closestTile.position;
            ghostModule.transform.rotation = closestTile.rotation;
            ghostModule.transform.Rotate(new Vector3(-90, 0, moduleRotation * 90));

            if (Input.GetButtonUp("Fire1")) {
                Instantiate(Modules[ModuleIndex], ghostModule.transform.position, ghostModule.transform.rotation, ghostModule.transform.parent);
            }
        }
    }

    void updateCurrentItem() {

        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) {
                if (ModuleIndex < Modules.Length - 1) {
                    ModuleIndex++;
                } else {
                    ModuleIndex = 0;
                }
            } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
                if (ModuleIndex > 0) {
                    ModuleIndex--;
                } else {
                    ModuleIndex = Modules.Length - 1;
                }
            }

            // Update UI to show what item is picked
            text.text = Modules[ModuleIndex].GetComponent<CartModule>().ToString();

            // Replace and set up ghost image of selected item
            Destroy(ghostModule);
            ghostModule = Instantiate(Modules[ModuleIndex]);
            NormalMaterial = ghostModule.GetComponent<Renderer>().materials[0];
            ghostModule.GetComponent<Collider>().enabled = false;
            ghostModule.GetComponent<Renderer>().material = ghostMaterial;
        }

        if (Input.GetButtonUp("Rotate Module")) {
            if (moduleRotation < 3) {
                moduleRotation++;
            } else {
                moduleRotation = 0;
            }
        }
    }

    Transform getClosestTile(Vector3 point, GameObject[] tiles) {
        Transform closest = null;
        float minDist = Mathf.Infinity;
        foreach (GameObject tile in tiles) {
            if (tile.GetComponent<GridTile>().IsFree) {
                float dist = Vector3.Distance(tile.transform.position, point);

                //replace closest if new distance is closer
                if (dist < minDist) {
                    closest = tile.transform;
                    minDist = dist;
                }
            }
        }
        return closest;
    }
}
