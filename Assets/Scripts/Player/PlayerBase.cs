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

    Vector3 mousePosition;      // 마우스 위치 

    float skill = 0.0f;

    public float ASkillCoefficient = 5.0f;
    public float BSkillCoefficient = 1.5f;

    private CharacterBase targetObject;
    GameObject clickObject = null;

    public int costA = 20;
    public int costB = 30;

    protected override void Awake()
    {
        base.Awake();
        inputAction = new PlayerInputAction();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))    // 마우스 클릭되었을때
        {
            mousePosition = Input.mousePosition;    // 마우스 포지션을 저장
            Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);    // 마우스 클릭 위치를 카메라 위치에 맞게 변경

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);    // collider를 hit에 리턴
            if (choiceAction != 0)      // 캐릭터 행동 선택이 0이 아닐시
            {
                if (hit.collider != null)   // 콜라이더 선택이 null이 아닐시(클릭을 콜라이더에 했을때)
                {
                    clickObject = hit.collider.gameObject;
                    targetObject = clickObject.GetComponent<CharacterBase>();
                    if (hit.collider.gameObject.name == "Enemy1")
                    {
                        ChoiceAction(targetObject, choiceAction);
                    }
                    else if (hit.collider.gameObject.name == "Enemy2")
                    {
                        ChoiceAction(targetObject, choiceAction);
                    }
                    choiceAction = 0;   //초기화
                }
            }
        }
    }

    private void OnEnable()     // inputAction 활성화
    {
        inputAction.Player.Enable();
        inputAction.Player.B1.performed += B1;  // 기본공격 선택 활성화
        inputAction.Player.B2.performed += B2;  // 스킬 선택 활성화
        inputAction.Player.B3.performed += B3;  // 적 선택 활성화
    }



    private void OnDisable()    // inputAction 비활성화
    {
        inputAction.Player.B1.performed -= B1;  // 적 선택 비활성화
        inputAction.Player.B2.performed -= B2;  // 스킬 선택 비활성화
        inputAction.Player.B3.performed -= B3;  // 기본공격 선택 비활성화
        inputAction.Player.Disable();
    }

    protected override void Attack(CharacterBase target, int attackType)
    {
        switch (attackType)
        {
            case 0:
                base.Attack(target,0);
                break;
            case 1:
                MP -= costA;
                if (Random.Range(0, 100) < Agility)
                {
                    skill = (Strike * (StrikeMultiple * ASkillCoefficient) + Intelligent * IntelligentMultiple) * Critical;
                }
                else skill = (Strike * (StrikeMultiple * ASkillCoefficient) + Intelligent * IntelligentMultiple);
                Debug.Log($"1번 스킬로 {skill}만큼 {target}에게 피해를 주었다.");
                target.GetDemage(skill, 0);
                
                break;
            case 2:
                MP -= costB;
                if (Random.Range(0, 100) < Agility)
                {
                    skill = (Strike * (StrikeMultiple * BSkillCoefficient) + Intelligent * IntelligentMultiple) * Critical;
                }
                else skill = (Strike * (StrikeMultiple * BSkillCoefficient) + Intelligent * IntelligentMultiple);
                Debug.Log($"2번 스킬로 {skill}만큼 {target}에게 피해를 주었다.");
                target.GetDemage(skill, 0);
                break;
        }
        
    }

    /// <summary>
    /// 캐릭터 선택 이후
    /// </summary>
    /// <param name="targetObject">공격 대상</param>
    /// <param name="choiceAction">평타,스킬 선택</param>
    void ChoiceAction(CharacterBase targetObject, int choiceAction)
    {
        if (choiceAction == 1)  // 행동이 1번이면
        {
            Attack(targetObject, 0);
        }
        else if (choiceAction == 2) // 행동이 2번이면
        {
            if (MP >= costA)
            {
                Attack(targetObject, 1);
            }
            else
            {
                LackMP();
            }
        }
        else if (choiceAction == 3) // 행동이 3번이면
        {
            if (MP >= costB)
            {
                Attack(targetObject, 2);
            }
            else
            {
                LackMP();
            }
        }
    }

    /// <summary>
    /// 마나 부족시 불러올 함수
    /// </summary>
    void LackMP()
    {
        Debug.Log("MP가 부족합니다 행동을 재선택하세요");
    }

    private void B1(InputAction.CallbackContext value)     // 일반공격 선택시(키보드 1)
    {
        Debug.Log("기본공격 선택");
        choiceAction = 1;
    }

    private void B2(InputAction.CallbackContext value)     // 스킬공격 선택시(키보드 2)
    {
        Debug.Log("1번 스킬 선택");
        choiceAction = 2;
    }

    private void B3(InputAction.CallbackContext value)     // 스킬공격 선택시(키보드 3)
    {
        Debug.Log("2번 스킬 선택");
        choiceAction = 3;
    }
}
