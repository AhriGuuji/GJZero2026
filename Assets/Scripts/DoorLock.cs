using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private ItemLock lockSource;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip unlockSound;

    private void OnEnable() => lockSource.Solved += OpenDoor;
    private void OnDisable() => lockSource.Solved -= OpenDoor;

    private void OpenDoor() 
    {
        if (unlockSound != null)
        {
            AudioManager.Instance.PlaySound(unlockSound);
        }
        animator.SetTrigger("Open");
    }
}
