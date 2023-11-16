using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    public PlayerController PlayRef;

    // Start is called before the first frame update
    void Start()
    {
        PlayRef.PlayerMoney = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayRef.PlayerMoney.ToString());
    }
}
