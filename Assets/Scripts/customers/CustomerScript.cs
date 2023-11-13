using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    public CustomerSpawn spawner;
    public GameObject CustomerBubble;
    public InventoryItemData[] Order;
    public GameObject ItemPreviewPrefab;
    public float maxDistance = 5f;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < maxDistance)
        {
            //CHeck if current item is in order
            InventoryItemData selectedItem = HotbarController.Instance.GetActiveItem();
            
            if (selectedItem != null)
            {
                foreach (var item in Order)
                {
                    if (item == selectedItem)
                    {
                        // Remove the item from the hotbar
                        HotbarController.Instance.ClearSlot(HotbarController.Instance.selectedSlot);
                        // Remove the item from the order
                        var index = System.Array.IndexOf(Order, item);
                        Order[index] = null;
                        UpdateBubble();
                        break;
                    }
                }
                
                // Check if order is complete
                bool orderComplete = true;
                foreach (var item in Order)
                {
                    if (item != null)
                    {
                        orderComplete = false;
                        break;
                    }
                }
                
                if (orderComplete)
                {
                    // Despawn the customer
                    Despawn();
                }
            }
        }
    }
    public void SetOrder(InventoryItemData[] items)
    {
        this.Order = items;
        UpdateBubble();
    }

    public void UpdateBubble()
    {
        foreach (Transform child in CustomerBubble.transform)
        {
            Destroy(child.gameObject);
        }
        
        var itemDict = new Dictionary<InventoryItemData, int>();
        
        foreach (var item in Order)
        {
            if (item == null) continue;
            
            if (itemDict.ContainsKey(item))
            {
                itemDict[item]++;
            }
            else
            {
                itemDict.Add(item, 1);
            }
        }
        
        // for each item in the dictionary, create a new item preview and update it with the item and amount
        foreach (var item in itemDict)
        {
            var preview = Instantiate(ItemPreviewPrefab, CustomerBubble.transform);
            preview.GetComponent<ItemPreview>().UpdateItem(item.Key, item.Value);
        }
    }
    
    private void Despawn()
    {
        spawner.CreateCustomer();
        Destroy(this.gameObject);
    }
    private void OnMouseDown()
    {
        spawner.CreateCustomer();
        Destroy(this.gameObject);
        // spawner.CustomerClick();
    }
}
