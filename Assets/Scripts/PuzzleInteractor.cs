using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleInteractor : MonoBehaviour
{
    private Camera _currentCamera;
    public void SetCamera(Camera cam)
    {
        _currentCamera = cam;
    }

    public void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Ray ray = _currentCamera.ScreenPointToRay(mousePos);
            
            Physics.Raycast(ray, out RaycastHit hit);

            if  (hit.collider.TryGetComponent(out Piece component))
            {
                component.Use();
            }
        }
    }
}