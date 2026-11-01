using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleReward : MonoBehaviour
{
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private ItemData item;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private Collider pickupCollider;

    private bool _unlocked;

    private void Awake() => pickupCollider.enabled = false;
    private void OnEnable() => puzzle.Solved += Unlock;
    private void OnDisable() => puzzle.Solved -= Unlock;

    // Public so an Animation Event at the end of the open animation can call it instead
    public void Unlock()
    {
        _unlocked = true;
        pickupCollider.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (!_unlocked || !Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Ray ray = puzzle.PuzzleCam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider == pickupCollider)
            Collect();
    }

    private void Collect()
    {
        _unlocked = false;
        inventory.Add(item);
        puzzle.Finish();              // camera returns to isometric, player regains control
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}