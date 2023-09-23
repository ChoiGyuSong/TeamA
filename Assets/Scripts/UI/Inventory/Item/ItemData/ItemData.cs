using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 한 종류에 대한 기본 정보를 가지는 스크립터블 오브젝트
// 스크립터블 오브젝트 : 데이터 파일의 양식을 설정할 수 있게 해주는 클래스

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("아이템 기본 데이터")]
    public ItemCode code;                       // 아이템 코드
    public string itemName = "아이템";          // 아이템 이름
    public Sprite itemIcon;                     // 아이템이 인벤토리 안에서 보일 아이콘
    public uint maxStackCount = 1;              // 아이템이 인벤토리 슬롯에서 최대 몇개지 누적될 수 있는지

    public virtual EquipType equipPart => EquipType.Armor;

    [HideInInspector]
    public int upgrade = 0;
    [HideInInspector]
    public int speed = 0;

    [HideInInspector]
    public int beforeStr = 0;           // 아이템의 강화 전 Str 수치
    [HideInInspector]
    public int beforeAgi = 0;           // 아이템의 강화 전 Agi 수치
    [HideInInspector]
    public int beforeInt = 0;           // 아이템의 강화 전 Int 수치
    [HideInInspector]
    public int beforeHP = 0;            // 아이템의 강화 전 HP 수치
    [HideInInspector]
    public int beforeMP = 0;            // 아이템의 강화 전 MP 수치
    [HideInInspector]
    public int beforeValue = 0;         // 아이템의 강화 전 강화 수치

    [HideInInspector]
    public int afterStr = 0;            // 아이템의 강화 후 Str 수치
    [HideInInspector]
    public int afterAgi = 0;            // 아이템의 강화 후 Agi 수치
    [HideInInspector]
    public int afterInt = 0;            // 아이템의 강화 후 Int 수치
    [HideInInspector]
    public int afterHP = 0;             // 아이템의 강화 후 HP 수치
    [HideInInspector]
    public int afterMP = 0;             // 아이템의 강화 후 MP 수치
    [HideInInspector]
    public int afterValue = 0;          // 아이템의 강화 후 강화 수치

    [HideInInspector]
    public int risingStr = 0;           // 아이템의 강화 시 상승 Str 수치
    [HideInInspector]
    public int risingAgi = 0;           // 아이템의 강화 시 상승 Agi 수치
    [HideInInspector]
    public int risingInt = 0;           // 아이템의 강화 시 상승 Int 수치
    [HideInInspector]
    public int risingHP = 0;            // 아이템의 강화 시 상승 HP 수치
    [HideInInspector]
    public int risingMP = 0;            // 아이템의 강화 시 상승 MP 수치

    [HideInInspector]
    public uint price = 0;              // 아이템 가치
    [HideInInspector]
    public int cost = 0;                // 아이템의 강화 시 소모 비용
    

    public virtual void ItemStatus()
    {
    }
}