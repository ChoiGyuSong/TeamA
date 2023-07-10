using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// PlayerBase 배열
    /// </summary>
    PlayerBase[] playerBase;

    /// <summary>
    /// EnemyBase 배열
    /// </summary>
    EnemyBase[] enemyBase;

    /// <summary>
    /// 플레이어 데스카운트(죽을때마다 씩증가)
    /// </summary>
    private int PdeathCount = 0;

    /// <summary>
    /// 적 데스카운트(죽을때마다 증가)
    /// </summary>
    private int EdeathCount = 0;

    private void Awake()
    {
        playerBase = FindObjectsOfType<PlayerBase>();
        enemyBase = FindObjectsOfType<EnemyBase>();
    }

    public void PlayerDied()
    {
        PdeathCount++;  // 플레이어 데스 카운트 1씩 증가
        if (PdeathCount >= playerBase.Length)   // 플레이어의 데스카운트가 플레이어 오브젝트 갯수보다 크거나 같아지면
        {
            Debug.Log("플레이어 패배");
            Time.timeScale = 0; // 게임종료(플레이어 패배)
        }
    }

    public void EnemyDied()
    {
        EdeathCount++;  // 적 데스 카운트 1씩증가

        if (EdeathCount >= enemyBase.Length)    // 적의 데스카운트가 적 오브젝트 갯수보다 크거나 같아지면
        {
            Debug.Log("플레이어 승리");
            Time.timeScale = 0; // 게임종료(플레이어 승리)
        }
    }

}