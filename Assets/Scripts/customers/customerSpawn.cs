using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class customerSpawn : MonoBehaviour
{

    public GameObject CustomerPrefab;

    public int currentCustomer = 0;

    // Use this for initialization
    void Start()
    {
        CustomerClick();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void CreateCustomer()
    {
        System.Random randomCustomer = new System.Random();

        var customer = Instantiate(CustomerPrefab, new Vector3(0.5f, 1, 0), Quaternion.identity);
        var spriteRenderer = customer.GetComponent<SpriteRenderer>();

        var sprite = Resources.Load<Sprite>("Sprites/Customers/customer" + (randomCustomer.Next(3) + 1));
        spriteRenderer.sprite = sprite;

        var cSCript = customer.GetComponent<CustomerScript>();
        cSCript.spawner = this;
        Debug.Log("Sprite Created");
    }

    public void CustomerClick()
    {
        if (currentCustomer == 0)
        {
            CreateCustomer();
            currentCustomer = 1;
        } else {

        }
        Debug.Log("Clicked");
    }
}