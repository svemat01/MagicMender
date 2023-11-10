using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class customerSpawn : MonoBehaviour
{
    public GameObject CustomerPrefab;
    public int currentCustomer = 0;
    private GameObject currentCustomerGameObject;
    private GameObject currentCustomerObject;

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
            Destroy(currentCustomerObject);
            currentCustomerObject = null;
            CreateCustomer();
        }
        Debug.Log("Clicked");
    }
}
