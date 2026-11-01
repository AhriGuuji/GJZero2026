using UnityEngine;

public class PotionPiece : Piece
{
    [SerializeField] private Outline outline;
    [SerializeField] private Caldron caldron;
    private void Update()
    {
        if (_beingUsed)
        {
            outline.enabled = true;
            IsPlaced = caldron.AddToCaldron(this);
            OnTouch();
            _beingUsed = false; 
        }
        else outline.enabled = false;
    }

    public void Reset()
    {
        IsPlaced = false;
    }
}
