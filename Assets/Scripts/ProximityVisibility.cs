using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityVisibility : MonoBehaviour
{
    public float maxDistance = 2f;
    public Vector3 offset = Vector2.zero;
    
    public GameObject[] objectsToHide;
    public GameObject[] objectsToShow;

    private void Awake()
    {
        // debug log current position, offset and position with offset
        Debug.Log(gameObject.name);
        Debug.Log("Current position: " + transform.position);
        Debug.Log("Offset: " + offset);
        Debug.Log("Position with offset: " + (transform.position + offset));
    }

    void Update()
    {
        if (Vector2.Distance(transform.position + offset, PlayerController.Instance.transform.position) < maxDistance)
        {
            foreach (var obj in objectsToHide)
            {
                obj.SetActive(false);
            }
            foreach (var obj in objectsToShow)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            foreach (var obj in objectsToHide)
            {
                obj.SetActive(true);
            }
            foreach (var obj in objectsToShow)
            {
                obj.SetActive(false);
            }
        }
    }
}
