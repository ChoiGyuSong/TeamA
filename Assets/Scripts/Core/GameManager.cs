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
            Debug.Log("�÷��̾� �й�");
            // ���� ���� ����
            // ��: SceneManager.LoadScene("GameOver");
        }
    }

    public void EnemyDied()
    {
        Debug.Log("���ӸŴ��� ����");
        EdeathCount++;

        if (EdeathCount >= enemyBase.Length)
        {
            Debug.Log("�÷��̾� �¸�");
            // ���� ���� ����
            // ��: SceneManager.LoadScene("GameOver");
        }
    }
}
