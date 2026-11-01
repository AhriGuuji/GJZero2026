using UnityEngine;

public class DrawerLock : MonoBehaviour
{
    [SerializeField] private FocusInteractable lockSource;
    [SerializeField] private Animator animator;

    private void OnEnable() => lockSource.Completed += OpenDrawer;
    private void OnDisable() => lockSource.Completed -= OpenDrawer;

    private void OpenDrawer()
    {
        animator.SetTrigger("Open");
    }
}
