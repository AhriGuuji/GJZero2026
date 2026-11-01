using System;
using System.Linq;
using UnityEngine;

public class ItemLock : Interactable
{
    [SerializeField] private ItemData[] requiredItems;
    [SerializeField] private bool consumeItems = true;

    public event Action Solved;
    public bool IsSolved { get; private set; }

    public override void TryInteract(Interactor interactor)
    {
        if (IsSolved) return;

        var inventory = interactor.gameObject.GetComponent<PlayerInventory>();
        if (inventory == null) return;

        if (requiredItems.Count() != 0)
        {
            if (!requiredItems.All(inventory.Has))
            {
                OnFailed();
                return;
            }
        }

        if (consumeItems)
            inventory.UseItems(requiredItems);

        IsSolved = true;
        HidePrompt();
        Solved?.Invoke();
    }

    protected virtual void OnFailed() { }
}
