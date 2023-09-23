using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Base : MonoBehaviour
{
    protected PlayerInputAction inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
    }

    protected virtual void OnEnable()
    {
        inputActions.Test.Enable();
        inputActions.Test.Test_1.performed += Test1;
        inputActions.Test.Test_2.performed += Test2;
        inputActions.Test.Test_3.performed += Test3;
        inputActions.Test.Test_4.performed += Test4;
        inputActions.Test.Test_5.performed += Test5;
    }

    protected virtual void OnDisable()
    {
        inputActions.Test.Test_5.performed -= Test5;
        inputActions.Test.Test_4.performed -= Test4;
        inputActions.Test.Test_3.performed -= Test3;
        inputActions.Test.Test_2.performed -= Test2;
        inputActions.Test.Test_1.performed -= Test1;
        inputActions.Test.Disable();
    }

    protected virtual void Test1(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }

    protected virtual void Test2(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }

    protected virtual void Test3(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }

    protected virtual void Test4(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }

    protected virtual void Test5(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }
}
