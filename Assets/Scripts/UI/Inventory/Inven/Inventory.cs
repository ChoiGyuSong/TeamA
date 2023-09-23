using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary>
/// 개념상 인벤토리(UI 없음)
/// </summary>
public class Inventory
{
    /// <summary>
    /// 인벤토리에 들어있는 인벤 슬롯의 기본 갯수
    /// </summary>
    public const int Default_Inventory_Size = 30;

    /// <summary>
    /// 임시슬롯용 인덱스
    /// </summary>
    public const uint TempSlotIndex = 999999999;

    /// <summary>
    /// 이 인벤토리에 들어있는 슬롯의 배열
    /// </summary>
    InvenSlot[] slots;

    EquipSlot[] equipSlots;

    /// <summary>
    /// 인벤토리 슬롯에 접근하기 위한 인덱서
    /// </summary>
    /// <param name="index">슬롯의 인덱스</param>
    /// <returns>슬롯</returns>
    public InvenSlot this[uint index] => slots[index];

    /// <summary>
    /// 인벤토리 슬롯의 갯수
    /// </summary>
    public int SlotCount => slots.Length;

    /// <summary>
    /// 임시 슬롯(드래그나 아이템 분리작업을 할 때 사용)
    /// </summary>
    InvenSlot tempSlot;
    public InvenSlot TempSlot => tempSlot;

    public static InvenSlot invenSlot;
    public EquipSlot equipSlot;
    Equipment equip;

    public EquipSlot ETempSlot => equipSlot;

    /// <summary>
    /// 아이템 데이터 메니저(아이템 종류별 데이터를 확인할 수 있다.)
    /// </summary>
    ItemDataManager itemDataManager;

    /// <summary>
    /// 인벤토리 소유자
    /// </summary>
    GameManager owner;
    public GameManager Owner => owner;

    /// <summary>
    /// 인벤토리 생성자
    /// </summary>
    /// <param name="owner">인벤토리 소유자</param>
    /// <param name="size">인벤토리의 크기</param>
    public Inventory(GameManager owner, uint size = Default_Inventory_Size)
    {
        invenSlot = new InvenSlot(size);

        slots = new InvenSlot[size];
        for (uint i = 0; i < size; i++)
        {
            slots[i] = new InvenSlot(i);                // 슬롯 만들어서 저장
        }

        equipSlots = new EquipSlot[size];
        for (uint i = 0; i < 9; i++)
        {
            equipSlots[i] = new EquipSlot(size);
        }

        tempSlot = new InvenSlot(TempSlotIndex);
        equipSlot = new EquipSlot(TempSlotIndex);

        itemDataManager = new ItemDataManager();
        itemDataManager = GameManager.Inst.ItemData;    // 아이템 데이터 메니저 캐싱
        this.owner = owner;                             // 소유자 기록
    }


    /// <summary>
    /// 인벤토리에 아이템을 하나 추가하는 함수
    /// </summary>
    /// <param name="code">추가할 아이템 종류</param>
    /// <returns>true면 추가 성공, false면 추가 실패</returns>
    public bool AddItem(ItemCode code)
    {
        bool result = false;
        ItemData data = itemDataManager[code];

        // 같은 종류의 아이템이 없다.
        InvenSlot emptySlot = FindEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.AssignSlotItem(data); // 빈슬롯이 있으면 아이템 하나 할당
            result = true;
        }
        else
        {
            // 비어있는 슬롯이 없다.
            Debug.Log("아이템 추가 실패 : 인벤토리가 가득 차있습니다.");
        }

