using UnityEngine;
using System;

public class Piece : MonoBehaviour
{
    public bool IsPlaced { get; protected set; }
    protected bool _beingUsed;

    private void Start()
    {
        _beingUsed = false;
    }

    public void Use()
    {
        _beingUsed = !_beingUsed;
    }

    public event Action Touch;

    protected void OnTouch()
    {
        Touch?.Invoke();
    }
}