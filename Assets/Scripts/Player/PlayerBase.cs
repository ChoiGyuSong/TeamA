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

    private CharacterBase targetObject;
    GameObject clickObject = null;

    protected override void Awake()
    {
        inputAction = new PlayerInputAction();
    }

    private void Start()
    {

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
                        if (choiceAction == 1)  // 행동이 1번이면
                        {
                            Debug.Log("1번 적에게 기본공격");
                            Attack(targetObject);
                            choiceAction = 0;   // 초기화
                        }
                        else if (choiceAction == 2) // 행동이 2번이면
                        {
                            Debug.Log("1번 적에게 1번 스킬 공격");
                            SkillA(targetObject);
                            choiceAction = 0;       // 초기화
                        }
                        else if (choiceAction == 3) // 행동이 3번이면
                        {
                            Debug.Log("1번 적에게 2번 스킬 공격");
                            SkillB(targetObject);
                            choiceAction = 0;       // 초기화
                        }

                    }
                    if (hit.collider.gameObject.name == "Enemy2")
                    {
                        if (choiceAction == 1)  // 행동이 1번이면
                        {
                            Debug.Log("2번 적에게 기본공격");
                            Attack(targetObject);
                            choiceAction = 0;   // 초기화
                        }
                        else if (choiceAction == 2) // 행동이 2번이면
                        {
                            Debug.Log("2번 적에게 1번 스킬 공격");
                            SkillA(targetObject);
                            choiceAction = 0;       // 초기화
                        }
                        else if (choiceAction == 3) // 행동이 3번이면
                        {
                            Debug.Log("2번 적에게 2번 스킬 공격");
                            SkillB(targetObject);
                            choiceAction = 0;       // 초기화
                        }
                    }
                }
            }
        }
    }

    private void OnEnable()     // inputAction 활성화
    {
        inputAction.Player.Enable();
        inputAction.Player.B1.performed += B1;    // 기본공격 선택 활성화
        inputAction.Player.B2.performed += B2;      // 스킬 선택 활성화
        inputAction.Player.B3.performed += B3;  // 적 선택 활성화
    }



    private void OnDisable()    // inputAction 비활성화
    {
        inputAction.Player.B1.performed -= B1;  // 적 선택 비활성화
        inputAction.Player.B2.performed -= B2;              // 스킬 선택 비활성화
        inputAction.Player.B3.performed -= B3;         // 기본공격 선택 비활성화
        inputAction.Player.Disable();
    }

    protected override void Attack(CharacterBase target)
    {
        base.Attack(target);
    }

    protected virtual void SkillA(CharacterBase target)
    {
        if (Random.Range(0, 100) < Agility)
        {
            skill = (Strike * StrikeMultiple) * Critical;
        }
        else skill = (Strike * StrikeMultiple);
        getDemage(skill, 0);
    }

    protected virtual void SkillB(CharacterBase target)
    {
        if (Random.Range(0, 100) < Agility)
        {
            skill = (Strike * StrikeMultiple) * Critical;
        }
        else skill = (Strike * StrikeMultiple);
        getDemage(skill, 0);
    }

    private void B1(InputAction.CallbackContext value)     // 일반공격 선택시(키보드 1)
    {
        Debug.Log("기본공격 선택");
        choiceAction = 1;
        // enemy1.getDemage(5, 0);
    }

    private void B2(InputAction.CallbackContext value)     // 스킬공격 선택시(키보드 2)
    {
        Debug.Log("1번 스킬 선택");
        choiceAction = 2;
        // enemy2.getDemage(5, 0);
    }

    private void B3(InputAction.CallbackContext value)     // 스킬공격 선택시(키보드 3)
    {
        Debug.Log("2번 스킬 선택");
        choiceAction = 3;
        // enemy3.getDemage(5, 0);
    }
}
