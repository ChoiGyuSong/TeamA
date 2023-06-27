using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    CharacterBase[] playerBase;
    CharacterBase[] enemyBase;

    private int PdeathCount = 0;
    private int EdeathCount = 0;

    private void Awake()
    {
        playerBase = FindObjectsOfType<PlayerBase>();
        enemyBase = FindObjectsOfType<EnemyBase>();
    }

    public void PlayerDied()
    {
        PdeathCount++;

        if (PdeathCount >= playerBase.Length)
        {
            Debug.Log("플레이어 패배");
            // 게임 종료 로직
            // 예: SceneManager.LoadScene("GameOver");
        }
    }

    public void EnemyDied()
    {
        Debug.Log("게임매니저 실행");
        EdeathCount++;

        if (EdeathCount >= enemyBase.Length)
        {
            Debug.Log("플레이어 승리");
            // 게임 종료 로직
            // 예: SceneManager.LoadScene("GameOver");
        }
    }
}
