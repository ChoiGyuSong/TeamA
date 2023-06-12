using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PoolObjectType
{
    Warrior =0,
    Archer,
    Mage,
    Enemy_Warrior,
    Enemy_Archer,
    Enemy_Mage,
}

public class Factory : Singleton<Factory>
{
    public GameObject warrior;
    public GameObject archer;
    public GameObject mage;
    public GameObject enemy_Warrior;
    public GameObject enemy_Archer;
    public GameObject enemy_Mage;
  
  
  

    public GameObject GetObject(PoolObjectType type)
    {
        GameObject result;
        switch (type)
        {
            case PoolObjectType.Warrior:
                result = Instantiate(warrior);
                break;
            case PoolObjectType.Archer:
                result = Instantiate(archer);
                break;
            case PoolObjectType.Mage:
                result = Instantiate(mage);
                break;
            case PoolObjectType.Enemy_Warrior:
                result = Instantiate(enemy_Warrior);
                break;
            case PoolObjectType.Enemy_Archer:
                result = Instantiate(enemy_Archer);
                break;
            case PoolObjectType.Enemy_Mage:
                result = Instantiate(enemy_Mage);
                break;
            default:
                result = new GameObject();
                break;
        }

        return result;
    }
}
