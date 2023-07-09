using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// PlayerBase �迭
    /// </summary>
    PlayerBase[] playerBase;

    /// <summary>
    /// EnemyBase �迭
    /// </summary>
    EnemyBase[] enemyBase;

    /// <summary>
    /// �÷��̾� ����ī��Ʈ(���������� ������)
    /// </summary>
    private int PdeathCount = 0;

    /// <summary>
    /// �� ����ī��Ʈ(���������� ����)
    /// </summary>
    private int EdeathCount = 0;

    private void Awake()
    {
        playerBase = FindObjectsOfType<PlayerBase>();
        enemyBase = FindObjectsOfType<EnemyBase>();
    }

    public void PlayerDied()
    {
        PdeathCount++;  // �÷��̾� ���� ī��Ʈ 1�� ����
        if (PdeathCount >= playerBase.Length)   // �÷��̾��� ����ī��Ʈ�� �÷��̾� ������Ʈ �������� ũ�ų� ��������
        {
            Debug.Log("�÷��̾� �й�");
            Time.timeScale = 0; // ��������(�÷��̾� �й�)
        }
    }

    public void EnemyDied()
    {
        EdeathCount++;  // �� ���� ī��Ʈ 1������

        if (EdeathCount >= enemyBase.Length)    // ���� ����ī��Ʈ�� �� ������Ʈ �������� ũ�ų� ��������
        {
            Debug.Log("�÷��̾� �¸�");
            Time.timeScale = 0; // ��������(�÷��̾� �¸�)
        }
    }

}