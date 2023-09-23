using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �� ������ ���� �⺻ ������ ������ ��ũ���ͺ� ������Ʈ
// ��ũ���ͺ� ������Ʈ : ������ ������ ����� ������ �� �ְ� ���ִ� Ŭ����

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("������ �⺻ ������")]
    public ItemCode code;                       // ������ �ڵ�
    public string itemName = "������";          // ������ �̸�
    public Sprite itemIcon;                     // �������� �κ��丮 �ȿ��� ���� ������
    public uint maxStackCount = 1;              // �������� �κ��丮 ���Կ��� �ִ� ��� ������ �� �ִ���

    public virtual EquipType equipPart => EquipType.Armor;

    [HideInInspector]
    public int upgrade = 0;
    [HideInInspector]
    public int speed = 0;

    [HideInInspector]
    public int beforeStr = 0;           // �������� ��ȭ �� Str ��ġ
    [HideInInspector]
    public int beforeAgi = 0;           // �������� ��ȭ �� Agi ��ġ
    [HideInInspector]
    public int beforeInt = 0;           // �������� ��ȭ �� Int ��ġ
    [HideInInspector]
    public int beforeHP = 0;            // �������� ��ȭ �� HP ��ġ
    [HideInInspector]
    public int beforeMP = 0;            // �������� ��ȭ �� MP ��ġ
    [HideInInspector]
    public int beforeValue = 0;         // �������� ��ȭ �� ��ȭ ��ġ

    [HideInInspector]
    public int afterStr = 0;            // �������� ��ȭ �� Str ��ġ
    [HideInInspector]
    public int afterAgi = 0;            // �������� ��ȭ �� Agi ��ġ
    [HideInInspector]
    public int afterInt = 0;            // �������� ��ȭ �� Int ��ġ
    [HideInInspector]
    public int afterHP = 0;             // �������� ��ȭ �� HP ��ġ
    [HideInInspector]
    public int afterMP = 0;             // �������� ��ȭ �� MP ��ġ
    [HideInInspector]
    public int afterValue = 0;          // �������� ��ȭ �� ��ȭ ��ġ

    [HideInInspector]
    public int risingStr = 0;           // �������� ��ȭ �� ��� Str ��ġ
    [HideInInspector]
    public int risingAgi = 0;           // �������� ��ȭ �� ��� Agi ��ġ
    [HideInInspector]
    public int risingInt = 0;           // �������� ��ȭ �� ��� Int ��ġ
    [HideInInspector]
    public int risingHP = 0;            // �������� ��ȭ �� ��� HP ��ġ
    [HideInInspector]
    public int risingMP = 0;            // �������� ��ȭ �� ��� MP ��ġ

    [HideInInspector]
    public uint price = 0;              // ������ ��ġ
    [HideInInspector]
    public int cost = 0;                // �������� ��ȭ �� �Ҹ� ���
    

    public virtual void ItemStatus()
    {
    }
}