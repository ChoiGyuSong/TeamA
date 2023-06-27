using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public bool playerLose = false;
    GameManager GM;


    int enemy = 0;

    protected override void Awake()
    {
        base.Awake();
        inputAction = new PlayerInputAction();
    }
    protected override void Start()
    {
    }

    private void Update()
    {

    }


    public void PlayerAttack()
    {
        if (IsDead == false)
            if (attack)
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
                        else if (hit.collider.gameObject.name == "Enemy3")
                        {
                            ChoiceAction(targetObject, choiceAction);
                        }
                        else if (hit.collider.gameObject.name == "Boss")
                        {
                            ChoiceAction(targetObject, choiceAction);
                        }
                    }
                }
                attack = false;
            }
        Turn += speed;

    }

    private void OnEnable()     // inputAction Ȱ��ȭ
    {
        inputAction.Player.Enable();
        inputAction.Player.B1.performed += B1;  // �⺻���� ���� Ȱ��ȭ
        inputAction.Player.B2.performed += B2;  // ��ų ���� Ȱ��ȭ
        inputAction.Player.B3.performed += B3;  // �� ���� Ȱ��ȭ
        inputAction.Player.Mouse.performed += MouseClick;
    }



    private void OnDisable()    // inputAction ��Ȱ��ȭ
    {
        inputAction.Player.Mouse.performed -= MouseClick;
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
                if (target.IsDead == false)
                {
                    base.Attack(target, 0);
                    choiceAction = 0;   //�ʱ�ȭ
                }
                else
                {
                    Debug.Log("�ش����� �̹� �׾����ϴ�");
                }
                break;
            case 1:
                MP -= costA;
                if (Random.Range(0, 100) < Agility)
                {
                    skill = (Strike * (StrikeMultiple * ASkillCoefficient) + Intelligent * IntelligentMultiple) * Critical;
                    choiceAction = 0;   //�ʱ�ȭ
                }
                else skill = (Strike * (StrikeMultiple * ASkillCoefficient) + Intelligent * IntelligentMultiple);
                choiceAction = 0;   //�ʱ�ȭ
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

    private void MouseClick(InputAction.CallbackContext value)
    {
        PlayerAttack();
    }

    protected override void Die()
    {
        base.Die();
        // GM.PlayerDied();
    }
}