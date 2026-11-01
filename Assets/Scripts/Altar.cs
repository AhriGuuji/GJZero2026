using UnityEngine;
using System.Collections;

public class Altar : Interactable
{
    [SerializeField] private GameObject book;
    [SerializeField] private AudioClip bookAudio;
    [SerializeField] private GameObject impObj;
    [SerializeField] private AudioClip MagicCircleAudio;
    [SerializeField] private float timeBeforeImp;

    private bool canInteract = false;

    public void SetInteractable() => canInteract = true;

    public override void TryInteract(Interactor interactor)
    {
        if (canInteract)
        {
            book.SetActive(true);
            AudioManager.Instance.PlaySound(bookAudio);

            StartCoroutine(WaitToFinishGame());
        }
    }

    private IEnumerator WaitToFinishGame()
    {
        yield return new WaitForSeconds(bookAudio.length);

        AudioManager.Instance.PlaySound(MagicCircleAudio);
        CameraShake.Instance.MakeItShake(timeBeforeImp, 1, 1);

        yield return new WaitForSeconds(timeBeforeImp);

        impObj.SetActive(true);
    }
}
