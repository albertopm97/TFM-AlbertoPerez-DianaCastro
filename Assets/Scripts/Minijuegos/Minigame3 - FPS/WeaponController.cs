using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public enum controllerType { GamePad, Mouse };
    
    //Referencias
    PlayerInput playerInput;

    Vector2 mousePositionInput;

    Vector3 direction;

    public controllerType controller;
    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    

    private void Update()
    {
        if (Gamepad.all.Count > 0)
        {
            controller = controllerType.GamePad;
        }
        else
        {
            controller = controllerType.Mouse;
        }

        if (controller == controllerType.Mouse)
        {
            mouseInputManager();
        }
        else
        {
            joystickInputManager();
        }
    }

    private void FixedUpdate()
    {
        rotateGun();
    }

    void mouseInputManager()
    {
        mousePositionInput = Mouse.current.position.ReadValue();

        Camera mainCamera = Camera.main;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionInput.x, mousePositionInput.y, mainCamera.nearClipPlane));

        mouseWorldPosition.z = transform.position.z;

        direction = mouseWorldPosition - transform.position;
    }

    void joystickInputManager()
    {
        direction = playerInput.actions["RotateGunJoystick"].ReadValue<Vector2>();
    }

    void rotateGun()
    {
        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rotationAngle = Mathf.Clamp(rotationAngle, -60f, 60f);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAngle));

    }
}
