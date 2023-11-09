using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    public customerSpawn spawner;

    private void OnMouseDown()
    {
        spawner.CustomerClick();
    }
}
