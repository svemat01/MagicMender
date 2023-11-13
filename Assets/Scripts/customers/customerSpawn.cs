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

    public float MoveSpeed = 10f;

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
         
        CustomerClick();
        // scriptName = gameObject.GetComponent<UpdateItem>(IronItem, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Add any update logic if needed
    }

    public void CreateCustomer()
    {
        System.Random randomCustomer = new System.Random();

        currentCustomerObject = Instantiate(CustomerPrefab, new Vector3(0.5f, 1, 0), Quaternion.identity);
        var spriteRenderer = currentCustomerObject.GetComponent<SpriteRenderer>();
        var sprite = Resources.Load<Sprite>("Sprites/Customers/customer" + (randomCustomer.Next(3) + 1));
        spriteRenderer.sprite = sprite;

        var cSCript = currentCustomerObject.GetComponent<CustomerScript>();
        cSCript.spawner = this;
        Debug.Log("Sprite Created");
        
        // Take random order from orders lsit
        var order = Orders[randomCustomer.Next(Orders.Length)];
        
        cSCript.SetOrder(new []{order});

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
