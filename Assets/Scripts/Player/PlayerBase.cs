using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : CharacterBase
{
    Enemy1 enemy1;
    Enemy2 enemy2;
    Enemy3 enemy3;
    GameObject enemy;
    PlayerInputAction inputAction;

    private void Awake()
    {
        enemy1 = FindObjectOfType<Enemy1>();
        enemy2 = FindObjectOfType<Enemy2>();
        enemy3 = FindObjectOfType<Enemy3>();
        inputAction = new PlayerInputAction();
    }

    private void Start()
    {

    }
    private void OnEnable()     // inputAction Ȱ��ȭ
    {
        inputAction.Player.Enable();
        inputAction.Player.B1.performed += B1;    // �⺻���� ���� Ȱ��ȭ
        inputAction.Player.B2.performed += B2;      // ��ų ���� Ȱ��ȭ
        inputAction.Player.B3.performed += B3;  // �� ���� Ȱ��ȭ
    }



    private void OnDisable()    // inputAction ��Ȱ��ȭ
    {
        inputAction.Player.B1.performed -= B1;  // �� ���� ��Ȱ��ȭ
        inputAction.Player.B2.performed -= B2;              // ��ų ���� ��Ȱ��ȭ
        inputAction.Player.B3.performed -= B3;         // �⺻���� ���� ��Ȱ��ȭ
        inputAction.Player.Disable();
    }

    private void B1(InputAction.CallbackContext value)    // �Ϲݰ��� ���ý�(Ű���� 1)
    {
        Debug.Log(1);
        enemy1.getDemage(5, 0);
    }

    private void B2(InputAction.CallbackContext value)     // ��ų���� ���ý�(Ű���� 2)
    {
        Debug.Log(2);
        enemy2.getDemage(5, 0);
    }

    private void B3(InputAction.CallbackContext value)
    {
        Debug.Log(3);
        enemy3.getDemage(5, 0);
    }
}
