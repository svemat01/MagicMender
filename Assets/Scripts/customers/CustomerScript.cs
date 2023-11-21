using System;
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

    public float horizontalMovementDistance = 9.0f; // Distance to move
    public float verticalMovementDistance = 3.0f;
    public float movementSpeed = 2.5f; // Speed of movement

    public string timeStamp = "";
    public long timeNow;

    private float despawnTime = 60.0f; // Time in seconds before despawning
    private float despawnTimer = 0.0f; // Timer to track despawn time
    private bool isDespawning = false;

    public float MoneyTime = 80.0f;

    private IEnumerator MoveSpriteLeftAndDownCoroutine;

    public bool orderComplete = true;

    void Start()
    {
        Debug.Log(PlayerController.Instance.PlayerMoney);
        timeNow = DateTimeOffset.Now.ToUnixTimeSeconds();
        timeStamp = timeNow.ToString();
        despawnTimer = Time.time + despawnTime; // Set the despawn timer

        MoveSpriteLeftAndDownCoroutine = MoveSpriteLeftAndDown();

    }
    void Update()
    {
        timeNow = DateTimeOffset.Now.ToUnixTimeSeconds();
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
                    float timeRemaining = despawnTimer - Time.time;
                    if (timeRemaining <= 20.0f)
                    {
                        MoneyTime = 40.0f;
                    }
                    else
                    {
                        MoneyTime = 80.0f;
                    }
                    // Despawn the customer
                    PlayerController.Instance.PlayerMoney += MoneyTime;
                    Despawn();
                    spawner.completedCustomer += 1;
                    Debug.Log("Completed Customers: " + spawner.completedCustomer);
                }
            }
        }
        if (Time.time > despawnTimer)
        {
            if (!isDespawning)
            {
                isDespawning = true;
                PlayerController.Instance.PlayerMoney -= 10.0f;
                StartCoroutine(MoveSpriteLeftAndDownCoroutine);
                Despawn();
                Debug.Log(PlayerController.Instance.PlayerMoney);
            }
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            if (!isDespawning)
            {
                isDespawning = true;
                PlayerController.Instance.PlayerMoney -= 10.0f;
                StartCoroutine(MoveSpriteLeftAndDownCoroutine);
                Despawn();
                Debug.Log(PlayerController.Instance.PlayerMoney);
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
        StartCoroutine(MoveSpriteLeftAndDown());
        Debug.Log(PlayerController.Instance.PlayerMoney);
    }

    private IEnumerator MoveSpriteLeftAndDown()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 verticalTargetPosition = initialPosition + Vector3.down * verticalMovementDistance;
        Vector3 horizontalTargetPosition = verticalTargetPosition + Vector3.left * horizontalMovementDistance; // Move up by 2 units

        // Move the customer horizontally to the right over a specific duration
        while (elapsedTime < movementSpeed)
        {
            transform.position = Vector3.Lerp(initialPosition, verticalTargetPosition, (elapsedTime / movementSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f; // Reset the elapsed time for vertical movement

        // Move the customer up vertically over a specific duration
        while (elapsedTime < movementSpeed)
        {
            transform.position = Vector3.Lerp(verticalTargetPosition, horizontalTargetPosition, (elapsedTime / movementSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Customer has moved to the right and up.");

        // Pause for 2 seconds (adjust as needed)
        yield return new WaitForSeconds(0.0f);

        Debug.Log("Pause is over. Resuming game...");
        // Resume the game or perform any other desired actions after the pause
        Destroy(this.gameObject);

    }


    /*private void OnMouseDown()
    {
        spawner.CreateCustomer();
        Destroy(this.gameObject);
        // spawner.CustomerClick();
    }*/
}
