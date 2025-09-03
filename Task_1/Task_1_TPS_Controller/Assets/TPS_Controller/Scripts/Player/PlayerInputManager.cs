using System.Linq;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public Vector2 movementInput;
    public Vector2 lookInput;

    private void OnEnable()
    {
        if (playerInputActions == null) { 
            playerInputActions = new PlayerInputActions();

            playerInputActions.PlayerControls.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerInputActions.PlayerControls.Look.performed += i => lookInput = i.ReadValue<Vector2>();
        }
        playerInputActions.Enable();
        playerInputActions.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    public bool GetIntractButtonPressed()
    {
        return playerInputActions.PlayerControls.Interact.triggered;
    }

    public bool GetInventoryButtonPressed()
    {
        return playerInputActions.PlayerControls.Inventory.triggered;
    }



}
