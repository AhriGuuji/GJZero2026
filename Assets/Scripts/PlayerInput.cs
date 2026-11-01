using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Movement playerMovement;
    private Interactor interactor;
    private PlayerInventory playerInventory;


    public Vector2 MoveInput { get; private set; }
    public bool SprintHeld { get; private set; }


    public Action OnWeaponChange;

    private void Start()
    {
        playerMovement = GetComponent<Movement>();
        interactor = GetComponent<Interactor>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnInventorySlotChange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            var keyboard = Keyboard.current;
            if (keyboard == null) return;

            for (int i = 1; i <= 9; i++)
            {
                Key key = (Key)((int)Key.Digit1 + i - 1);

                if (keyboard[key].wasPressedThisFrame)
                {
                    playerInventory.UpdateSelected(i - 1);
                    return;
                }
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            interactor.ExecuteInteraction();
        }
    }

    public void OnTryFocus(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            playerInventory.TryFocus();
        }
    }
}