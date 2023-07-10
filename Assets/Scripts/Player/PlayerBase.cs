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
    /// ��ų ������
    /// </summary>
    float skill = 0.0f;

    /// <summary>
    /// �ϸŴ��� �ҷ���
    /// </summary>
    TurnManager turnManager;

    public float ASkillCoefficient = 5.0f;  // 1����ų(A��ų) ���
    public float BSkillCoefficient = 1.5f;  // 2����ų(B��ų) ���

    public int costA = 20;  // 1����ų ���� �Ҹ�
    public int costB = 30;  // 2����ų ���� �Ҹ�

    public int damagetype = 0;  // ������ �Ҹ�

    public void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
    }
    
    /// <summary>
    /// TurnManager���� ���콺 Ŭ���� ���� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="target"> ���õ� �� </param>
    /// <param name="attackType"> ������ ���� </param>
    public override void Attack(CharacterBase target, int attackType)
    {
        switch (attackType)
        {
            case 0:
                if (target.IsDead == false)     // Ÿ���� ����ִٸ�
                {
                    base.Attack(target, 0);     // �⺻���� ����
                    turnManager.choiceAction = 0;  // �ൿ �ʱ�ȭ
                }
                else  // ���� �׾��ٸ�
                {
                    Debug.Log("�ش����� �̹� �׾����ϴ�");
                }
                break;
            case 1:
                if (target.IsDead == false)     // Ÿ���� ����ִٸ�
                {
                    MP -= costA;
                    if (Random.Range(0, 100) < Agility)
                    {
                        skill = (Strike * (StrikeMultiple * ASkillCoefficient) + Intelligent * IntelligentMultiple) * Critical;
                        turnManager.choiceAction = 0;   //�ʱ�ȭ
                    }
                    else skill = (Strike * (StrikeMultiple * ASkillCoefficient) + Intelligent * IntelligentMultiple);
                    turnManager.choiceAction = 0;   //�ʱ�ȭ
                    Debug.Log($"1�� ��ų�� {skill}��ŭ {target}���� ���ظ� �־���.");
                    target.GetDemage(skill, 0);
                }
                else  // ���� �׾��ٸ�
                {
                    Debug.Log("�ش����� �̹� �׾����ϴ�");
                }

                break;
            case 2:
                if (target.IsDead == false)     // Ÿ���� ����ִٸ�
                {
                    MP -= costB;
                    if (Random.Range(0, 100) < Agility)
                    {
                        skill = (Strike * (StrikeMultiple * BSkillCoefficient) + Intelligent * IntelligentMultiple) * Critical;
                    }
                    else skill = (Strike * (StrikeMultiple * BSkillCoefficient) + Intelligent * IntelligentMultiple);
                    Debug.Log($"2�� ��ų�� {skill}��ŭ {target}���� ���ظ� �־���.");
                    target.GetDemage(skill, 0);
                }
                else  // ���� �׾��ٸ�
                {
                    Debug.Log("�ش����� �̹� �׾����ϴ�");
                }
                break;
        }
    }
    

    /// <summary>
    /// ĳ���� ���� ����
    /// </summary>
    /// <param name="targetObject">���� ���</param>
    /// <param name="choiceAction">��Ÿ,��ų ����</param>
    public void ChoiceAction(CharacterBase targetObject, int choiceAction)
    {
        if (choiceAction == 1)  // �ൿ�� 1���̸�
        {
            Attack(targetObject, 0);
        }
        else if (choiceAction == 2) // �ൿ�� 2���̸�
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
        else if (choiceAction == 3) // �ൿ�� 3���̸�
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
    /// ���� ������ �ҷ��� �Լ�
    /// </summary>
    void LackMP()
    {
        Debug.Log("MP�� �����մϴ� �ൿ�� �缱���ϼ���");
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
