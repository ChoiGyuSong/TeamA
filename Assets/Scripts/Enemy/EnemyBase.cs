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
    public int damagetype = 0;

    protected override void Awake()
    {
        base.Awake();
        player1 = FindObjectOfType<Player1>();
        player2 = FindObjectOfType<Player2>();
        player3 = FindObjectOfType<Player3>();
    }

    public virtual void EnemyAttack()
    {
        if (attack)
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
}