using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItemData itemData;
    private float maxDistance = 3f;

    private void OnMouseDown()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > maxDistance)
        {
            Debug.Log("Too far away");
            return;
        }
        
        Debug.Log("Picked up " + itemData.name);
        
        if (HotbarController.Instance.AddItem(itemData))
        {
            Destroy(this.gameObject);
        }
    }
}
