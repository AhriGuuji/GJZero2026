using UnityEngine;

public class DrawerLock : MonoBehaviour
{
    [SerializeField] private FocusInteractable lockSource;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip drawerSound;

    private void OnEnable() => lockSource.Completed += OpenDrawer;
    private void OnDisable() => lockSource.Completed -= OpenDrawer;

    private void OpenDrawer()
    {
        if (drawerSound != null)
        {
            AudioManager.Instance.PlaySound(drawerSound);
        }
        animator.SetTrigger("Open");
    }
}
