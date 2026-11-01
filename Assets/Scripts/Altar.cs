using UnityEngine;

public class Altar : Interactable
{
    [SerializeField] private GameObject book;

    private bool canInteract = false;

    public void SetInteractable() => canInteract = true;

    public override void TryInteract(Interactor interactor)
    {
        if (canInteract)
        {
            book.SetActive(true);
        }
    }
}
