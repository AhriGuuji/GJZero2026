using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxOccupiedInventorySize;
    [SerializeField] private Image[] slotsImage;
    [SerializeField] private Color unselectedColor;
    [SerializeField] private Color selectedColor;

    private int occupiedSlotsCnt;
    private ItemData[] slots;
    public event Action<ItemData> ItemAddedmax;

    private void Start()
    {
        slots = new ItemData[maxOccupiedInventorySize];
        UpdateInventory();
    }

    public bool Add(ItemData item) 
    {
        if (occupiedSlotsCnt < maxOccupiedInventorySize)
        {
            int idx = Array.FindIndex(slots, s => s == null);

            // Should never happens, but just to make sure
            if ((idx == -1) is false)
            {
                slots[idx] = item;
                UpdateInventory();
                return true;
            } 
        }

        return false;
    }

    public void UseItems(ItemData[] items) 
    {
        foreach (var item in items) Remove(item);
    }

    public void UpdateSelected(int idx)
    {
        for (int i = 0; i < slotsImage.Length; i++)
        {
            if (idx == i) slotsImage[i].color = selectedColor;
            else slotsImage[i].color = unselectedColor;
        }
    }

    private ItemData GetSelected(int idx)
    {
        return null;
    }

    private void UpdateInventory()
    {
        occupiedSlotsCnt = slots.Count(x => x != null);

        for (int i = 0; i < maxOccupiedInventorySize; i++)
        {
            bool has = slots[i] != null;
            slotsImage[i].sprite = has ? slots[i].icon : null;  
        }
    }

    public bool Has(ItemData item) => Array.IndexOf(slots, item) != -1;

    public bool Remove(ItemData item)
    {
        int idx = Array.IndexOf(slots, item);
        if (idx == -1) return false;

        slots[idx] = null;
        UpdateInventory();
        return true;
    }
}
