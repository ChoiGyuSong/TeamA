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
    private void OnEnable()     // inputAction 활성화
    {
        inputAction.Player.Enable();
        inputAction.Player.Attack.performed += OnAttack;    // 기본공격 선택 활성화
        inputAction.Player.Skill.performed += OnSkill;      // 스킬 선택 활성화
        inputAction.Player.ChoiceEnemy.performed += OnChoiceEnemy;  // 적 선택 활성화
    }



    private void OnDisable()    // inputAction 비활성화
    {
        inputAction.Player.ChoiceEnemy.performed -= OnChoiceEnemy;  // 적 선택 비활성화
        inputAction.Player.Skill.performed -= OnSkill;              // 스킬 선택 비활성화
        inputAction.Player.ChoiceEnemy.performed -= OnAttack;         // 기본공격 선택 비활성화
        inputAction.Player.Disable();
    }

    private void OnAttack(InputAction.CallbackContext value)    // 일반공격 선택시(키보드 1)
    {
        Debug.Log("일반 공격이 선택되었습니다.");
    }

    private void OnSkill(InputAction.CallbackContext value)     // 스킬공격 선택시(키보드 2)
    {
        Debug.Log("스킬이 선택되었습니다.");
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
