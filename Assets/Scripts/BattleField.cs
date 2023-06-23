using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BattleField : MonoBehaviour
{
    GameObject player1;
    GameObject player2;
    GameObject player3;
    GameObject enemy1;
    GameObject enemy2;
    GameObject enemy3;
    int turn = 0;
    public int enemyDie = 0;

    private void Awake()
    {
        player1 = GameObject.FindObjectOfType<Player1>().gameObject;
        player2 = GameObject.FindObjectOfType<Player2>().gameObject;
        player3 = GameObject.FindObjectOfType<Player3>().gameObject;
        enemy1 = GameObject.FindObjectOfType<Enemy1>().gameObject;
        enemy2 = GameObject.FindObjectOfType<Enemy2>().gameObject;
        enemy3 = GameObject.FindObjectOfType<Enemy3>().gameObject;
    }
    private void Start()
    {
        while(true)
        {
            turn++;
            Debug.Log($"{turn}¹øÂ° ÅÏ");
            player1.GetComponent<Player1>().PlayerAttack();
            player2.GetComponent<Player2>().PlayerAttack();
            player3.GetComponent<Player3>().PlayerAttack();
            enemy1.GetComponent<Enemy1>().EnemyAttack();
            enemy2.GetComponent<Enemy2>().EnemyAttack();
            enemy3.GetComponent<Enemy3>().EnemyAttack();
        }
    }
}
