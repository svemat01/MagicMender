using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HotbarSlot : MonoBehaviour
{
    [CanBeNull] public InventoryItemData item;
    private int index = 0;
    public TextMeshProUGUI text;
    public Image icon;
    public GameObject bg;
    public Sprite hotbar_chosen;
    public Sprite hotbar;

    private void Awake()
    {
        icon.enabled = false;
        index = transform.GetSiblingIndex();
        text.text = (index + 1).ToString();

        // Add listener to button
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void SetItem(InventoryItemData item)
    {
        this.item = item;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnClick()
    {
        HotbarController.Instance.selectedSlot = index;
    }

    public void UpdateSelectedSlotUI()
    {
        if (HotbarController.Instance.selectedSlot == index)
        {
            text.color = Color.white;
            text.fontWeight = FontWeight.Bold;
            bg.GetComponent<Image>().sprite = hotbar_chosen;
        }
        else
        {
            text.color = Color.black;
            text.fontWeight = FontWeight.Regular;
            bg.GetComponent<Image>().sprite = hotbar;
        }
    }
}
