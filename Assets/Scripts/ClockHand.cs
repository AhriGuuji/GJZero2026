using UnityEngine;
using UnityEngine.InputSystem;

public class ClockHand : Piece
{
    [SerializeField] private float rotationSpeed = 5.0f;
    [Range(0,360)]
    [SerializeField] private float yRightValue = 90;
    [SerializeField] private bool isMinuteHand;
 
    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float direction = 0f;

        if (Keyboard.current.kKey.isPressed)
            direction = 1f;

        if (Keyboard.current.jKey.isPressed)
            direction = -1f;

        if (direction == 0f)
            return;

        RotateHands(direction);
    }

    private void RotateHands(float direction)
    {
        float rotationAmount = direction * rotationSpeed * Time.deltaTime;

        if (isMinuteHand) transform.Rotate(0f, -rotationAmount, 0f);
        else transform.Rotate(0f, -rotationAmount / 12f, 0);
        Debug.Log("Im " + name + " and my angle is: " + transform.localEulerAngles.y);

        IsPlaced = Mathf.Abs(Mathf.DeltaAngle(transform.localEulerAngles.y, yRightValue)) < 5f;
        OnTouch();
    }
}