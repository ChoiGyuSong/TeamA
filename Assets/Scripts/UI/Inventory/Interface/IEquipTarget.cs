using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipTarget
{
    /// <summary>
    /// ��� �������� ������ ���� Ȯ�ο� �ε���
    /// </summary>
    /// <param name="part">Ȯ���� ����</param>
    /// <returns>null�̸� ���Ǿ����� �ʴ�. null�� �ƴϸ� �� ���Կ� ����ִ� �������� ���� ����</returns>
    EquipSlot this[EquipType part] { get; }

    /// <summary>
    /// �������� ����ϴ� �Լ�
    /// </summary>
    /// <param name="part">����� ����</param>
    /// <param name="slot">����� �������� ����ִ� ����</param>
    void EquipItem(EquipType part, EquipSlot slot);

    /// <summary>
    /// �������� ��� �����ϴ� �Լ�
    /// </summary>
    /// <param name="part">�������� ������ ����</param>
    void UnEquipItem(EquipType part);

    /// <summary>
    /// ���� �������� �ڽ����� �� Ʈ�������� �����ִ� �Լ�
    /// </summary>
    /// <param name="part">���� ����</param>
    /// <returns>���� ������ �θ�Ʈ������</returns>
    Transform GetEquipParentTransform(EquipType part);
}
