using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    public customerSpawn spawner;
    public GameObject CustomerPrefab;

    private void OnMouseDown()
    {
        spawner.CustomerClick();
    }
}
