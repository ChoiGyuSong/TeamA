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
    GameManager gameManager;
    public int damagetype = 0;
    public int DeadCount = 0;
    public int enemyDie = 0;
    public bool enemyLose = false;


    protected override void Awake()
    {
        base.Awake();
        player1 = FindObjectOfType<Player1>();
        player2 = FindObjectOfType<Player2>();
        player3 = FindObjectOfType<Player3>();
        gameManager = FindObjectOfType<GameManager>();
    }

 
    private void Update()
    {
    }

    public virtual void EnemyAttack()
    {
        if (attack == true && IsDead == false)
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
        Turn += speed;
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("적이 사망하였습니다.");
        gameManager.EnemyDied();
    }
}