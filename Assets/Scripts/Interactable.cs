using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private bool isAutomatic = false;
    [SerializeField] private GameObject interactionHint;

    public bool IsAutomatic => isAutomatic;

    public abstract void TryInteract(Interactor interactor);
    protected virtual void Interact() { }

    public virtual void ShowPrompt() => interactionHint.SetActive(true);

    public virtual void HidePrompt() => interactionHint.SetActive(false);
}