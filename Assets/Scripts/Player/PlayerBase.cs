using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class PlayerBase : CharacterBase
{
    PlayerInputAction inputAction;
    int choiceAction = 0;

    Vector3 mousePosition;      // ���콺 ��ġ 

    BoxCollider2D enemyCollider;

    protected override void Awake()
    {
        inputAction = new PlayerInputAction();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))    // ���콺 Ŭ���Ǿ�����
        {
            mousePosition = Input.mousePosition;    // ���콺 �������� ����
            Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);    // ���콺 Ŭ�� ��ġ�� ī�޶� ��ġ�� �°� ����

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);    // collider�� hit�� ����
            if (choiceAction != 0)      // ĳ���� �ൿ ������ 0�� �ƴҽ�
            {
                if (hit.collider != null)   // �ݶ��̴� ������ null�� �ƴҽ�(Ŭ���� �ݶ��̴��� ������)
                {
                    if (choiceAction == 1)  // �ൿ�� 1���̸�
                    {
                        Debug.Log("������ �⺻����");  
                        choiceAction = 0;   // �ʱ�ȭ
                    }
                    else if (choiceAction == 2) // �ൿ�� 2���̸�
                    {
                        Debug.Log("������ 1�� ��ų ����");
                        choiceAction = 0;       // �ʱ�ȭ
                    }
                    else if (choiceAction == 3) // �ൿ�� 3���̸�
                    {
                        Debug.Log("������ 2�� ��ų ����");
                        choiceAction = 0;       // �ʱ�ȭ
                    }
                    else
                    {
                        Debug.Log("���� �����ϼ���");
                    }
                }
            }
        }
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

    private void B1(InputAction.CallbackContext value)     // �Ϲݰ��� ���ý�(Ű���� 1)
    {
        Debug.Log("�⺻���� ����");
        choiceAction = 1;
        // enemy1.getDemage(5, 0);
    }

    private void B2(InputAction.CallbackContext value)     // ��ų���� ���ý�(Ű���� 2)
    {
        Debug.Log("1�� ��ų ����");
        choiceAction = 2;
        // enemy2.getDemage(5, 0);
    }

    private void B3(InputAction.CallbackContext value)     // ��ų���� ���ý�(Ű���� 3)
    {
        Debug.Log("2�� ��ų ����");
        choiceAction = 3;
        // enemy3.getDemage(5, 0);
    }
}
