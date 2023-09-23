using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    /// <summary>
    /// 이 UI가 보여줄 인벤토리 
    /// </summary>
    Inventory inven;

    /// <summary>
    /// 장비창
    /// </summary>
    Equipment equip;

    /// <summary>
    /// 이 인벤토리가 가지고 있는 모든 슬롯의 UI
    /// </summary>
    InvenSlotUI[] slotsUI;

    /// <summary>
    /// 장비창의 슬롯UI
    /// </summary>
    EquipSlotUI[] equipSlotsUI;

    /// <summary>
    /// 아이템 이동이나 분리할 때 사용할 임시 슬롯 UI
    /// </summary>
    TempSlotUI tempSlotUI;

    /// <summary>
    /// 아이템의 상세정보를 표시하는 패널
    /// </summary>
    DetailInfoUI detail;

    /// <summary>
    /// 소유자가 가지고 있는 금액을 보여주는 패널
    /// </summary>
    MoneyPanel moneyPanel;

    /// <summary>
    /// 1번 캐릭터의 스텟
    /// </summary>
    Stat_1 stat1;

    /// <summary>
    /// 2번 캐릭터의 스텟
    /// </summary>
    Stat_2 stat2;

    /// <summary>
    /// 3번 캐릭터의 스텟
    /// </summary>
    Stat_3 stat3;

    /// <summary>
    /// 이 인벤토리의 소유자를 확인하기 위한 프로퍼티
    /// </summary>
    public GameManager Owner => inven.Owner;

    /// <summary>
    /// 인풋 액션
    /// </summary>
    PlayerInputAction inputActions;

    CanvasGroup canvasGroup;

    public Action detailOpen;
    public Action detailClose;

    public Action onInventoryOpen;
    public Action onInventoryClose;


    /// <summary>
    /// 디테일 창 표시시 index 저장할 변수
    /// </summary>
    uint old;




    private void Awake()
    {
        equipSlotsUI = FindObjectsOfType<EquipSlotUI>();

        tempSlotUI = FindObjectOfType<TempSlotUI>();

        detail = FindObjectOfType<DetailInfoUI>();

        inputActions = new PlayerInputAction();

        equipSlotsUI[0].equipType = EquipType.Gun;      // 장비 슬롯별로 타입 하나씩 지정
        equipSlotsUI[1].equipType = EquipType.Hammer;
        equipSlotsUI[2].equipType = EquipType.LBow;
        equipSlotsUI[3].equipType = EquipType.Pants;
        equipSlotsUI[4].equipType = EquipType.Armor;
        equipSlotsUI[5].equipType = EquipType.Pants;
        equipSlotsUI[6].equipType = EquipType.Armor;
        equipSlotsUI[7].equipType = EquipType.Armor;
        equipSlotsUI[8].equipType = EquipType.Pants;

        Transform child = transform.GetChild(0);
        Transform Gchild = child.transform.GetChild(1);

        stat1 = Gchild.GetComponent<Stat_1>();

        child = transform.GetChild(1);
        Gchild = child.transform.GetChild(1);
        stat2 = Gchild.GetComponent<Stat_2>();

        child = transform.GetChild(2);
        Gchild = child.transform.GetChild(1);
        stat3 = Gchild.GetComponent<Stat_3>();
    }

    /// <summary>
    /// 인벤토리 UI 초기화 함수
    /// </summary>
    /// <param name="equipInven">이 UI와 연결될 인벤토리</param>
    public void InitializeInventory(Equipment equipInven, Inventory inventory)
    {
        equip = equipInven;
        inven = inventory;

        // 슬롯 초기화(초기화 함수 실행 및 델리게이트 연결하기)
        for (uint i = 0; i < equipSlotsUI.Length; i++)
        {
            equipSlotsUI[i].EInitializeSlot(equip[i]);
            equipSlotsUI[i].onDragBegin += OnItemMoveBegin;
            equipSlotsUI[i].onDragEnd += OnItemMoveEnd;
            equipSlotsUI[i].onClick += OnSlotClick;
            equipSlotsUI[i].onItemLeftClick += OnItemDetailClickOn;
            equipSlotsUI[i].onItemRightClick += OnItemDetailClickOff;
        }

        // 임시 슬롯 초기화
        tempSlotUI.EInitializeSlot(equip.ETempSlot);
        tempSlotUI.onTempSlotOpenClose += OnDetailPause;

        // 상세 정보창 닫아 놓기
        detail.Close();
    }

    /// <summary>
    /// 슬롯UI에서 드래그가 시작되면 실행될 함수
    /// </summary>
    /// <param name="index">드래그가 시작된 슬롯의 인덱스</param>
    private void OnItemMoveBegin(uint index)
    {
        equip.MoveItem(index, tempSlotUI.Index);    // 시작 슬롯에서 임시 슬롯으로 아이템 옮기기
        EquipSlotUI.dragStartSlotIndex = index;     // 드래그 시작지점 전역변수 dragStartSlotIndex에 저장
        tempSlotUI.Open();                          // 임시 슬롯 열기
    }

    /// <summary>
    /// 슬롯UI에서 드래그가 끝났을 때 실행될 함수
    /// </summary>
    /// <param name="index">드래그가 끝난 슬롯의 인덱스</param>
    /// <param name="isSuccess">드래그가 성공적인지 여부</param>
    private void OnItemMoveEnd(uint index, bool isSuccess)
    {
        uint finalIndex = index;                        // 실제 사용할 인덱스(기본적으로는 파라메터로 받은 인덱스)
        if(!Inventory.invenSlot.IsEmpty)    // 드래그가 끝났을때 인벤토리에서 발생된 임시슬롯에 아이템이 있다면
        {
            if (Inventory.invenSlot.ItemData.equipPart == equipSlotsUI[index].equipType)    // 임시슬롯에 있는 아이템 타입 == 마우스 위치 슬롯의 지정타입이라면
            {
                equip.MoveItem(tempSlotUI.Index, finalIndex);   // 임시 슬롯에서 장비 슬롯으로 아이템 옮기기
                stat1.Status();     // 이동 끝났으면 1번 캐릭터의 스텟 갱신
                stat2.Status();     // 이동 끝났으면 2번 캐릭터의 스텟 갱신
                stat3.Status();     // 이동 끝났으면 3번 캐릭터의 스텟 갱신
            }
            else
            {
                // 임시슬롯에 있는 아이템 타입 =! 마우스 위치 슬롯의 지정타입이라면(장착 실패)
                finalIndex = EquipSlotUI.dragStartSlotIndex;    // 드래그를 시작한 위치로
                inven.MoveItem(tempSlotUI.Index, finalIndex);   // 임시 슬롯에서 인벤 슬롯으로 아이템 옮기기(원래 자리로 돌아감)
            }
        }
        else if(!Equipment.equipSlot.IsEmpty)   // 드래그가 끝났을때 장비창에서 발생된 임시슬롯에 아이템이 있다면
        {
            if (Equipment.equipSlot.ItemData.equipPart == equipSlotsUI[index].equipType)    // 임시슬롯에 있는 아이템 타입 == 마우스 위치 슬롯의 지정타입
            {
                equip.MoveItem(tempSlotUI.EIndex, finalIndex);   // 임시 슬롯에서 장비 슬롯으로 아이템 옮기기
                stat1.Status();     // 이동 끝났으면 1번 캐릭터의 스텟 갱신
                stat2.Status();     // 이동 끝났으면 2번 캐릭터의 스텟 갱신
                stat3.Status();     // 이동 끝났으면 3번 캐릭터의 스텟 갱신
            }
            else
            {
                // 임시슬롯에 있는 아이템 타입 =! 마우스 위치 슬롯의 지정타입이라면(장착 실패)
                finalIndex = EquipSlotUI.dragStartSlotIndex;    // 드래그를 시작한 위치로
                equip.MoveItem(tempSlotUI.EIndex, finalIndex);  // 임시 슬롯에서 원래 슬롯으로 아이템 옮기기
            }
        }
        
        if (!isSuccess)     // 실패했다면
        {
            finalIndex = EquipSlotUI.dragStartSlotIndex;    // 드래그를 시작한 위치로
            equip.MoveItem(tempSlotUI.Index, finalIndex);   // 임시 슬롯에서 장비 슬롯으로 아이템 옮기기
        }
        if (tempSlotUI.InvenSlot.IsEmpty)               // 비었다면
        {
            tempSlotUI.Close();                         // 임시 슬롯 닫기
        }
    }

    /// <summary>
    /// 마우스 포인터가 슬롯위에서 클릭되었을때 실행되는 함수
    /// </summary>
    /// <param name="index">올라간 슬롯의 인덱스</param>
    private void OnItemDetailClickOn(uint index)
    {
        detail.Open(equipSlotsUI[index].EquipSlot.ItemData); // 상세정보창 열기
    }

    private void OnItemDetailClickOff(uint index)
    {
        detail.Close();
    }

    /// <summary>
    /// 임시 슬롯이 열리면 상세정보창을 일시 정지하고, 닫히면 일시 정지를 푸는 함수
    /// </summary>
    /// <param name="isPause">true면 일시 정지, false 해제</param>
    private void OnDetailPause(bool isPause)
    {
        detail.IsPause = isPause;
    }

    /// <summary>
    /// 슬롯UI에 마우스가 클릭이 되었을 때 실행될 함수
    /// </summary>
    /// <param name="index">클릭된 슬롯의 인덱스</param>
    private void OnSlotClick(uint index)
    {
        if (!tempSlotUI.InvenSlot.IsEmpty)
        {
            // 임시 슬롯에 아이템이 있을 때 클릭이 되었으면
            OnItemMoveEnd(index, true); // 클릭된 슬롯으로 아이템 이동
        }
    }

    /// <summary>
    /// 인벤토리를 여는 함수
    /// </summary>
    public void Open()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        onInventoryOpen?.Invoke();
    }

    /// <summary>
    /// 인벤토리를 닫을 함수
    /// </summary>
    private void Close()
    {
        if (!tempSlotUI.InvenSlot.IsEmpty)
        {
            OnItemMoveEnd(0, false);
        }

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0.0f;
        onInventoryClose?.Invoke();
    }
}
