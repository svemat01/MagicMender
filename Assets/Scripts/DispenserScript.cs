using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DispenserScript : MonoBehaviour
{
    public InventoryItemData[] items;
    public int itemIndex = 0;
    public float maxDistance = 2f;
    public Vector3 offset = Vector2.zero;
    
    public TextMeshProUGUI priceText;
    public SpriteRenderer chestIcon;
    public SpriteRenderer popupIcon;

    private void Start()
    {
        UpdateItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Vector2.Distance(transform.position + offset, PlayerController.Instance.transform.position) <
              maxDistance)) return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(PlayerController.Instance.PlayerMoney >= 10){
                var item = items[itemIndex % items.Length];
                // if (PlayerController.Instance.money >= price)
                // {
                // PlayerController.Instance.money -= price;
                HotbarController.Instance.AddItem(item);
                // }
                PlayerController.Instance.PlayerMoney -= 10;
                Debug.Log(PlayerController.Instance.PlayerMoney);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            itemIndex++;
            UpdateItem();
        }
    }

    void UpdateItem()
    {
        var item = items[itemIndex % items.Length];
        priceText.text = item.price.ToString();
        chestIcon.sprite = item.icon;
        popupIcon.sprite = item.icon;
    }
}
