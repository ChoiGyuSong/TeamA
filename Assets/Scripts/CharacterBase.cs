using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public bool IsDead = false;
    //���� ������
    float Damage;
    //�⺻ ����
    public float Strike = 1f;
    public float Intelligent = 1f;
    public float Agility = 1f;
    public float Defence = 1f;
    public float Anti = 1f;
    float hp;
    float mp;
    public float speed = 60f;
    //�ִ� ����ü��
    public float MaxHp = 100f;
    public float MaxMp = 100f;
    //���� ��� ����
    public float StrikeMultiple = 1.0f;
    public float IntelligentMultiple = 1.0f;
    public float DefenceMultiple = 1.0f;
    public float AntiMultiple = 1.0f;
    //ġ��Ÿ ���
    public float Critical = 2f;
    //�ӽ� ������ ����Ʈ �ð�
    public float AnimationTime = 1f;

    public float turnSpeed; // �� �ӵ�
    public bool isTurnComplete ; // �� �Ϸ� ����
    public bool isAlive = true; // ���� ����


    public int a;
    public float HP
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                if (hp > 0)
                {
                    Debug.Log($"����ü�� {HP}");
                }
                else
                {
                    hp = 0;
                    Die();
                }
            }
        }
    }
    public virtual float MP
    {
        get => mp;
        set
        {
            if (mp != value)
            {
                mp = value;
                Debug.Log($"���� ������ {mp}");
            }
        }
    }

    /// <summary>
    /// ĳ���� ����
    /// </summary>
    protected virtual void Awake()
    {
        // enemy = find
        //CharacterStats();
        //Hit = transform.GetChild(0).gameObject;
        //Hit.SetActive(false);
        hp = MaxHp;
        mp = MaxMp;
    }

    /// <summary>
    /// �ִ� ������
    /// </summary>
    /// <returns>�ִ� ������ ����</returns>
    public virtual void Attack(CharacterBase target, int attackType)
    {
        if (Random.Range(0, 100) < Agility)
        {
            Damage = (Strike * StrikeMultiple + Intelligent * IntelligentMultiple) * Critical;
        }
        else Damage = (Strike * StrikeMultiple + Intelligent * IntelligentMultiple);
        Debug.Log($"�⺻�������� {Damage}��ŭ {target}���� ���ظ� ��");
        target.GetDemage(Damage, 0);       // ������ ������ ��
    }

    /// <summary>
    /// �޴� ������
    /// </summary>
    /// <param name="getDamage">������</param>s
    /// <param name="DamageSort">�޴� ������ ����</param>
    public void GetDemage(float getDamage, int DamageSort)
    {
        if (DamageSort == 0) HP -= getDamage / Defence;
        else if (DamageSort == 1) HP -= getDamage / Anti;
        //StartCoroutine(hit());
    }

    /// <summary>
    /// �����ֱ�
    /// </summary>
    protected virtual void Die()
    {
        IsDead = true;
        isAlive = false;
    }

    public void StartTurn()
    {

        // �� �ϷḦ �ʱ�ȭ
        isTurnComplete = false;
    }

    public virtual void PlayerAction()
    {
        // PlayerBase���� �÷��̾ �ൿ
    }

    public virtual void EnemyAction()
    {
        // EnemyBase���� ���� �ൿ
    }

    public virtual void EndTurn()
    {
        isTurnComplete = true;
    }

}
