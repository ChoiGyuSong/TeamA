using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    int randAttack = Random.Range(0, 3);

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Attack(CharacterBase target, int attackType)
    {
        base.Attack(target, attackType);
    }

    IEnumerator EnemyAttack()
    {
        if(randAttack == 0)
        {

            yield break;
        }
    }
}
