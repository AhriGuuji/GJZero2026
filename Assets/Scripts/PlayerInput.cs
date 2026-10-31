using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private UIManager uiHandler;

    //private Character character;
    private Movement playerMovement;
    private Interactor interactor;
    
    
    public Vector2 MoveInput { get; private set; }
    public bool SprintHeld { get; private set; }


    public Action OnWeaponChange;

    private void Start()
    {
        //uiHandler = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        //character = GetComponent<Character>();
        playerMovement = GetComponent<Movement>();
        interactor = GetComponent<Interactor>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        // if (!VerifyIfPlayerCanAct()) return;

        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Entrei porra");
        if (context.phase == InputActionPhase.Performed)
        {
            interactor.ExecuteInteraction();
        }
    }

    public void OnCollect(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // Logic to collect and send to inventory
            // interactor.Collect(); ??
        }
    }

    //private bool VerifyIfPlayerCanAct() => GameManager.Instance.CanPlayerAct;
}