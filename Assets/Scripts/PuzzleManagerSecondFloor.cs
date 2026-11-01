using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerSecondFloor : MonoBehaviour
{
    [SerializeField] private int numberOfPuzzles = 0;
    
    [SerializeField] private Altar altar;

    [SerializeField] private List<Puzzle> puzzles;
    [SerializeField] private List<ItemLock> itemLocks;


    private int completedPuzzleCtn;

    private void OnEnable()
    {
        foreach (var puzzle in puzzles)
            puzzle.Solved += IncreasePuzzleCompleted;

        foreach (var itemLock in itemLocks)
            itemLock.Solved += IncreasePuzzleCompleted;
    }

    private void OnDisable()
    {
        foreach (var puzzle in puzzles)
            puzzle.Solved -= IncreasePuzzleCompleted;

        foreach (var itemLock in itemLocks)
            itemLock.Solved -= IncreasePuzzleCompleted;
    }

    private void IncreasePuzzleCompleted()
    {
        completedPuzzleCtn++;

        if (completedPuzzleCtn >= numberOfPuzzles)
        {
            altar.SetInteractable();
        }
    }
}
