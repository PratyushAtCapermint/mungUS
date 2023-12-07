using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] float MovementSpeed = 1;
    float initialMovementSpeeed;

    [Header("Mouse Variables")]
    [SerializeField] float horizontalSpeed = 1f;
    [SerializeField] float verticalSpeed = 1f;
    [SerializeField] float xRotation = 0.0f;
    [SerializeField] float yRotation = 0.0f;
    [SerializeField] Camera cam;
    [SerializeField] Vector3 offset;
    [Header("Animator")]
    [SerializeField] Animator animator;

    CharacterController characterController;
    Vector3 camStartPosition;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        initialMovementSpeeed = MovementSpeed;
        camStartPosition = cam.transform.localPosition;
    }
    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    float horizontal;
    float vertical;
    Vector3 moveDir;
    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            MovementSpeed = initialMovementSpeeed + 5;
        else
            MovementSpeed = initialMovementSpeeed;

         horizontal = Input.GetAxis("Horizontal") * MovementSpeed * Time.deltaTime;
         vertical = Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime;

        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("isMoving", true);
          
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        //transform.Translate(new(horizontal, 0, vertical));
        moveDir = transform.forward * vertical + transform.right * horizontal;
        characterController.Move(moveDir);

        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -10, 10);

        cam.transform.eulerAngles = new Vector3(xRotation,yRotation, 0.0f);
        transform.eulerAngles = new Vector3(0, yRotation, 0);
    }

   

    void HandleInteractions()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Debug.Log("player interacted");

        if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
            Debug.Log("player killed someone");
    }
}
