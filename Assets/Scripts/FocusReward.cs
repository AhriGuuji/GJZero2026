using UnityEngine;
using UnityEngine.InputSystem;

public class FocusReward : MonoBehaviour
{
    [SerializeField] private FocusInteractable source; // a Puzzle or a plain FocusInteractable
    [SerializeField] private ItemData item;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private Collider pickupCollider;
    [SerializeField] private AudioClip pickupAudio;

    private bool _unlocked;

    private void Awake() => pickupCollider.enabled = false;
    private void OnEnable() => source.Completed += Unlock;
    private void OnDisable() => source.Completed -= Unlock;

    // Public so an Animation Event can call it instead
    public void Unlock()
    {
        _unlocked = true;
        pickupCollider.enabled = true;
    }

    private void Update()
    {
        if (!_unlocked || !Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Ray ray = source.FocusCam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider == pickupCollider)
            Collect();
    }

    private void Collect()
    {
        AudioManager.Instance.PlaySound(pickupAudio);
        _unlocked = false;
        inventory.Add(item);
        source.Finish();
        gameObject.SetActive(false);
    }
}