using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    /// <summary>
    /// 이 아이템이 장착될 부위
    /// </summary>
    EquipType equipPart { get; }
}
