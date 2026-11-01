using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private ItemLock lockSource;
    [SerializeField] private Animator animator;

    private void OnEnable() => lockSource.Solved += OpenDoor;
    private void OnDisable() => lockSource.Solved -= OpenDoor;

    private void OpenDoor() 
    {
        animator.SetTrigger("Open");
    }
}
