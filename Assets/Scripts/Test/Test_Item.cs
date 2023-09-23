using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Item : Test_Base
{
    public uint size = 30;
    public ItemCode code = ItemCode.Gun2;
    public uint index = 0;

    DetailInfoUI detail;
    ItemData item;
    GameManager gameManager;
    Inventory inven;
    

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //inven.AddItem(code, 0);
        //inven.AddItem(code, 30);
        detail = FindObjectOfType<DetailInfoUI>();
        Inventory.invenSlot.slotItemData = ScriptableObject.CreateInstance<ItemData>();
    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        gameManager.ResultGetItem();
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        Debug.Log($"{Inventory.invenSlot}");
        Debug.Log($"{Inventory.invenSlot.slotItemData}");
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
    }

    protected override void Test4(InputAction.CallbackContext context)
    {
        inven.ClearSlot(index);
    }

    protected override void Test5(InputAction.CallbackContext context)
    {
        inven.ClearInventory();
    }
}
