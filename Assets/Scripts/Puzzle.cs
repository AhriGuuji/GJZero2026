using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Puzzle : Interactable
{
    [SerializeField] private Camera puzzleCam;
    [SerializeField] private Piece[] puzzlePieces;
    [SerializeField] private ItemData reward;
    [SerializeField] private GameObject[] objToActivateWhenPuzzleStart;
    [SerializeField] private bool waitForPickup = true;

    private Interactor _currentInteractor;
    private PuzzleInteractor _puzzleInteractor;
    private Movement _playerMovement;
    private PlayerInventory _playerInventory;
    public bool DoingIt { get; private set; }
    public bool IsSolved { get; private set; }
    public Camera PuzzleCam => puzzleCam;

    public event Action Solved;

    private void Start()
    {
        IsSolved = false;
        DoingIt = false;

        foreach (Piece piece in puzzlePieces)
        {
            piece.Touch += VerifyConditions;
        }
    }

    public override void TryInteract(Interactor interactor)
    {
        Debug.Log("DOing it");
        if (IsSolved)
            return; 

        puzzleCam.gameObject.SetActive(true);

        _currentInteractor = interactor;
        _puzzleInteractor = interactor.GetComponent<PuzzleInteractor>();
        _playerMovement = interactor.GetComponent<Movement>();
        _playerInventory = interactor.GetComponent<PlayerInventory>();

        _currentInteractor.enabled = false;
        _playerMovement.enabled = false;
        _puzzleInteractor.enabled = true;
        _puzzleInteractor.SetCamera(puzzleCam);

        foreach (Piece piece in puzzlePieces)
        {
            piece.enabled = true;
        }

        foreach (var obj in objToActivateWhenPuzzleStart)
            obj.SetActive(true);

        DoingIt = true;
    }

    private void Update()
    {
        if (!DoingIt || IsSolved) // Q is disabled while waiting for the pickup
            return;

        if (Keyboard.current.qKey.wasPressedThisFrame)
            QuitPuzzle();
    }

    private void QuitPuzzle()
    {
        puzzleCam.gameObject.SetActive(false);
        _currentInteractor.enabled = true;
        _playerMovement.enabled = true;
        _puzzleInteractor.enabled = false;

        foreach (Piece piece in puzzlePieces)
        {
            piece.enabled = false;
        }

        foreach(var obj in objToActivateWhenPuzzleStart)
            obj.SetActive(false);

        DoingIt = false;
    }

    private void VerifyConditions()
    {
        if (IsSolved || !puzzlePieces.All(piece => piece.IsPlaced))
            return;

        IsSolved = true;

        foreach (Piece piece in puzzlePieces)
        {
            piece.Touch -= VerifyConditions;
            piece.enabled = false; // hands stop reacting to J/K
        }

        Solved?.Invoke(); // ClockLock plays the open animation

        if (!waitForPickup)
            QuitPuzzle();
    }

    // Called by whatever finishes the puzzle flow (e.g. the reward pickup)
    public void Finish() => QuitPuzzle();


}
