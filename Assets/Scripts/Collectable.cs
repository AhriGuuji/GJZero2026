using UnityEngine;

public class Collectable : Interactable
{
    [SerializeField] private ItemData item;
    [SerializeField] private AudioClip collectAudio;

    public override void TryInteract(Interactor interactor)
    {
        var inventory = interactor.gameObject.GetComponent<PlayerInventory>();

        if (inventory is not null)
        {
            if (!inventory.Add(item)) return;

            OnCollected(interactor);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Error trying to add collectable");
        }
    }

    protected virtual void OnCollected(Interactor who) 
    {
        AudioManager.Instance.PlaySound(collectAudio);
    }
}
