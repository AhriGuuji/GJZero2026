using UnityEngine;

public class ClockLock : MonoBehaviour
{
    [SerializeField] private Puzzle lockSource;
    [SerializeField] private Animator animator;

    private void OnEnable() => lockSource.Solved += OpenClock;
    private void OnDisable() => lockSource.Solved -= OpenClock;

    private void OpenClock()
    {
        animator.SetTrigger("Open");
    }
}
