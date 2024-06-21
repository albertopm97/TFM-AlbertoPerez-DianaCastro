using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class VehicleController : MonoBehaviour
{
    //Referencias
    PlayerInput playerInput;
    [SerializeField] private Rigidbody2D frontTireRb;
    [SerializeField] private Rigidbody2D backTireRb;
    [SerializeField] private Rigidbody2D carRb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    [Header("Sonido")]
    [SerializeField] private StudioEventEmitter motorLoop;

    float horizontalInput;

    bool loopPlaying;

    void Start()
    { 
        playerInput = GetComponent<PlayerInput>();

        moveSpeed = 250f;

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

        if(horizontalInput != 0 && !loopPlaying) 
        {
            motorLoop.Play();

            loopPlaying = true;

            print("playing loop");
        }

        if(horizontalInput == 0)
        {
            motorLoop.Stop();

            loopPlaying = false;
        }
    }

    void move()
    {
        frontTireRb.AddTorque(-horizontalInput * moveSpeed * Time.fixedDeltaTime);
        backTireRb.AddTorque(-horizontalInput * moveSpeed * Time.fixedDeltaTime);
        carRb.AddTorque(-horizontalInput * rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            RaceGameManager.Instance.finish();
        }
    }
}
