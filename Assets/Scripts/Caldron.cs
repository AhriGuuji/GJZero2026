using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Caldron : MonoBehaviour 
{
    [SerializeField] private List<PotionPiece> orderedPotions;
    [SerializeField] private List<ItemData> potionsData;
    [SerializeField] private PlayerInventory playerInventory;
    private List<PotionPiece> potions;
    public List<PotionPiece> Potions => potions;

    private void OnEnable() 
    {
        potions = new ();   

        for (int i = 0; i < orderedPotions.Count; i++)
        {
            if (playerInventory.Has(potionsData[i]))
                orderedPotions[i].gameObject.SetActive(true);
            else
                orderedPotions[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            CleanCaldron();
        }
    }

    public bool AddToCaldron(PotionPiece potion)
    {
        potions.Add(potion);
        potion.gameObject.SetActive(false);
        if (potions.IndexOf(potion) == orderedPotions.IndexOf(potion))
            return true;
        else
            return false;
    }

    public void CleanCaldron()
    {
        foreach (PotionPiece potion in potions)
            potion.Reset();

        potions = new();
        for (int i = 0; i < orderedPotions.Count; i++)
        {
            if (playerInventory.Has(potionsData[i]))
                orderedPotions[i].gameObject.SetActive(true);
            else
                orderedPotions[i].gameObject.SetActive(false);
        }
    }
}