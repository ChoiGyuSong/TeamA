using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PoolObjectType
{
    DefenceEnemy1 = 0,
}

public class Factory : Singleton<Factory>
{
    Enemy1Pool enemy1Pool;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        enemy1Pool = GetComponentInChildren<Enemy1Pool>();

        enemy1Pool?.Initialize();
    }

    public GameObject GetObject(PoolObjectType type)
    {
        GameObject result = type switch
        {
            PoolObjectType.DefenceEnemy1 => enemy1Pool?.GetObject()?.gameObject,
            _ => new GameObject(),
        };
        return result;
    }
}
