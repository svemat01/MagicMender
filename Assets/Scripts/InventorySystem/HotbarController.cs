using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class HotbarController : MonoBehaviour
{
    public static HotbarController Instance;
    public Transform content;
    private List<HotbarSlot> _slots = new List<HotbarSlot>();
    
    public Transform spawnPoint; // where to spawn dropped items in the world, TEMP until we have proper player position

    // int selected slot with getter and setter where setter requires value between 0 and 8
    private int _selectedSlot = 0;
    public int selectedSlot
    {
        get => _selectedSlot;
        set
        {
            if (value < 0 || value > 9)
            {
                Debug.Log("Selected slot must be between 0 and 9");
                return;
            }
            _selectedSlot = value;
            
            Debug.Log("Selected slot " + _selectedSlot);
            
            // Update selected slot UI
            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i].UpdateSelectedSlotUI();
            }
        }
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of HotbarManager");
            return;
        }
        Instance = this;
        
        for (int i = 0; i < content.childCount; i++)
        {
            HotbarSlot slot = content.GetChild(i).GetComponent<HotbarSlot>();
            _slots.Add(slot);
        }
    }
    
    public bool AddItem(InventoryItemData item)
    {
        // find first empty slot
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].item == null)
            {
                _slots[i].SetItem(item);
                Debug.Log("Added " + item.name + " to inventory");
                return true;
            }
        }
        
        // if no empty slots, add to end of list
        return false;
    }
    
    public bool ClearSlot(int index)
    {
        if (index < 0 || index > 8)
        {
            Debug.Log("Selected slot must be between 0 and 8");
            return false;
        }
        _slots[index].ClearSlot();
        return true;
    }
    
    // Get active slot item or null
    [CanBeNull]
    public InventoryItemData GetActiveItem()
    {
        if (_slots.Count > 0)
        {
            return _slots[selectedSlot].item;
        }
        return null;
    }
    
    // Drop the current item, aka remove it from the inventory and spawn it in the world at the player's feet from a prefab
    public void DropActiveItem()
    {
        InventoryItemData item = _slots[selectedSlot].item;
        if (item == null) throw new System.Exception("No item in slot " + selectedSlot);
            
        ClearSlot(selectedSlot);
        Instantiate(item.prefab, spawnPoint.position, Quaternion.identity);
    }
}
