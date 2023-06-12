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
    }

    private void Start()
    {

    }
    private void OnEnable()     // inputAction Ȱ��ȭ
    {
        inputAction.Player.Enable();
        inputAction.Player.Attack.performed += OnAttack;    // �⺻���� ���� Ȱ��ȭ
        inputAction.Player.Skill.performed += OnSkill;      // ��ų ���� Ȱ��ȭ
        inputAction.Player.ChoiceEnemy.performed += OnChoiceEnemy;  // �� ���� Ȱ��ȭ
    }



    private void OnDisable()    // inputAction ��Ȱ��ȭ
    {
        inputAction.Player.ChoiceEnemy.performed -= OnChoiceEnemy;  // �� ���� ��Ȱ��ȭ
        inputAction.Player.Skill.performed -= OnSkill;              // ��ų ���� ��Ȱ��ȭ
        inputAction.Player.ChoiceEnemy.performed -= OnAttack;         // �⺻���� ���� ��Ȱ��ȭ
        inputAction.Player.Disable();
    }

    private void OnAttack(InputAction.CallbackContext value)    // �Ϲݰ��� ���ý�(Ű���� 1)
    {
        Debug.Log("�Ϲ� ������ ���õǾ����ϴ�.");
    }

    private void OnSkill(InputAction.CallbackContext value)     // ��ų���� ���ý�(Ű���� 2)
    {
        Debug.Log("��ų�� ���õǾ����ϴ�.");
    }

    private void OnChoiceEnemy(InputAction.CallbackContext value)
    {
        switch (value)
        {
            case 1: enemy = enemy1.GameObject();
                break;
            case 2: enemy = enemy2.GameObject();
                break;
            case 3: enemy = enemy3.GameObject();
                break;
        }
    }
}
