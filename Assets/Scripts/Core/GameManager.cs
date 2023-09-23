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
    /// PlayerBase �迭
    /// </summary>
    PlayerBase[] playerBase;

    /// <summary>
    /// EnemyBase �迭
    /// </summary>
    EnemyBase[] enemyBase;

    ItemCode itemcode;

    /// <summary>
    /// �÷��̾�
    /// </summary>
    PlayerBase player;
    public PlayerBase Player => player;

    /// <summary>
    /// ������ ������ �޴���
    /// </summary>
    ItemDataManager itemDataManager;
    public ItemDataManager ItemData => itemDataManager;

    /// <summary>
    /// �÷��̾��� �κ��丮
    /// </summary>
    Inventory inven;

    /// <summary>
    /// �÷��̾��� �����κ�
    /// </summary>
    Equipment equip;

    /// <summary>
    /// �κ��丮 Ȯ�ο� ������Ƽ
    /// </summary>
    public Inventory Inventory => inven;

    /// <summary>
    /// ��� �������� ������ ��� ����(������ �������� �ִ� ������ ������ ����)
    /// </summary>
    public InvenSlot[] partsSlot;

    /// <summary>
    /// ��� �������� ������ ���� Ȯ�ο� �ε���
    /// </summary>
    /// <param name="part">Ȯ���� ����� ����</param>
    /// <returns>null�̸� ��� �ȵǾ�����. null�� �ƴϸ� �� ���Կ� ����ִ� �������� ���Ǿ� ����</returns>
    public InvenSlot this[EquipType part] => partsSlot[(int)part];

    /// <summary>
    /// �κ��丮 UI
    /// </summary>
    InventoryUI inventoryUI;
    public InventoryUI InvenUI => inventoryUI;

    /// <summary>
    /// �����κ� UI
    /// </summary>
    EquipmentUI equipmentUI;
    public EquipmentUI EquipUI => equipmentUI;

    /// <summary>
    /// �÷��̾ ������ �ִ� �ݾ�
    /// </summary>
    int money = 0;

    /// <summary>
    /// �÷��̾ ������ �ִ� �ݾ� Ȯ�� �� ������ ������Ƽ
    /// </summary>
    public int Money
    {
        get => money;
        set
        {
            if (money != value)  // �ݾ��� ����Ǿ��� ����
            {
                money = value;  // �����ϰ�
                onMoneyChange?.Invoke(money);   // ��������Ʈ�� �˸�
                Debug.Log($"Player Money : {money}");
            }
        }
    }

    /// <summary>
    /// ������ �ݾ��� ����Ǿ����� �˸��� ��������Ʈ(�Ķ����:���� ������ �ݾ�)
    /// </summary>
    public Action<int> onMoneyChange;


    private void Awake()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
        playerBase = FindObjectsOfType<PlayerBase>();
        enemyBase = FindObjectsOfType<EnemyBase>();
        partsSlot = new InvenSlot[Enum.GetValues(typeof(EquipType)).Length];    // EquipType�� �׸� ������ŭ �迭 �����
    }

    protected void Start()
    {
        inven = new Inventory(this);    // itemDataManager ���� ������ awake�� �ȵ�
        equip = new Equipment(this);
        if (GameManager.Inst.InvenUI != null)
        {
            GameManager.Inst.InvenUI.InitializeInventory(inven, equip);  // �κ��丮�� �κ��丮 UI����
        }

        if (GameManager.Inst.EquipUI != null)
        {
            GameManager.Inst.EquipUI.InitializeInventory(equip, inven);  // ���â�� ���â UI����

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
        Debug.Log("�÷��̾� �¸�");
        // �������� �¸��� ��� UI ��� ��, ������ ȹ��
    }

    public void BattleResultLoss()
    {
        Debug.Log("�÷��̾� �й�");
        // �������� �й��� ��� UI ���
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
        inven.AddItem(itemcode);     // ��� �Һ񰡴��� �������� �ƴϸ� ������ �߰� �õ�
    }

}