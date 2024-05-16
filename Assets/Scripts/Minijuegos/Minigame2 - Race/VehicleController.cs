using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    //Referencias
    PlayerInput playerInput;
    [SerializeField] private Rigidbody2D frontTireRb;
    [SerializeField] private Rigidbody2D backTireRb;
    [SerializeField] private Rigidbody2D carRb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    float horizontalInput;

    void Start()
    { 
        playerInput = GetComponent<PlayerInput>();

        moveSpeed = 400f;

        rotationSpeed = 600f;
    }

    private void Update()
    {
        inputManager();
    }

    private void FixedUpdate()
    {
        move();
    }

    void inputManager()
    {
        Vector2 movimientoInput = playerInput.actions["Move"].ReadValue<Vector2>();

        //Only need horizontal move inout
        horizontalInput = movimientoInput.x;
    }

    void move()
    {
        frontTireRb.AddTorque(-horizontalInput * moveSpeed * Time.fixedDeltaTime);
        backTireRb.AddTorque(-horizontalInput * moveSpeed * Time.fixedDeltaTime);
        carRb.AddTorque(-horizontalInput * rotationSpeed * Time.fixedDeltaTime);
    }
}
