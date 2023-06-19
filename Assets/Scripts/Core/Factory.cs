using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PoolObjectType
{
    Player1 =0,
    Player2,
    Player3,
    Enemy1,
    Enemy2,
    Enemy3,
}

public class Factory : Singleton<Factory>
{
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
  
  
  

    public GameObject GetObject(PoolObjectType type)
    {
        GameObject result = type switch
        {
            PoolObjectType.Player1 => Instantiate(player1),
            PoolObjectType.Player2 => Instantiate(player2),
            PoolObjectType.Player3 => Instantiate(player3),
            PoolObjectType.Enemy1 => Instantiate(enemy1),
            PoolObjectType.Enemy2 => Instantiate(enemy2),
            PoolObjectType.Enemy3 => Instantiate(enemy3),
            _ => new GameObject(),
        };
        return result;
    }
}
