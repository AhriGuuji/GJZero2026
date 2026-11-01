using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Book : MonoBehaviour
{
    [SerializeField] private GameObject book;
    private Movement _movement;
    private PuzzleInteractor _puzzleInteractor;
    private void Start()
    {
        _movement = GetComponent<Movement>();
        _puzzleInteractor = GetComponent<PuzzleInteractor>();
    }

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (!book.activeSelf & !_puzzleInteractor.enabled)
            {
                book.SetActive(true);
                _movement.enabled = false;
            } 
            else 
            {
                book.SetActive(false);
                _movement.enabled = true;
            }
        }
    }
}
