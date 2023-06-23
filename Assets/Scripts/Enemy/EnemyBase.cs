using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBase : CharacterBase
{
    Player1 player1;
    Player2 player2;
    Player3 player3;
    BattleField battleField;
    public int damagetype = 0;
    public int DeadCount = 0;

    protected override void Awake()
    {
        base.Awake();
        player1 = FindObjectOfType<Player1>();
        player2 = FindObjectOfType<Player2>();
        player3 = FindObjectOfType<Player3>();
        battleField = GetComponent<BattleField>();
    }

    public virtual void EnemyAttack()
    {
        if (attack && !IsDead)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    Attack(player1, damagetype);
                    break;
                case 1:
                    Attack(player2, damagetype);
                    break;
                case 2:
                    Attack(player3, damagetype);
                    break;
            }
            attack = false;
        }
        Debug.Log("ео а╬╥А");
        Turn += speed;
    }

    protected override void Die()
    {
        battleField.enemyDie++;
        base.Die();
    }
}