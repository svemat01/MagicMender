using UnityEngine;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour
{
    public InventoryItemData data;
    // Icon renderer
    public Image image;
    // Item amount text
    public TMPro.TextMeshProUGUI amountText;
    
    public void UpdateItem(InventoryItemData itemData, int amount)
    {
        data = itemData;
        image.sprite = data.icon;
        
        if (amount > 1)
        {
            amountText.text = "x" + amount.ToString();
        }
        else
        {
            amountText.text = "";
        }
    }
}