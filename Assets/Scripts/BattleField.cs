using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleField : MonoBehaviour
{
    // 변수 만들어서 캐릭터 저장
    // 
    public PoolObjectType warriorObj;
    List<GameObject> objects = new List<GameObject>();
    Mage mage;
    PlayerInputAction inputAction;

    private void Start()
    {
         // int EnemyClass = Random.Range(4, 7);
         // ChoiceClass();
    }

    private void Awake()
    {
        inputAction = new PlayerInputAction();
        mage = FindObjectOfType<Mage>();
    }

    private void OnEnable()
    {
        inputAction.Player.Enable();
        inputAction.Player.Attack.performed += OnAttack;
        inputAction.Player.Skill.performed += OnSkill;
    }
    private void OnDisable()
    {
        inputAction.Player.Skill.performed -= OnSkill;
        inputAction.Player.Attack.performed -= OnAttack;
        inputAction.Player.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log($"{mage}Attack");
        mage.getDemage(5, 0);
    }

    private void OnSkill(InputAction.CallbackContext context)
    {
        Debug.Log($"{mage}Skill");
        mage.getDemage(5, 1);
    }
}