        return result;
    }

    /// <summary>
    /// 인벤토리의 특정 슬롯에 아이템을 하나 추가하는 함수
    /// </summary>
    /// <param name="code">추가할 아이템의 종류</param>
    /// <param name="slotIndex">아이템을 추가할 인덱스</param>
    /// <returns></returns>
    public bool AddItem(ItemCode code, uint slotIndex)
    {
        bool result = false;

        if (IsValidIndex(slotIndex))   // 인덱스가 적절한지 확인
        {
            ItemData data = itemDataManager[code];  // 아이템 데이터 가져오기
            InvenSlot slot = slots[slotIndex];      // 아이템을 추가할 슬롯 가져오기
            if (slot.IsEmpty)
            {
                slot.AssignSlotItem(data);          // 슬롯이 비었으면 아이템 할당
            }
            else
            {

            }
        }
        return result;
    }

    /// <summary>
    /// 인벤토리에서 아이템을 삭제하는 함수
    /// </summary>
    /// <param name="slotIndex">아이템을 삭제할 슬롯의 인덱스</param>
    public void ClearSlot(uint slotIndex)
    {
        if (IsValidIndex(slotIndex))
        {
            InvenSlot invenSlot = slots[slotIndex];
            invenSlot.ClearSlotItem();
        }
    }

    /// <summary>
    /// 인벤토리를 전부 비우는 함수
    /// </summary>
    public void ClearInventory()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlotItem();
        }
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
            InvenSlot fromSlot = (from == TempSlotIndex) ? TempSlot : slots[from];  // 임시 슬롯을 감안해서 삼항연산자로 처리
            equipSlot = (from == TempSlotIndex) ? Equipment.equipSlot : equipSlots[from];
            if (fromSlot != null)
            {
                if (!fromSlot.IsEmpty)//(equipSlot.IsEmpty)
                {
                    InvenSlot toSlot = (to == TempSlotIndex) ? TempSlot : slots[to];

                    if (toSlot != null)     // toSlot이 Inven이라면          && EtoSlot == null
                    {
                        // 다른 종류의 아이템이면 서로 스왑
                        ItemData tempData = fromSlot.ItemData;
                        fromSlot.AssignSlotItem(toSlot.ItemData);
                        toSlot.AssignSlotItem(tempData);

                        invenSlot = toSlot;
                    }
                }
                else if (!equipSlot.IsEmpty) //(fromSlot.IsEmpty)
                {
                    InvenSlot toSlot = (to == TempSlotIndex) ? TempSlot : slots[to];

                    if (toSlot.IsEmpty)     // toSlot이 Inven이라면          && EtoSlot == null
                    {
                        // 다른 종류의 아이템이면 서로 스왑
                        ItemData tempData = equipSlot.ItemData;
                        equipSlot.AssignSlotItem(toSlot.ItemData);
                        toSlot.AssignSlotItem(tempData);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 모든 슬롯이 변경되었음을 알리는 함수
    /// </summary>
    void RefreshInventory()
    {
        foreach (var slot in slots)
        {
            slot.onSlotItemChange?.Invoke();
        }
    }

    /// <summary>
    /// 인벤토리에서 비어있는 슬롯을 찾는 함수
    /// </summary>
    /// <returns>비어있는 슬롯(첫번째)</returns>
    InvenSlot FindEmptySlot()
    {
        InvenSlot findSlot = null;
        foreach (var slot in slots)     // 모든 슬롯을 다 돌면서
        {
            if (slot.IsEmpty)            // 비어있는 슬롯이 있으면 찾았다.
            {
                findSlot = slot;
                break;
            }
        }

        return findSlot;
    }

    /// <summary>
    /// 빈슬롯을 못찾았을 때의 인덱스
    /// </summary>
    const uint NotFindEmptySlot = uint.MaxValue;

    /// <summary>
    /// 비어있는 슬롯의 인덱스를 돌려주는 함수
    /// </summary>
    /// <param name="index">출력용 파라메터, 빈슬롯을 찾았을 경우에 인덱스값</param>
    /// <returns>true면 빈슬롯을 찾았다, false면 빈슬롯이 없다.</returns>
    public bool FindEmpySlotIndex(out uint index)
    {
        bool result = false;
        index = NotFindEmptySlot;

        InvenSlot slot = FindEmptySlot();   // 빈슬롯 찾아서
        if (slot != null)
        {
            index = slot.Index;             // 빈슬롯이 있으면 인덱스 설정
            result = true;
        }
        return result;
    }

    /// <summary>
    /// 적절한 인덱스인지 확인하는 함수
    /// </summary>
    /// <param name="index">확인할 인덱스</param>
    /// <returns>true면 적절한 인덱스, false면 없는 인덱스</returns>
    bool IsValidIndex(uint index) => (index < SlotCount) || (index == TempSlotIndex) || (index == TempSlotIndex);
}