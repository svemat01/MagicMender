using System.Collections.Generic;
using UnityEngine;

public class CraftingPopup : MonoBehaviour
{
    public GameObject ItemsContainer;
    public GameObject ItemPreviewPrefab;
    public GameObject CraftingText;

    
    public void UpdateItems(InventoryItemData[] items)
    {
        foreach (Transform child in ItemsContainer.transform)
        {
            Destroy(child.gameObject);

            CraftingText.SetActive(true);
        }
        

        
        // ItemsContainer is part of a menu showing all items in a crafting station. It should show which unique items are currenly stored in the crafting station. for each item it should show how many are in there. So if the items array is [iron, iron, copper] it should show 2x iron 1x copper. This is done via the ItemPReview script on the preview prefab with its UpdateItem(data, amount) function

        // convert the items array to a dictionary with the item as key and the amount as value
        var itemDict = new Dictionary<InventoryItemData, int>();
        
        foreach (var item in items)
        {
            CraftingText.SetActive(false);
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
            var preview = Instantiate(ItemPreviewPrefab, ItemsContainer.transform);
            preview.GetComponent<ItemPreview>().UpdateItem(item.Key, item.Value);
        }
    }
}