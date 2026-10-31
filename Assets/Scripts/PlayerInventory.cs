using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxInventorySize;

    private ItemData[] slots;
    public event Action<ItemData> ItemAdded;

    public bool Add(ItemData item) 
    {
        if (slots.Count() < maxInventorySize)
        {
            int idx = Array.FindIndex(slots, s => s == null);

            // Should never happens, but just to make sure
            if ((idx == -1) is false)
            {
                slots[idx] = item;    
                return true;
            } 
        }

        return false;
    }

    public void UseSelected() 
    {
        
    }

    public void UpdateSelected(KeyCode key)
    {
        return;
    }

    private ItemData GetSelected(int idx)
    {
        return null;
    }

    private void UpdateInventory()
    {

    } 
}
