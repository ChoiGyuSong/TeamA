using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceEnemySpawner : MonoBehaviour
{
    [Serializable]
    public struct SpawnData
    {
        public SpawnData(PoolObjectType type = PoolObjectType.DefenceEnemy1, float interval = 0.5f)
        {
            this.spawnType = type;
            this.interval = interval;
        }
        public PoolObjectType spawnType;

        public float interval;
    }

    public SpawnData[] spawnDatas;

    private void Start()
    {
        foreach (var spawnData in spawnDatas)
        {
            StartCoroutine(SpawnCoroutine(spawnData));
        }
    }

    protected virtual DefenceEnemyBase Spawn(PoolObjectType spawnType)
    {
        GameObject obj = Factory.Inst.GetObject(spawnType);
        DefenceEnemyBase enemy = obj.GetComponent<DefenceEnemyBase>();
        return enemy;
    }

    IEnumerator SpawnCoroutine(SpawnData data)
    {
        while (true)
        {
            yield return new WaitForSeconds(data.interval);
            Spawn(data.spawnType);
        }
    }
}
