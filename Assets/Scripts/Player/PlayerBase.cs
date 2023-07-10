using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;

public class PlayerBase : CharacterBase
{
    /// <summary>
    /// 스킬 데미지
    /// </summary>
    float skill = 0.0f;

    /// <summary>
    /// 턴매니저 불러옴
    /// </summary>
    TurnManager turnManager;

    public float ASkillCoefficient = 5.0f;  // 1번스킬(A스킬) 계수
    public float BSkillCoefficient = 1.5f;  // 2번스킬(B스킬) 계수

    public int costA = 20;  // 1번스킬 마나 소모량
    public int costB = 30;  // 2번스킬 마나 소모량

    public int damagetype = 0;  // 데미지 소모량

    public void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
    }
    
    /// <summary>
    /// TurnManager에서 마우스 클릭이 된후 실행되는 함수
    /// </summary>
    /// <param name="target"> 선택된 적 </param>
    /// <param name="attackType"> 데미지 형식 </param>
    public override void Attack(CharacterBase target, int attackType)
    {
        switch (attackType)
        {
            case 0:
                if (target.IsDead == false)     // 타겟이 살아있다면
                {
                    base.Attack(target, 0);     // 기본공격 실행
                    turnManager.choiceAction = 0;  // 행동 초기화
                }
                else  // 적이 죽었다면
                {
                    Debug.Log("해당적은 이미 죽었습니다");
                }
                break;
            case 1:
                if (target.IsDead == false)     // 타겟이 살아있다면
                {
                    MP -= costA;
                    if (Random.Range(0, 100) < Agility)
                    {
                        skill = (Strike * (StrikeMultiple * ASkillCoefficient) + Intelligent * IntelligentMultiple) * Critical;
                        turnManager.choiceAction = 0;   //초기화
                    }
                    else skill = (Strike * (StrikeMultiple * ASkillCoefficient) + Intelligent * IntelligentMultiple);
                    turnManager.choiceAction = 0;   //초기화
                    Debug.Log($"1번 스킬로 {skill}만큼 {target}에게 피해를 주었다.");
                    target.GetDemage(skill, 0);
                }
                else  // 적이 죽었다면
                {
                    Debug.Log("해당적은 이미 죽었습니다");
                }

                break;
            case 2:
                if (target.IsDead == false)     // 타겟이 살아있다면
                {
                    MP -= costB;
                    if (Random.Range(0, 100) < Agility)
                    {
                        skill = (Strike * (StrikeMultiple * BSkillCoefficient) + Intelligent * IntelligentMultiple) * Critical;
                    }
                    else skill = (Strike * (StrikeMultiple * BSkillCoefficient) + Intelligent * IntelligentMultiple);
                    Debug.Log($"2번 스킬로 {skill}만큼 {target}에게 피해를 주었다.");
                    target.GetDemage(skill, 0);
                }
                else  // 적이 죽었다면
                {
                    Debug.Log("해당적은 이미 죽었습니다");
                }
                break;
        }
    }
    

    /// <summary>
    /// 캐릭터 선택 이후
    /// </summary>
    /// <param name="targetObject">공격 대상</param>
    /// <param name="choiceAction">평타,스킬 선택</param>
    public void ChoiceAction(CharacterBase targetObject, int choiceAction)
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

    public override void PlayerAction()
    {
        base.PlayerAction();
    }
    protected override void Die()
    {
        base.Die();
    }
}
