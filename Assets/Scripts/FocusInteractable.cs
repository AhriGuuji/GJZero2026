using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FocusInteractable : Interactable
{
    [Header("Focus Settings")]
    [SerializeField] private Camera puzzleCam;
    [SerializeField] private GameObject[] objToActivateWhenPuzzleStart;

    private Interactor _currentInteractor;
    private PuzzleInteractor _puzzleInteractor;
    private Movement _playerMovement;
    private bool _finished;

    public bool IsFocused { get; private set; }
    public Camera FocusCam => puzzleCam;

    // Fires when the reward should become available:
    // on entering the focus (non-puzzle) or when solved (Puzzle)
    public event Action Completed;

    protected virtual bool CanFocus => true;
    protected virtual bool CanQuit => true;
    protected virtual bool CompleteOnFocus => true;
    protected virtual void OnFocusStart() { }
    protected virtual void OnFocusEnd() { }
    protected void RaiseCompleted() => Completed?.Invoke();

    public override void TryInteract(Interactor interactor)
    {
        if (_finished || IsFocused || !CanFocus)
            return;

        puzzleCam.gameObject.SetActive(true);

        _currentInteractor = interactor;
        _puzzleInteractor = interactor.GetComponent<PuzzleInteractor>();
        _playerMovement = interactor.GetComponent<Movement>();

        _currentInteractor.enabled = false;
        _playerMovement.enabled = false;
        _puzzleInteractor.enabled = true;
        _puzzleInteractor.SetCamera(puzzleCam);

        foreach (var obj in objToActivateWhenPuzzleStart)
            obj.SetActive(true);

        IsFocused = true;
        OnFocusStart();

        if (CompleteOnFocus)
            RaiseCompleted();
    }

    private void Update()
    {
        if (IsFocused && CanQuit && Keyboard.current.qKey.wasPressedThisFrame)
            EndFocus();
    }

    // Called when the flow is over (e.g. the reward was picked up).
    // The object can't be entered again afterwards.
    public void Finish()
    {
        _finished = true;
        EndFocus();
    }

    private void EndFocus()
    {
        if (!IsFocused)
            return;

        puzzleCam.gameObject.SetActive(false);
        _currentInteractor.enabled = true;
        _playerMovement.enabled = true;
        _puzzleInteractor.enabled = false;

        foreach (var obj in objToActivateWhenPuzzleStart)
            obj.SetActive(false);

        IsFocused = false;
        OnFocusEnd();
    }
}