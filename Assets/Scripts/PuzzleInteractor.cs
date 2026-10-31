using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayerMask;
    private Camera _currentCamera;
    public void SetCamera(Camera cam)
    {
        _currentCamera = cam;
    }

    private void Start()
    {
        enabled = false;
    }

    public void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = _currentCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, interactableLayerMask))
        {
            if (hit.collider.TryGetComponent(out Piece component))
            {
                component.Use();
            }
        }
    }
}