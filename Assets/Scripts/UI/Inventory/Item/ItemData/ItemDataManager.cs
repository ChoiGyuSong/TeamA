using JetBrains.Annotations;
using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipType
{
    Armor = 0,
    Pants,
    SSword,
    LSword,
    SBow,
    LBow,
    Hammer,
    Gun
}

public enum ItemCode
{
    Armor = 0,
    Pants,
    SBow1,
    SBow2,
    LBow1,
    LBow2,
    SSword1,
    SSword2,
    LSword1,
    LSword2,
    Hammer1,
    Hammer2,
    Gun1,
    Gun2,
}

// ��� ������ ������ ���� ������ ������ �ִ� ������ Ŭ����(���� �޴����� ���� ���� ����)

public class ItemDataManager : MonoBehaviour
{
    /// <summary>
    /// ��� ������ ������ ���� �迭
    /// </summary>
    public ItemData[] itemDatas = null;
    //public ItemData[] itemDatas = new ItemData[Enum.GetValues(typeof(ItemCode)).Length];

    /// <summary>
    /// ������ ������ ������ ���� �ε���
    /// </summary>
    /// <param name="code">������ �������� �ڵ�</param>
    /// <returns>������ ������</returns>
    public ItemData this[ItemCode code] => itemDatas[(int)code];

    /// <summary>
    /// ������ ������ ������ ���� �ε���(�׽�Ʈ��)
    /// </summary>
    /// <param name="index">������ �������� �ε���</param>
    /// <returns></returns>
    public ItemData this[int index] => itemDatas[index];

    /// <summary>
    /// ���� �����ϴ� ������ ������ ��� ����
    /// </summary>
    public int length => itemDatas.Length;

    private void Start()
    {
        for(int i = 0; i < length; i++)
        {
            itemDatas[i].ItemStatus();
        }
    }
}
