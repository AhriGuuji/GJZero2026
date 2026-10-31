using UnityEngine;

public class Interactor : MonoBehaviour
{
    private Interactable currentInteractable;

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.gameObject.GetComponentInParent<Interactable>();

        if (interactable != null)
        {
            Debug.Log("Entrei num collider trigger Interactable!");
            if (interactable.IsAutomatic)
            {
                interactable.TryInteract(this);
            }
            else
            {
                currentInteractable = interactable;
                currentInteractable.ShowPrompt();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponentInParent<Interactable>();
        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable.HidePrompt();
            currentInteractable = null;
        }
    }

    public void ExecuteInteraction()
    {
        if (currentInteractable != null)
        {
            currentInteractable.TryInteract(this);
        }
    }
}