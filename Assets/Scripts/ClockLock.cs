using UnityEngine;

public class ClockLock : MonoBehaviour
{
    [SerializeField] private Puzzle lockSource;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip clockSound;

    private void OnEnable() => lockSource.Solved += OpenClock;
    private void OnDisable() => lockSource.Solved -= OpenClock;

    private void OpenClock()
    {
        if (clockSound != null)
        {
            AudioManager.Instance.PlaySound(clockSound);
        }
        animator.SetTrigger("Open");

    }
}
