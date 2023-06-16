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
        if (Input.GetMouseButtonDown(0))    // ���콺 Ŭ���Ǿ�����
        {
            mousePosition = Input.mousePosition;    // ���콺 �������� ����
            Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);    // ���콺 Ŭ�� ��ġ�� ī�޶� ��ġ�� �°� ����

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);    // collider�� hit�� ����
            if (choiceAction != 0)      // ĳ���� �ൿ ������ 0�� �ƴҽ�
            {
                if (hit.collider != null)   // �ݶ��̴� ������ null�� �ƴҽ�(Ŭ���� �ݶ��̴��� ������)
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
                    choiceAction = 0;   //�ʱ�ȭ
                }
            }
        }
    }

    private void OnEnable()     // inputAction Ȱ��ȭ
    {
        inputAction.Player.Enable();
        inputAction.Player.B1.performed += B1;  // �⺻���� ���� Ȱ��ȭ
        inputAction.Player.B2.performed += B2;  // ��ų ���� Ȱ��ȭ
        inputAction.Player.B3.performed += B3;  // �� ���� Ȱ��ȭ
    }



    private void OnDisable()    // inputAction ��Ȱ��ȭ
    {
        inputAction.Player.B1.performed -= B1;  // �� ���� ��Ȱ��ȭ
        inputAction.Player.B2.performed -= B2;  // ��ų ���� ��Ȱ��ȭ
        inputAction.Player.B3.performed -= B3;  // �⺻���� ���� ��Ȱ��ȭ
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
                Debug.Log($"1�� ��ų�� {skill}��ŭ {target}���� ���ظ� �־���.");
                target.GetDemage(skill, 0);
                
                break;
            case 2:
                MP -= costB;
                if (Random.Range(0, 100) < Agility)
                {
                    skill = (Strike * (StrikeMultiple * BSkillCoefficient) + Intelligent * IntelligentMultiple) * Critical;
                }
                else skill = (Strike * (StrikeMultiple * BSkillCoefficient) + Intelligent * IntelligentMultiple);
                Debug.Log($"2�� ��ų�� {skill}��ŭ {target}���� ���ظ� �־���.");
                target.GetDemage(skill, 0);
                break;
        }
        
    }

    /// <summary>
    /// ĳ���� ���� ����
    /// </summary>
    /// <param name="targetObject">���� ���</param>
    /// <param name="choiceAction">��Ÿ,��ų ����</param>
    void ChoiceAction(CharacterBase targetObject, int choiceAction)
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

    private void B1(InputAction.CallbackContext value)     // �Ϲݰ��� ���ý�(Ű���� 1)
    {
        Debug.Log("�⺻���� ����");
        choiceAction = 1;
    }

    private void B2(InputAction.CallbackContext value)     // ��ų���� ���ý�(Ű���� 2)
    {
        Debug.Log("1�� ��ų ����");
        choiceAction = 2;
    }

    private void B3(InputAction.CallbackContext value)     // ��ų���� ���ý�(Ű���� 3)
    {
        Debug.Log("2�� ��ų ����");
        choiceAction = 3;
    }
}
