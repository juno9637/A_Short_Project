using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController: IDisposable {
    readonly InputSystem_Actions controls;
    readonly Transform mainCamera;
    
    public Vector3 moveDirection {get; private set;}
    public bool jumpPressed {get; private set;}
    public bool jumpReleased {get; private set;}
    public bool jumpHeld {get; private set;}

    public InputController(Transform mainCamera)
    {
        this.mainCamera = mainCamera;
        controls = new InputSystem_Actions();
        controls.Player.Enable();
    }

    public void GetInput()
    {
        Vector2 rawMove = controls.Player.Move.ReadValue<Vector2>();
        
        //Get where the camera is looking towards, and flatten the vector into the horizontal plane (vector3.up being the normal)
        Vector3 forward = Vector3.ProjectOnPlane(mainCamera.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(mainCamera.right, Vector3.up).normalized;
        moveDirection = rawMove.y * forward + rawMove.x * right;
        
        jumpPressed = controls.Player.Jump.WasPressedThisFrame();
        jumpHeld = controls.Player.Jump.IsPressed();
    }
    
    public void Dispose() { controls.Player.Disable(); controls.Dispose(); }
}
