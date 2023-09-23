using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceEnemyBase : PooledObject
{
    public float moveSpeed = 1.0f;
    public float attackRange = 0.0f;
    public float playerX = -3.5f;
    public float defence = 50.0f;

    float life;
    public float MaxLife = 10.0f;
    public float Life
    {
        get => life;
        set
        {
            if(life != value)
            {
                life = value;
                if (life > 0) Debug.Log(Life);
                else
                {
                    life = 0;
                    Die();
                }
            }
        }
    }

    /// <summary>
    /// 타겟의 겟데미지 어케 불러오냐 시바
    /// </summary>

    public Action<int> onDie;

    protected virtual void Awake()
    {
        life = MaxLife;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OnInintialize();
    }

    protected override void OnDisable()
    {
        onDie = null;
        base.OnDisable();
    }

    private void Update()
    {
        OnMove();
    }

    protected virtual void OnMove()
    {
        if (transform.position.x > playerX)
        {
            transform.Translate(Time.deltaTime * moveSpeed * -transform.right);
        }
        else Attack();
    }

    protected virtual void Attack()
    {
        
        Die();
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }

    public virtual void GetDamage(float damage)
    {
        float getDamge = (100f - defence) * 0.01f * damage;
        Life -= getDamge;
    }

    public virtual void OnInintialize()
    {
    }
}
