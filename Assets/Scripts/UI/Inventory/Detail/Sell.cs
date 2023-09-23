using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sell : MonoBehaviour
{
    DetailInfoUI detail;
    Button sellButton;
    private void Awake()
    {
        detail = FindObjectOfType<DetailInfoUI>();
        sellButton = GetComponent<Button>();
        sellButton.onClick.AddListener(detail.SellButtonClick);
    }
}
