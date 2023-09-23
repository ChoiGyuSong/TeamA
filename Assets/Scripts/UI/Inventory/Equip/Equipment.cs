using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary>
/// 개념상 인벤토리(UI 없음)
/// </summary>
public class Equipment
{
    /// <summary>
    /// 임시슬롯용 인덱스
    /// </summary>
    public const uint TempSlotIndex = 999999999;

    /// <summary>
    /// 인벤토리 슬롯의 배열
    /// </summary>
    InvenSlot[] slots;

    EquipSlot[] equipSlots;

    /// <summary>
    /// 인벤토리 슬롯에 접근하기 위한 인덱서
    /// </summary>
    /// <param name="index">슬롯의 인덱스</param>
    /// <returns>슬롯</returns>
    public EquipSlot this[uint index] => equipSlots[index];

    /// <summary>
    /// 인벤토리 슬롯의 갯수
    /// </summary>
    public int SlotCount => equipSlots.Length;

    /// <summary>
    /// 임시 슬롯(드래그나 아이템 분리작업을 할 때 사용)
    /// </summary>
    InvenSlot tempSlot;
    public InvenSlot TempSlot => tempSlot;

    public EquipSlot EtempSlot;
    public EquipSlot ETempSlot => EtempSlot;

    public static EquipSlot equipSlot;

    /// <summary>
    /// 아이템 데이터 메니저(아이템 종류별 데이터를 확인할 수 있다.)
    /// </summary>
    ItemDataManager itemDataManager;

    /// <summary>
    /// 인벤토리 소유자(게임 매니저)
    /// </summary>
    GameManager owner;
    public GameManager Owner => owner;

    Inventory inven;

    /// <summary>
    /// 인벤토리, 장비창의 연결 및 초기화
    /// </summary>
    /// <param name="owner">인벤토리 소유자</param>
    /// <param name="size">인벤토리의 크기</param>
    public Equipment(GameManager owner, uint size = 9)
    {
        inven = new Inventory(owner);

        slots = new InvenSlot[30];
        for (uint i = 0; i < size; i++)
        {
            slots[i] = new InvenSlot(i);                // 슬롯 만들어서 저장
        }

        equipSlots = new EquipSlot[size];
        for (uint i = 0; i < size; i++)
        {
            equipSlots[i] = new EquipSlot(i);                // 슬롯 만들어서 저장
        }

        EtempSlot = new EquipSlot(TempSlotIndex);
        tempSlot = new InvenSlot(TempSlotIndex);

        itemDataManager = GameManager.Inst.ItemData;    // 아이템 데이터 메니저 캐싱
        this.owner = owner;                             // 소유자 기록
    }

    /// <summary>
    /// 인벤토리의 아이템을 from위치에서 to위치로 아이템을 이동시키는 함수
    /// </summary>
    /// <param name="from">위치 변경이 시작되는 인덱스</param>
    /// <param name="to">위치 변경이 끝나는 인덱스</param>
    public void MoveItem(uint from, uint to)
    {
        // from지점과 to지점이 다르고 from과 to가 모두 valid해야 한다.
        if ((from != to) && IsValidIndex(from) && IsValidIndex(to))
        {
            // from이 TempSlotIndex(임시슬롯)과 동일하면 fromSlot에 ETempSlot(장비창에서 만들어진 임시슬롯), 동일하지 않다면 장비창의 출발 슬롯
            EquipSlot fromSlot = (from == TempSlotIndex) ? ETempSlot : equipSlots[from];

            // tempSlot이 TempSlotIndex(임시슬롯)과 동일하면 fromSlot에 Inventroy.invenSlot(인벤토리에서 만들어진 임시슬롯), 동일하지 않으면 인벤토리의 출발슬롯
            tempSlot = (from == TempSlotIndex) ? Inventory.invenSlot : slots[from];

            if (!fromSlot.IsEmpty && tempSlot.IsEmpty)  // 장비창에서 만든 임시슬롯 or 출발슬롯은 존재하고, 인벤토리에서 만들어진 임시슬롯 or 출발슬롯이 비어있다
            {
                EquipSlot EtoSlot = (to == TempSlotIndex) ? ETempSlot : equipSlots[to];     // to가 임시슬롯이라면
                                                                                               
                if (EtoSlot != null)
                {
                    ItemData tempData = fromSlot.ItemData;      // 아이템 데이터 저장
                    fromSlot.AssignSlotItem(EtoSlot.ItemData);  // fromSlotd의 아이템을 EtoSlot에 저장
                    EtoSlot.AssignSlotItem(tempData);           // EtoSlot의 아이템을 tempData에 저장

                    equipSlot = EtoSlot;    // 전역변수 equipSlot에 EtoSlot아이템 저장(인벤토리로의 이동때)
                }
            }

            else if(!tempSlot.IsEmpty && fromSlot.IsEmpty)  // 인벤토리에서 만들어진 임시슬롯이 존재하고, 장비창에서 만든 임시슬롯은 비어있다
            {
                EquipSlot EtoSlot = (to == TempSlotIndex) ? ETempSlot : equipSlots[to];

                if(EtoSlot != null)
                {
                    // 다른 종류의 아이템이면 서로 스왑
                    ItemData tempData = tempSlot.ItemData;
                    tempSlot.AssignSlotItem(EtoSlot.ItemData);
                    EtoSlot.AssignSlotItem(tempData);
                }
            }
        }
    }

    /// <summary>
    /// 적절한 인덱스인지 확인하는 함수
    /// </summary>
    /// <param name="index">확인할 인덱스</param>
    /// <returns>true면 적절한 인덱스, false면 없는 인덱스</returns>
    bool IsValidIndex(uint index) => (index < SlotCount) || (index == TempSlotIndex) || (index == TempSlotIndex);

}
