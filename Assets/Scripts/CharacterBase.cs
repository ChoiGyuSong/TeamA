using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    //최종 데미지
    float Damage;
    //기본 스탯
    public float Strike = 1f;
    public float Intelligent = 1f;
    public float Agility = 1f;
    public float Defence = 1f;
    public float Anti = 1f;
    float hp;
    float mp;
    //최대 마나체력
    public float MaxHp = 100f;
    public float MaxMp = 100f;
    //스탯 배수 설정
    public float StrikeMultiple;
    public float IntelligentMultiple;
    public float DefenceMultiple;
    public float AntiMultiple;
    //치명타 배수
    public float Critical = 2f;
    //임시 데미지 이펙트 시간
    public float AnimationTime = 1f;
    //마나 체력 받기
    public float HP
    {
        get => hp;
        set
        {
            if(hp != value)
            {
                if (hp > 0)
                {
                    hp = value;
                    Debug.Log(HP);
                }
                else
                {
                    hp = 0;
                    Die();
                }
            }
        }
    }
    protected virtual float MP
    {
        get => mp;
        set
        {
            if(mp != value)
            {
                mp = value;
                Debug.Log($"현재 마나는 {mp}");
            }
        }
    }
    
    /// <summary>
    /// 캐릭터 스탯
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
    /// 주는 데미지
    /// </summary>
    /// <returns>주는 데미지 리턴</returns>
    protected virtual void Attack(CharacterBase target, int attackType)
    {
        if (Random.Range(0, 100) < Agility)
        {
            Damage = (Strike * StrikeMultiple + Intelligent * IntelligentMultiple) * Critical;
        }
        else Damage = (Strike * StrikeMultiple + Intelligent * IntelligentMultiple);
        Debug.Log($"기본공격으로 {Damage}만큼 {target}에게 피해를 줌");
        target.GetDemage(Damage, 0);       // 적에게 데미지 줌
    }

    /// <summary>
    /// 받는 데미지
    /// </summary>
    /// <param name="getDamage">데미지</param>s
    /// <param name="DamageSort">받는 데미지 종류</param>
    public void GetDemage(float getDamage, int DamageSort)
    {
        if(DamageSort == 0) HP -= getDamage / Defence;
        else if(DamageSort == 1) HP -= getDamage / Anti;
        //StartCoroutine(hit());
    }

    /// <summary>
    /// 으앙주금
    /// </summary>
    protected virtual void Die()
    {
        Debug.Log("Die");
    }
}
