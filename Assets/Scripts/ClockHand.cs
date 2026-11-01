using UnityEngine;
using UnityEngine.InputSystem;

public class ClockHand : Piece
{
    [SerializeField] private Transform center;
    [Range(0,360)]
    [SerializeField] private float zRightValue = 90;

    private void Update()
    {
        if (_beingUsed)
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
                new Vector3(mouseScreenPos.x, mouseScreenPos.y, Camera.main.transform.position.z - center.position.z));
            Vector3 currentDir = (transform.position - center.position);
            Vector3 mouseDir   = (mouseWorld - center.position);
            currentDir.x = 0f;
            mouseDir.x = 0f;

            float currentAngle = Mathf.Atan2(currentDir.z, currentDir.y) * Mathf.Rad2Deg;
            float targetAngle  = Mathf.Atan2(mouseDir.z,   mouseDir.y)   * Mathf.Rad2Deg;
            float delta        = Mathf.DeltaAngle(currentAngle, targetAngle);

            transform.RotateAround(center.position, Vector3.right, delta);

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Use();
                IsPlaced = Mathf.Abs(transform.rotation.eulerAngles.z - zRightValue) < 5f;
                OnTouch();
            }
        }
    }
}