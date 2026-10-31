using UnityEngine;
using System;

public class Piece : MonoBehaviour
{
    public bool IsPlaced { get; private set; }
    private bool _beingUsed;

    public void Use()
    {
        _beingUsed = !_beingUsed;
    }

    public event Action Touch;

    private void OnTouch()
    {
        Touch?.Invoke();
    }
}