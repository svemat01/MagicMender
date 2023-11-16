using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayMoney : MonoBehaviour
{
    public TMP_Text _moneyText;
    // Start is called before the first frame update
    void Start()
    {
        _moneyText.text = PlayerController.Instance.PlayerMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _moneyText.text = PlayerController.Instance.PlayerMoney.ToString();
    }
}
