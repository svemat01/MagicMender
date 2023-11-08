using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItemData itemData;

    private void OnMouseDown()
    {
        // DISABLED until we've got proper player access
        // if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) > 5f)
        // {
        //     Debug.Log("Too far away");
        //     return;
        // }
        
        Debug.Log("Picked up " + itemData.name);
        
        if (HotbarController.Instance.AddItem(itemData))
        {
            Destroy(gameObject);
        }
    }
}
