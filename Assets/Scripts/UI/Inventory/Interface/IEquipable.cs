using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    /// <summary>
    /// �� �������� ������ ����
    /// </summary>
    EquipType equipPart { get; }
}
