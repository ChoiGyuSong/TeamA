using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    /// <summary>
    /// �� �������� ������ ����
    /// </summary>
    EquipType equipPart { get; }

    /// <summary>
    /// �������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="target">�������� ���</param>
    /// <param name="slot">������ �������� �ִ� ����</param>
    void EquipItem(GameObject target, EquipSlot slot);

    /// <summary>
    /// �������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="target">���� ������ ���</param>
    /// <param name="slot">������ �������� �ִ� ����</param>
    void UnEquipItem(GameObject target, EquipSlot slot);

    /// <summary>
    /// ��Ȳ�� ���� �������� �����ϰ� �����ϴ� �Լ�
    /// </summary>
    /// <param name="target">���� �� ���� ���</param>
    /// <param name="slot">�������� �ִ� ����</param>
    void ToggleEquip(GameObject target, EquipSlot slot);
}
