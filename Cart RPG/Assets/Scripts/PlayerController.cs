using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //camera variables
    public float lookSensitivity = 5;
    public float lookSmoothnes = 0.1f;

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

    // Use this for initialization
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {

        CameraMovement();
        PlayerMovement();
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
        transform.GetChild(0).rotation = Quaternion.Euler(xRotation, yRotation, 0);

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
}
