using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class CustomerSpawn : MonoBehaviour
{
    public GameObject CustomerPrefab;
    public int currentCustomer = 0;
    private GameObject currentCustomerObject;
    public GameObject CustomerBubble;
    private GameObject currentCustomerBubble;
    // public GameObject ItemPreview;
    // public InventoryItemData data;
    
    // ItemPreview scriptName; // local variable to script instance in this object

    public float movementDistance = 9.0f; // Distance to move
    public float movementSpeed = 2.0f; // Speed of movement

    // string[][] orders;

    public InventoryItemData[] Orders;
    
    // Use this for initialization
    void Start()
    {
        // orders = new string[][]
        // {
        //     new string[] { "order1", "ironsword" },
        //     new string[] { "order2", "ironswordmagical" }
        // };

        // CustomerClick();
        // scriptName = gameObject.GetComponent<UpdateItem>(IronItem, 1);
        Debug.Log("yeet");
        Debug.Log(this);
        CreateCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        // Add any update logic if needed
    }

    public void CreateCustomer()
    {
        System.Random randomCustomer = new System.Random();

        currentCustomerObject = Instantiate(CustomerPrefab, transform.position, Quaternion.identity);
        var spriteRenderer = currentCustomerObject.GetComponent<SpriteRenderer>();
        var sprite = Resources.Load<Sprite>("Sprites/Customers/customer" + (randomCustomer.Next(3) + 1));
        spriteRenderer.sprite = sprite;

        var cSCript = currentCustomerObject.GetComponent<CustomerScript>();
        Debug.Log(cSCript);
        cSCript.spawner = this;
        Debug.Log("Sprite Created");
        
        // Take random order from orders lsit
        var order = Orders[randomCustomer.Next(Orders.Length)];
        
        cSCript.SetOrder(new []{order});

        StartCoroutine(MoveCustomerRight());

        //         if (CustomerBubble != null)
        //         {
        //             
        //             // Instantiate the bubble as a child of the customer sprite
        //             /*currentCustomerBubble = Instantiate(CustomerBubble, currentCustomerObject.transform);
        //             currentCustomerBubble.transform.localPosition = new Vector3(0, 0, 0);
        //
        //             var bubbleRenderer = currentCustomerBubble.GetComponent<SpriteRenderer>();
        //             var bubbleSprite = Resources.Load<Sprite>("Orders/" + orders[1][1]);
        //             Debug.Log(orders[1][1]);
        //             bubbleRenderer.sprite = bubbleSprite;*/
        //
        //             Debug.Log("Bubble Created");
        //         }
        //         else
        //         {
        //             Debug.LogError("CustomerBubble is not assigned in the Inspector.");
        //         }
    }

    IEnumerator MoveCustomerRight()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = currentCustomerObject.transform.position;
        Vector3 targetPosition = initialPosition + Vector3.right * movementDistance;

        // Move the customer to the right over a specific duration
        while (elapsedTime < movementSpeed)
        {
            currentCustomerObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, (elapsedTime / movementSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Customer has moved to the right.");

        // Pause for 2 seconds (adjust as needed)
        yield return new WaitForSeconds(2f);

        Debug.Log("Pause is over. Resuming game...");
        // Resume the game or perform any other desired actions after the pause
    }

    public void CustomerClick()
    {
        if (currentCustomerObject == null)
        {
            CreateCustomer();
        }
        else
        {
            Debug.Log("Customer GameObject found");

            /*currentCustomerObject.transform.position -= transform.right * MoveSpeed * Time.deltaTime;
            Thread.Sleep(1000);*/
            Destroy(currentCustomerObject);
            currentCustomerObject = null;
            CreateCustomer();
        }
        Debug.Log("Clicked");
    }
}
