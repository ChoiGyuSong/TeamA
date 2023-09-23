using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    DetailInfoUI detail;
    Button upgradeButton;
    private void Awake()
    {
        detail = FindObjectOfType<DetailInfoUI>();
        upgradeButton = GetComponent<Button>();
        upgradeButton.onClick.AddListener(detail.UpgradeButtonClick);
    }

    // 강화 종료후에는 DetailInfoUI.Open(itemData);
}
