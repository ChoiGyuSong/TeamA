using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Inventory : Test_Base
{
    public uint size = 6;
    public ItemCode code = ItemCode.Gun1;
    public uint index = 0;

    Inventory inven;

    private void Start()
    {
        inven = new Inventory(null, size);
    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        inven.AddItem(code);
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        inven.AddItem(code, index);
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
    }

    protected override void Test4(InputAction.CallbackContext context)
    {
    }

    protected override void Test5(InputAction.CallbackContext context)
    {
    }
}
