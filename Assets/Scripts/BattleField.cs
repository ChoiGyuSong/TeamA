using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleField : MonoBehaviour
{
    // Player1 player1;
    // Player2 player2;
    // Player3 player3;
    // Enemy1 enemy1;
    // Enemy2 enemy2;
    // Enemy3 enemy3;
    GameObject enemyBase;
    int turn = 0;

    private void Awake()
    {
        // player1 = GetComponent<Player1>();
        // player2 = GetComponent<Player2>();
        // player3 = GetComponent<Player3>();
        /*enemy1 = GetComponent<Enemy1>();
        enemy2 = GetComponent<Enemy2>();
        enemy3 = GetComponent<Enemy3>();*/
        enemyBase = GameObject.FindObjectOfType<EnemyBase>().gameObject;
    }
    private void Start()
    {
        while(true)
        {
            turn++;
            Debug.Log($"{turn}¹øÂ° ÅÏ");
            /*enemy1.EnemyAttack();
            enemy2.EnemyAttack();
            enemy3.EnemyAttack();*/
        }
    }
}
