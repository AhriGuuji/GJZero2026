using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private bool isAutomatic = false;
    [SerializeField] private GameObject interactionHint;
    [SerializeField] private Outline outline;

    public bool IsAutomatic => isAutomatic;

    public abstract void TryInteract(Interactor interactor);
    protected virtual void Interact() { }

    public virtual void ShowPrompt()
    {
        interactionHint.SetActive(true);
        outline.enabled = true;
    }

    public virtual void HidePrompt()
    {
        interactionHint.SetActive(false);
        outline.enabled = false;
    }
}