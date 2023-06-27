using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BattleField : GameManager
{
    CharacterBase[] playerBase;
    CharacterBase[] enemyBase;
    public int PdeadCount = 0;
    public int EdeadCount = 0;


    private void Awake()
    {
        playerBase = FindObjectsOfType<PlayerBase>();
        enemyBase = FindObjectsOfType<EnemyBase>();
    }    
}
