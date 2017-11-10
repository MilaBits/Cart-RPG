using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //camera variables
    public float lookSensitivity = 5;
    public float lookSmoothnes = 0.1f;
    public float maxUseDistance = 2f;

    private float yRotation;
    private float xRotation;
    private float currentXRotation;
    private float currentYRotation;
    private float yRotationV;
    private float xRotationV;

    //movement variables
    public float walkSpeed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float runSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Camera camera;
    [SerializeField]

    private GameObject PlayerInventoryCanvas;
    private Inventory PlayerInventory;
    private int playerInventoryId = 1;
    private GameObject ObjectInventoryCanvas;
    //private int objectInventoryId = 2; TODO: MAKE UI IT'S OWN CONTROLLER!

    private ItemDatabase itemDatabase;

    // Use this for initialization
    void Start() {
        PlayerInventoryCanvas = GameObject.Find("PlayerInventoryCanvas");
        PlayerInventory = PlayerInventoryCanvas.transform.GetChild(0).GetComponent<Inventory>();
        ObjectInventoryCanvas = GameObject.Find("ObjectInventoryCanvas");
        itemDatabase = GameObject.Find("Game").GetComponent<ItemDatabase>();
        controller = GetComponent<CharacterController>();
        camera = GetComponentInChildren<Camera>();
        PlayerInventory.LoadItemsFromDatabase(playerInventoryId);
    }

    // Update is called once per frame
    void Update() {

        //keep cursor in game window
        if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerInventoryCanvas.activeSelf == false) {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (!PlayerInventoryCanvas.activeSelf && !ObjectInventoryCanvas.activeSelf) {
            CameraMovement();
            PlayerMovement();
        }
        PlayerInteract();
    }

    void CameraMovement() {

        //Get mouse movements
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;

        //Limit vertical rotation to prevent going upside down
        xRotation = Mathf.Clamp(xRotation, -80, 100);

        //Smoothen camera movement
        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothnes);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothnes);

        //Rotate character and camera
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        camera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

    }

    void PlayerMovement() {

        //make sure player isn't jumping
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= walkSpeed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }

        //move character
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void PlayerInteract() {

        if (Input.GetButtonUp("Inventory")) {
            PlayerInventoryCanvas.SetActive(!PlayerInventoryCanvas.activeSelf);
        }

        if (Input.GetButtonUp("Use")) {

            //hide inventories if they are currently shown
            if (PlayerInventoryCanvas.activeSelf || ObjectInventoryCanvas.activeSelf) {
                PlayerInventoryCanvas.SetActive(false);

                itemDatabase.SaveItemsToStorage(playerInventoryId, PlayerInventory.items, PlayerInventory.getItemAmounts());

                ObjectInventoryCanvas.SetActive(false);
                return;
            }

            // Get the point the player is looking at
            RaycastHit hit;
            Ray ray = new Ray(camera.transform.position, camera.transform.forward);
            if (Physics.Raycast(ray, out hit)) {
                //Debug.Log("Trying to use " + hit.transform.name + " at " + hit.distance);

                //make sure it's close enough to use
                if (hit.distance < maxUseDistance && hit.transform.GetComponent<CartStorageModule>() != null) {
                    //Debug.DrawLine(camera.transform.position, hit.point, Color.green);

                    PlayerInventoryCanvas.SetActive(true);
                    ObjectInventoryCanvas.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    //PlayerInventoryCanvas.GetComponent<InventoryUI>().LoadInventory(hit.transform.gameObject.GetComponent<CartStorageModule>().Items);
                    //Debug.Log("InventoryUI enabled:" + PlayerInventoryCanvas.activeSelf);
                } else {
                    //Debug.DrawLine(camera.transform.position, hit.point, Color.red);
                    //Debug.Log("Too far!");
                }
            }

        }
    }
}
