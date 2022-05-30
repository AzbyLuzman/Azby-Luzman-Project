using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerControls controls;
    public Vector2 move;
    public Vector2 cameraInput;
    public static bool interact;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Camera.canceled += ctx => cameraInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
