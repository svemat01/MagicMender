using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class customerSpawn : MonoBehaviour
{
    public GameObject CustomerPrefab;
    public int currentCustomer = 0;
    private GameObject currentCustomerObject;
    public GameObject CustomerBubble;
    private GameObject currentCustomerBubble;

    public float MoveSpeed = 10f;

    // Use this for initialization
    void Start()
    {
        CustomerClick();
    }

    // Update is called once per frame
    void Update()
    {
        // Add any update logic if needed
    }

    public void CreateCustomer()
    {
        System.Random randomCustomer = new System.Random();

        currentCustomerObject = Instantiate(CustomerPrefab, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        var spriteRenderer = currentCustomerObject.GetComponent<SpriteRenderer>();
        var sprite = Resources.Load<Sprite>("Sprites/Customers/customer" + (randomCustomer.Next(3) + 1));
        spriteRenderer.sprite = sprite;

        var cSCript = currentCustomerObject.GetComponent<CustomerScript>();
        cSCript.spawner = this;
        Debug.Log("Sprite Created");

        if (CustomerBubble != null)
        {
            currentCustomerBubble = Instantiate(CustomerBubble, new Vector3(1, 2, 0), Quaternion.identity);
            var bubbleRenderer = currentCustomerBubble.GetComponent<SpriteRenderer>();
            Debug.Log("Bubble Created");
        }
        else
        {
            Debug.LogError("CustomerBubble is not assigned in the Inspector.");
        }
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
