using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mage : CharacterBase
{
    float meteor;                   // ��ų ���׿� 
    PlayerInputAction inputAction;  // inputAction �߰�
    int choiceAttack;               // �ൿ ����
    public GameObject attackEffect; // ���� ����Ʈ
    public GameObject skillEffect;  // ��ų ����Ʈ
    WaitForSeconds attackWait;      // ���� ����Ʈ ��ٸ�
    WaitForSeconds skillWait;       // ��ų ����Ʈ ��ٸ�

    private void Skill()            // ��ų(���׿�) �Լ�
    {
        if (Random.Range(0, 100) < Agility)     // ũ��Ƽ�� Ȯ��
        {
            meteor = (Intelligent * IntelligentMultiple) * Critical;    // ũ��Ƽ�� ��
        }
        else meteor = (Intelligent * IntelligentMultiple);              // �Ϲ� ��
        StartCoroutine(SkillEffect());          // ��ų ����Ʈ �ڷ�ƾ
    }

    protected override void Attack()        // �⺻���� �Լ�
    {
        base.Attack();
        StartCoroutine(AttackEffect());     // �⺻���� ����Ʈ �ڷ�ƾ
    }

    protected override void Awake()
    {
        base.Awake();
        inputAction = new PlayerInputAction();              // inputAction 
        attackEffect = transform.GetChild(1).gameObject;    // �⺻���� ����Ʈ �ҷ�����
        skillEffect = transform.GetChild(2).gameObject;     // ��ų ����Ʈ �ҷ�����
        attackWait = new WaitForSeconds(1);                 // �⺻���� ����Ʈ ��½ð�
        skillWait = new WaitForSeconds(1);                  // ��ų���� ����Ʈ ��½ð�

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
        choiceAttack = 1;       // �ൿ ������ 1�� ����
        Debug.Log("�Ϲ� ������ ���õǾ����ϴ�.");
    }

    private void OnSkill(InputAction.CallbackContext value)     // ��ų���� ���ý�(Ű���� 2)
    {
        choiceAttack = 2;       // �ൿ ������ 2�� ����
        Debug.Log("��ų�� ���õǾ����ϴ�.");
    }

    private void OnChoiceEnemy(InputAction.CallbackContext value)   // ���� ���ý�(Ű���� a)
    {
        if ( choiceAttack == 1)         // �� ���ý� �ൿ ������ 1�̾��ٸ�
        {
            Attack();                   // �⺻ ���� ����
            Debug.Log("�� ����");
            choiceAttack = 0;           // �ൿ ���� �ʱ�ȭ
        }
        else if(choiceAttack == 2)      // �� ������ �ൿ ������ 2�̾��ٸ�
        {
            Skill();                    // ��ų ���� ����
            Debug.Log("������ ��ų");
            choiceAttack = 0;           // �ൿ ���� �ʱ�ȭ
        }
        else                            // �ൿ ������ ����� ���� �ȵǾ�����
        {
            Debug.Log("�ൿ�� �����ϼ���");
        }
    }

    IEnumerator AttackEffect()          // �⺻���� ����Ʈ IEnumerator
    {
        attackEffect.SetActive(true);   // Ȱ��ȭ
        yield return attackWait;        // attackWait ��ŭ ���
        attackEffect.SetActive(false);  // ��Ȱ��ȭ
    }

    IEnumerator SkillEffect()           // ��ų���� ����Ʈ IEnumerator
    {
        skillEffect.SetActive(true);    // Ȱ��ȭ
        yield return skillWait;         // skillWait ��ŭ ���
        skillEffect.SetActive(false);   // ��Ȱ��ȭ
    }

}