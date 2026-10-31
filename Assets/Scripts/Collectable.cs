using UnityEngine;

public class Collectable : Interactable
{
    [SerializeField] private ItemData item;

    public override void TryInteract(Interactor interactor)
    {
        if (interactor.TryGetComponent(out PlayerInventory inventory) && inventory.Add(item))
        {
            OnCollected(interactor);
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollected(Interactor who) { /* sound, VFX */ }
}
