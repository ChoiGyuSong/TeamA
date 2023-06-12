using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mage : CharacterBase
{
    float meteor;                   // 스킬 메테오 
    PlayerInputAction inputAction;  // inputAction 추가
    int choiceAttack;               // 행동 선택
    public GameObject attackEffect; // 공격 이펙트
    public GameObject skillEffect;  // 스킬 이펙트
    WaitForSeconds attackWait;      // 어택 이펙트 기다림
    WaitForSeconds skillWait;       // 스킬 이펙트 기다림

    private void Skill()            // 스킬(메테오) 함수
    {
        if (Random.Range(0, 100) < Agility)     // 크리티컬 확률
        {
            meteor = (Intelligent * IntelligentMultiple) * Critical;    // 크리티컬 식
        }
        else meteor = (Intelligent * IntelligentMultiple);              // 일반 식
        StartCoroutine(SkillEffect());          // 스킬 이펙트 코루틴
    }

    protected override void Attack()        // 기본공격 함수
    {
        base.Attack();
        StartCoroutine(AttackEffect());     // 기본공격 이펙트 코루틴
    }

    protected override void Awake()
    {
        base.Awake();
        inputAction = new PlayerInputAction();              // inputAction 
        attackEffect = transform.GetChild(1).gameObject;    // 기본공격 이펙트 불러오기
        skillEffect = transform.GetChild(2).gameObject;     // 스킬 이펙트 불러오기
        attackWait = new WaitForSeconds(1);                 // 기본공격 이펙트 출력시간
        skillWait = new WaitForSeconds(1);                  // 스킬공격 이펙트 출력시간

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
        choiceAttack = 1;       // 행동 선택을 1로 변경
        Debug.Log("일반 공격이 선택되었습니다.");
    }

    private void OnSkill(InputAction.CallbackContext value)     // 스킬공격 선택시(키보드 2)
    {
        choiceAttack = 2;       // 행동 선택을 2로 변경
        Debug.Log("스킬이 선택되었습니다.");
    }

    private void OnChoiceEnemy(InputAction.CallbackContext value)   // 적을 선택시(키보드 a)
    {
        if ( choiceAttack == 1)         // 적 선택시 행동 선택이 1이었다면
        {
            Attack();                   // 기본 공격 실행
            Debug.Log("적 공격");
            choiceAttack = 0;           // 행동 선택 초기화
        }
        else if(choiceAttack == 2)      // 적 선택히 행동 선택이 2이었다면
        {
            Skill();                    // 스킬 공격 실행
            Debug.Log("적한테 스킬");
            choiceAttack = 0;           // 행동 선택 초기화
        }
        else                            // 행동 선택이 제대로 선택 안되었을때
        {
            Debug.Log("행동을 선택하세요");
        }
    }

    IEnumerator AttackEffect()          // 기본공격 이펙트 IEnumerator
    {
        attackEffect.SetActive(true);   // 활성화
        yield return attackWait;        // attackWait 만큼 대기
        attackEffect.SetActive(false);  // 비활성화
    }

    IEnumerator SkillEffect()           // 스킬공격 이펙트 IEnumerator
    {
        skillEffect.SetActive(true);    // 활성화
        yield return skillWait;         // skillWait 만큼 대기
        skillEffect.SetActive(false);   // 비활성화
    }

}