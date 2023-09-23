using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OpenButton : MonoBehaviour
{
    InventoryUI invenLink;
    Button openButton;

    private void Awake()
    {
        invenLink = FindObjectOfType<InventoryUI>();
        openButton = GetComponent<Button>();
        openButton.onClick.AddListener(invenLink.Open);
    }

}
