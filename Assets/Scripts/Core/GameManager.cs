using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// PlayerBase 배열
    /// </summary>
    PlayerBase[] playerBase;

    /// <summary>
    /// EnemyBase 배열
    /// </summary>
    EnemyBase[] enemyBase;

    ItemCode itemcode;

    /// <summary>
    /// 플레이어
    /// </summary>
    PlayerBase player;
    public PlayerBase Player => player;

    /// <summary>
    /// 아이템 데이터 메니저
    /// </summary>
    ItemDataManager itemDataManager;
    public ItemDataManager ItemData => itemDataManager;

    /// <summary>
    /// 플레이어의 인벤토리
    /// </summary>
    Inventory inven;

    /// <summary>
    /// 플레이어의 장착인벤
    /// </summary>
    Equipment equip;

    /// <summary>
    /// 인벤토리 확인용 프로퍼티
    /// </summary>
    public Inventory Inventory => inven;

    /// <summary>
    /// 장비 아이템의 부위별 장비 상태(장착한 아이템이 있는 슬롯을 가지고 있음)
    /// </summary>
    public InvenSlot[] partsSlot;

    /// <summary>
    /// 장비 아이템의 부위별 슬롯 확인용 인덱서
    /// </summary>
    /// <param name="part">확인할 장비의 종류</param>
    /// <returns>null이면 장비가 안되어있음. null이 아니면 그 슬롯에 들어있는 아이템이 장비되어 있음</returns>
    public InvenSlot this[EquipType part] => partsSlot[(int)part];

    /// <summary>
    /// 인벤토리 UI
    /// </summary>
    InventoryUI inventoryUI;
    public InventoryUI InvenUI => inventoryUI;

    /// <summary>
    /// 장착인벤 UI
    /// </summary>
    EquipmentUI equipmentUI;
    public EquipmentUI EquipUI => equipmentUI;

    /// <summary>
    /// 플레이어가 가지고 있는 금액
    /// </summary>
    int money = 0;

    /// <summary>
    /// 플레이어가 가지고 있는 금액 확인 및 설정용 프로퍼티
    /// </summary>
    public int Money
    {
        get => money;
        set
        {
            if (money != value)  // 금액이 변경되었을 때만
            {
                money = value;  // 수정하고
                onMoneyChange?.Invoke(money);   // 델리게이트로 알림
                Debug.Log($"Player Money : {money}");
            }
        }
    }

    /// <summary>
    /// 보유한 금액이 변경되었음을 알리는 델리게이트(파라메터:현재 보유한 금액)
    /// </summary>
    public Action<int> onMoneyChange;


    private void Awake()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
        playerBase = FindObjectsOfType<PlayerBase>();
        enemyBase = FindObjectsOfType<EnemyBase>();
        partsSlot = new InvenSlot[Enum.GetValues(typeof(EquipType)).Length];    // EquipType의 항목 개수만큼 배열 만들기
    }

    protected void Start()
    {
        inven = new Inventory(this);    // itemDataManager 설정 때문에 awake는 안됨
        equip = new Equipment(this);
        if (GameManager.Inst.InvenUI != null)
        {
            GameManager.Inst.InvenUI.InitializeInventory(inven, equip);  // 인벤토리와 인벤토리 UI연결
        }

        if (GameManager.Inst.EquipUI != null)
        {
            GameManager.Inst.EquipUI.InitializeInventory(equip, inven);  // 장비창와 장비창 UI연결

        }
    }

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>();
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        player = FindObjectOfType<PlayerBase>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        equipmentUI = FindObjectOfType<EquipmentUI>();
    }

    public void BattleResultVictory()
    {
        Debug.Log("플레이어 승리");
        // 전투에서 승리한 경우 UI 출력 및, 아이템 획득
    }

    public void BattleResultLoss()
    {
        Debug.Log("플레이어 패배");
        // 전투에서 패배한 경우 UI 출력
    }

    public void ResultGetItem()
    {
        int getItem = UnityEngine.Random.Range(0, 2);
        
        switch(getItem)
        {
            case 0:
                itemcode = ItemCode.Armor;
                break;
            case 1:
                itemcode = ItemCode.Pants;
                break;
            case 2:
                itemcode = ItemCode.SSword1;
                break;
            case 3:
                itemcode = ItemCode.SSword1;
                break;
            case 4:
                itemcode = ItemCode.LSword1;
                break;
            case 6:
                itemcode = ItemCode.LSword2;
                break;
            case 7:
                itemcode = ItemCode.SBow1;
                break;
            case 8:
                itemcode = ItemCode.SBow2;
                break;
            case 9:
                itemcode = ItemCode.LBow1;
                break;
            case 10:
                itemcode = ItemCode.LBow2;
                break;
            case 11:
                itemcode = ItemCode.Hammer1;
                break;
            case 12:
                itemcode = ItemCode.Hammer2;
                break;
            case 13:
                itemcode = ItemCode.Gun1;
                break;
            case 14:
                itemcode = ItemCode.Gun2;
                break;

        }
        inven.AddItem(itemcode);     // 즉시 소비가능한 아이템이 아니면 아이템 추가 시도
    }

}