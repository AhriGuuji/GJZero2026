using System;
using System.Linq;
using UnityEngine;

public class Puzzle : FocusInteractable
{
    [SerializeField] private Piece[] puzzlePieces;
    [SerializeField] private bool waitForPickup = true;

    public bool IsSolved { get; private set; }
    public event Action Solved;

    protected override bool CanFocus => !IsSolved;
    protected override bool CanQuit => !IsSolved;       // Q disabled while waiting for the pickup
    protected override bool CompleteOnFocus => false;   // completes only when solved

    private void Start()
    {
        foreach (Piece piece in puzzlePieces)
            piece.Touch += VerifyConditions;
    }

    protected override void OnFocusStart()
    {
        foreach (Piece piece in puzzlePieces)
            piece.enabled = true;
    }

    protected override void OnFocusEnd()
    {
        foreach (Piece piece in puzzlePieces)
            piece.enabled = false;
    }

    private void VerifyConditions()
    {
        if (IsSolved || !puzzlePieces.All(piece => piece.IsPlaced))
        {
            Debug.Log("Is not done");
            return;
        }

        IsSolved = true;

        foreach (Piece piece in puzzlePieces)
        {
            piece.Touch -= VerifyConditions;
            piece.enabled = false;
        }

        Solved?.Invoke();   // ClockLock keeps working unchanged
        RaiseCompleted();   // unlocks the reward

        if (!waitForPickup)
            Finish();
    }
}