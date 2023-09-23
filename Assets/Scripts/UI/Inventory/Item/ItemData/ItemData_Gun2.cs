﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data - Gun2", menuName = "Scriptable Object/Item Data - Gun2", order = 15)]
public class ItemData_Gun2 : ItemData
{
    [Header("총2 데이터")]
    ItemData data;

    public override EquipType equipPart => EquipType.Gun;

    public override void ItemStatus()
    {
        price = 1000;      // 아이템 가치

        speed = 11;
        beforeStr = (upgrade + 1) * 10;                 // 아이템의 강화 전 Str 수치
        beforeAgi = (upgrade + 1) * 10;                 // 아이템의 강화 전 Agi 수치
        beforeInt = (upgrade + 1) * 10;                 // 아이템의 강화 전 Int 수치
        beforeHP = (upgrade + 1) * 20;                 // 아이템의 강화 전 HP 수치
        beforeMP = (upgrade + 1) * 20;                 // 아이템의 강화 전 MP 수치
        beforeValue = upgrade;                         // 아이템의 강화 전 강화 수치

        afterStr = (upgrade + 2) * 10;                  // 아이템의 강화 후 Str 수치
        afterAgi = (upgrade + 2) * 10;                  // 아이템의 강화 후 Agi 수치
        afterInt = (upgrade + 2) * 10;                  // 아이템의 강화 후 Int 수치
        afterHP = (upgrade + 2) * 20;                  // 아이템의 강화 후 HP 수치
        afterMP = (upgrade + 2) * 20;                  // 아이템의 강화 후 MP 수치
        afterValue = Mathf.Min(upgrade + 1, 5);        // 아이템의 강화 후 강화 수치

        risingStr = afterStr - beforeStr;    // 아이템의 강화 시 상승 Str 수치
        risingAgi = afterAgi - beforeAgi;    // 아이템의 강화 시 상승 Agi 수치
        risingInt = afterInt - beforeInt;    // 아이템의 강화 시 상승 Int 수치
        risingHP = afterHP - beforeHP;       // 아이템의 강화 시 상승 HP 수치
        risingMP = afterMP - beforeMP;       // 아이템의 강화 시 상승 MP 수치

        cost = (upgrade + 1) * 1000;                          // 아이템의 강화 시 소모 비용
    }
}