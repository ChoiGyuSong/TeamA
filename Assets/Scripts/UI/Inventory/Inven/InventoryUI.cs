using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    /// <summary>
    /// 이 UI가 보여줄 인벤토리 
    /// </summary>
    Inventory inven;

    InvenSlot tempSlot;

    /// <summary>
    /// 이 인벤토리가 가지고 있는 모든 슬롯의 UI
    /// </summary>
    InvenSlotUI[] slotsUI;

    InvenSlotUI[] invenSlotUI;
    InvenSlotUI[] equipSlotsUI; 
    InvenSlotUI[] equipSlotsUI1; 
    InvenSlotUI[] equipSlotsUI2; 
    InvenSlotUI[] equipSlotsUI3; 

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

    bool mousePoint;

    /// <summary>
    /// 디테일 창 표시시 index 저장할 변수
    /// </summary>
    uint old;

    Stat_1 stat1;
    Stat_2 stat2;
    Stat_3 stat3;

    Button closeButton;


    private void Awake()
    {
        Transform child = transform.GetChild(0);
        invenSlotUI = child.GetComponentsInChildren<InvenSlotUI>();

        child = transform.GetChild(3);
        Transform grandChild = child.transform.GetChild(0);
        equipSlotsUI1 = grandChild.GetComponentsInChildren<InvenSlotUI>();
        grandChild = child.transform.GetChild(1);
        equipSlotsUI2 = grandChild.GetComponentsInChildren<InvenSlotUI>();
        grandChild = child.transform.GetChild(2);
        equipSlotsUI3 = grandChild.GetComponentsInChildren<InvenSlotUI>();

        equipSlotsUI1[0].equipType = EquipType.Gun;      // 장비 슬롯별로 타입 하나씩 지정
        equipSlotsUI1[1].equipType = EquipType.Armor;
        equipSlotsUI1[2].equipType = EquipType.Pants;
        equipSlotsUI2[0].equipType = EquipType.Hammer;
        equipSlotsUI2[1].equipType = EquipType.Armor;
        equipSlotsUI2[2].equipType = EquipType.Pants;
        equipSlotsUI3[0].equipType = EquipType.LSword;
        equipSlotsUI3[1].equipType = EquipType.Armor;
        equipSlotsUI3[2].equipType = EquipType.Pants;

        equipSlotsUI = equipSlotsUI1.Concat(equipSlotsUI2).Concat(equipSlotsUI3).ToArray();

        slotsUI = invenSlotUI.Concat(equipSlotsUI).ToArray();


        child = transform.GetChild(1);
        closeButton = child.GetComponent<Button>();
        closeButton.onClick.AddListener(Close);

        //  아이템 강화 및 판매
        //  buttons = new Button[2];
        //  child = transform.GetChild(2);
        //  buttons[0] = child.GetComponent<Button>();
        //  buttons[0].onClick.AddListener(Close);
        //  
        //  child = transform.GetChild(6);
        //  buttons[1] = child.GetComponent<Button>();
        //  buttons[1].onClick.AddListener(Open);

        child = transform.GetChild(2);
        moneyPanel = child.GetComponent<MoneyPanel>();

        child = transform.GetChild(4);
        tempSlotUI = child.GetComponent<TempSlotUI>();

        child = transform.GetChild(5);
        detail = child.GetComponent<DetailInfoUI>();

        inputActions = new PlayerInputAction();

        canvasGroup = GetComponent<CanvasGroup>();

        child = transform.GetChild(0);
        Transform Gchild = child.transform.GetChild(1);

        stat1 = FindObjectOfType<Stat_1>();
        stat2 = FindObjectOfType<Stat_2>();
        stat3 = FindObjectOfType<Stat_3>();
    }

    /// <summary>
    /// 인벤토리 UI 초기화 함수
    /// </summary>
    /// <param name="playerInven">이 UI와 연결될 인벤토리</param>
    public void InitializeInventory(Inventory playerInven)
    {
        inven = playerInven;

        // 슬롯 초기화(초기화 함수 실행 및 델리게이트 연결하기)
        for (uint i = 0; i < slotsUI.Length; i++)
        {
            slotsUI[i].InitializeSlot(inven[i]);
            slotsUI[i].onDragBegin += OnItemMoveBegin;
            slotsUI[i].onDragEnd += OnItemMoveEnd;
            slotsUI[i].onClick += OnSlotClick;
            slotsUI[i].onItemLeftClick += OnItemDetailClickOn;
            slotsUI[i].onItemRightClick += OnItemDetailClickOff;
            slotsUI[i].onPointerEnter += OnItemDetailOn;
            slotsUI[i].onPointerExit += OnItemDetailOff;
        }

        // 임시 슬롯 초기화
        tempSlotUI.InitializeSlot(inven.TempSlot);
        tempSlotUI.onTempSlotOpenClose += OnDetailPause;
        
        // 상세 정보창 닫아 놓기
        detail.Close();
        
        
        // 오너와 머니 패널 연결하기
        Owner.onMoneyChange += moneyPanel.Refresh;
        moneyPanel.Refresh(Owner.Money);
        
        
        //시작할 때 인벤토리 닫은채로 시작
        Close();
        
    }

    /// <summary>
    /// 슬롯UI에서 드래그가 시작되면 실행될 함수
    /// </summary>
    /// <param name="index">드래그가 시작된 슬롯의 인덱스</param>
    private void OnItemMoveBegin(uint index)
    {
        inven.MoveItem(index, tempSlotUI.Index);    // 시작 슬롯에서 임시 슬롯으로 아이템 옮기기
        InvenSlotUI.dragStartSlotIndex = index;
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
        if (30 <= finalIndex && finalIndex <= 38)
        {
            if (Inventory.invenSlot.ItemData.equipPart == slotsUI[index].equipType)
            {
                inven.MoveItem(tempSlotUI.Index, finalIndex);   // 임시 슬롯에서 장비 슬롯으로 아이템 옮기기
                stat1.Status();     // 이동 끝났으면 1번 캐릭터의 스텟 갱신
                stat2.Status();     // 이동 끝났으면 2번 캐릭터의 스텟 갱신
                stat3.Status();     // 이동 끝났으면 3번 캐릭터의 스텟 갱신
            }
            else
            {
                // 임시슬롯에 있는 아이템 타입 =! 마우스 위치 슬롯의 지정타입이라면(장착 실패)
                finalIndex = InvenSlotUI.dragStartSlotIndex;    // 드래그를 시작한 위치로
                inven.MoveItem(tempSlotUI.Index, finalIndex);   // 임시 슬롯에서 인벤 슬롯으로 아이템 옮기기(원래 자리로 돌아감)
            }
        }
        else
        {
            inven.MoveItem(tempSlotUI.Index, finalIndex);   // 임시 슬롯에서 도착 슬롯으로 아이템 옮기기
            stat1.Status();
            stat2.Status();
            stat3.Status();
        }

        if (tempSlotUI.InvenSlot.IsEmpty)               // 비었다면(같은 종류의 아이템일 때 일부만 들어가는 경우가 있을 수 있으므로)
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
        if (mousePoint == true)
        {
            detail.Open(slotsUI[index].InvenSlot.ItemData); // 상세정보창 열기
        }
        else if (mousePoint == false)
        {
            detail.Close(); // 상세정보창 닫기
        }
    }

    private void OnItemDetailClickOff(uint index)
    {
        detail.Close();
    }

    /// <summary>
    /// 마우스 포인터가 슬롯위로 올라왔을 때 실행되는 함수
    /// </summary>
    /// <param name="index"></param>
    private void OnItemDetailOn(uint index)
    {
        mousePoint = true;
    }

    /// <summary>
    /// 마우스 포인터가 슬롯위에서 나갔을 때 실행되는 함수
    /// </summary>
    /// <param name="index">나간 슬롯의 인덱스</param>
    private void OnItemDetailOff(uint index)
    {
        mousePoint = false;
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
