using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    public PlayerController PlayRef;

    public CustomerSpawn spawner;
    public GameObject CustomerBubble;
    public InventoryItemData[] Order;
    public GameObject ItemPreviewPrefab;
    public float maxDistance = 5f;

    public float movementDistance = 9.0f; // Distance to move
    public float movementSpeed = 2.0f; // Speed of movement

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
        // Start moving the sprite
        StartCoroutine(MoveSpriteLeft());
        PlayRef.PlayerMoney += 50;
        Debug.Log(PlayRef.PlayerMoney.ToString());
    }

    IEnumerator MoveSpriteLeft()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.left * movementDistance;

        // Move the sprite to the left over a specific duration
        while (elapsedTime < movementSpeed)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, (elapsedTime / movementSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Sprite has moved to the left.");

        // Pause for 2 seconds (adjust as needed)
        yield return new WaitForSeconds(3f);

        Debug.Log("Pause is over. Resuming game...");
        // Resume the game or perform any other desired actions after the pause

        spawner.CreateCustomer();
        Destroy(this.gameObject);
    }

    /*private void OnMouseDown()
    {
        spawner.CreateCustomer();
        Destroy(this.gameObject);
        // spawner.CustomerClick();
    }*/
}
