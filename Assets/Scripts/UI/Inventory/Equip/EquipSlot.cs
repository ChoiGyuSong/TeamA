﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot
{
    /// <summary>
    /// 인벤토리에서의 인덱스
    /// </summary>
    uint slotIndex;

    /// <summary>
    /// 인벤토리에서의 인덱스를 확인하기 위한 프로퍼티
    /// </summary>
    public uint Index => slotIndex;

    /// <summary>
    /// 이 슬롯에 들어있는 아이템의 종류
    /// </summary>
    public ItemData slotItemData = null;

    /// <summary>
    /// 이 슬롯에 들어있는 아이템의 종류를 확인하기 위한 프로퍼티(쓰기는 private)
    /// </summary>
    public ItemData ItemData
    {
        get => slotItemData;
        private set
        {
            if (slotItemData != value)      // 종류가 변경될 때만
            {
                slotItemData = value;       // 변경 작업 처리
                onSlotItemChange?.Invoke(); // 아이템 종류가 변경되었다고 알람 보내기
            }
        }
    }
    /// <summary>
    /// 슬롯에 들어있는 아이템의 종류, 개수, 장비 여부가 변경되었다고 알리는 델리게이트
    /// </summary>
    public Action onSlotItemChange;

    /// <summary>
    /// 슬롯에 아이템이 있는지 없는지 확인하는 프로퍼티(true면 비어있고, false면 아이템이 들어있다.)
    /// </summary>
    public bool IsEmpty => slotItemData == null;

    /// <summary>
    /// 이 슬롯의 아이템이 장비되었는지 여부
    /// </summary>
    bool isEquipped = false;

    /// <summary>
    /// 이 슬롯의 장비여부를 확인하기 위한 프로퍼티
    /// </summary>
    public bool IsEquipped
    {
        get => isEquipped;
        set
        {
            isEquipped = value;
            onSlotItemChange?.Invoke(); // 무조건 변경되었다고 알림
        }
    }

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="index">이 슬롯의 인덱스(인벤토리에서 몇번째 슬롯인지)</param>
    public EquipSlot(uint index)
    {
        slotIndex = index;      // slotIndex는 이후로 절대 변하면 안된다.
        IsEquipped = false;
    }

    /// <summary>
    /// 이 슬롯에 아이템을 설정하는 함수
    /// </summary>
    /// <param name="data">설정할 아이템 종류</param>
    /// <param name="count">설정할 아이템 개수(set 용도, 추가되는 것이 아님)</param>
    public void AssignSlotItem(ItemData data, uint count = 1)
    {
        //Debug.Log("이큅슬롯 아이템 설정");
        //Debug.Log($"ItemData = {data}");

        if (data != null)
        {
            ItemData = data;
            IsEquipped = false;
            
            //Debug.Log($"인벤토리 {slotIndex}번 슬롯에 \"{ItemData.itemName}\" 아이템이 {ItemCount}개 설정");
        }
        else
        {
            ClearSlotItem();    // data가 null이면 해당 슬롯은 초기화
        }
    }

    /// <summary>
    /// 이 슬롯을 비우는 함수
    /// </summary>
    public void ClearSlotItem()
    {
        ItemData = null;
        IsEquipped = false;
        //Debug.Log($"인벤토리 {slotIndex}번 슬롯을 비웁니다.");
    }

    /// <summary>
    /// 슬롯에 있는 아이템을 장비하는 함수
    /// </summary>
    /// <param name="target">아이템을 장비할 대상</param>
    public void EquipItem(GameObject target)
    {
        IEquipable equip = ItemData as IEquipable;  // 장비 가능한 아이템이면
        if (equip != null)
        {
            equip.ToggleEquip(target, this);        // target에게 장비 시도
        }
    }
}
