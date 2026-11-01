using UnityEngine;
using System.Collections;

public class Altar : Interactable
{
    [SerializeField] private GameObject book;
    [SerializeField] private GameObject impObj;
    [SerializeField] private float timeBeforeImp;

    private bool canInteract = false;

    public void SetInteractable() => canInteract = true;

    public override void TryInteract(Interactor interactor)
    {
        if (canInteract)
        {
            book.SetActive(true);

            StartCoroutine(WaitToFinishGame());
        }
    }

    private IEnumerator WaitToFinishGame()
    {
        CameraShake.Instance.MakeItShake(timeBeforeImp, 3, 1);

        yield return new WaitForSeconds(timeBeforeImp);

        impObj.SetActive(true);
    }
}
