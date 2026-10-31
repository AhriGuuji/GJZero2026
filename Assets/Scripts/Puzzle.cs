using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Puzzle : Interactable
{
    [SerializeField] private Camera puzzleCam;
    [SerializeField] private Piece[] puzzlePieces;
    private Interactor _currentInteractor;
    private PuzzleInteractor _puzzleInteractor;
    private Movement _playerMovement;
    public bool DoingIt { get; private set; }
    public bool IsSolved { get; private set; }

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

        _currentInteractor.enabled = false;
        _playerMovement.enabled = false;
        _puzzleInteractor.enabled = true;
        _puzzleInteractor.SetCamera(puzzleCam);

        DoingIt = true;
    }

    private void Update()
    {
        if (!DoingIt)
            return;
        
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            QuitPuzzle();
        }
    }

    private void QuitPuzzle()
    {
        puzzleCam.gameObject.SetActive(false);
        _currentInteractor.enabled = true;
        _playerMovement.enabled = true;
        _puzzleInteractor.enabled = false;
        DoingIt = false;
    }

    private void VerifyConditions()
    {
        IsSolved = puzzlePieces.All(piece => piece.IsPlaced);

        if (IsSolved)
        {
            foreach (Piece piece in puzzlePieces)
            {
                piece.Touch -= VerifyConditions;
            }

            QuitPuzzle();
            this.enabled = false;
        }
    }
}
