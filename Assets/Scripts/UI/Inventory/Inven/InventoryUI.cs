using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    /// <summary>
    /// �� UI�� ������ �κ��丮 
    /// </summary>
    Inventory inven;

    Equipment equip;

    InvenSlot tempSlot;

    /// <summary>
    /// �� �κ��丮�� ������ �ִ� ��� ������ UI
    /// </summary>
    InvenSlotUI[] slotsUI;

    /// <summary>
    /// ���â�� ����UI
    /// </summary>
    EquipSlotUI[] equipSlotsUI;

    /// <summary>
    /// ������ �̵��̳� �и��� �� ����� �ӽ� ���� UI
    /// </summary>
    TempSlotUI tempSlotUI;

    /// <summary>
    /// �������� �������� ǥ���ϴ� �г�
    /// </summary>
    DetailInfoUI detail;

    /// <summary>
    /// �����ڰ� ������ �ִ� �ݾ��� �����ִ� �г�
    /// </summary>
    MoneyPanel moneyPanel;

    /// <summary>
    /// �� �κ��丮�� �����ڸ� Ȯ���ϱ� ���� ������Ƽ
    /// </summary>
    public GameManager Owner => inven.Owner;

    /// <summary>
    /// ��ǲ �׼�
    /// </summary>
    PlayerInputAction inputActions;

    CanvasGroup canvasGroup;

    public Action detailOpen;
    public Action detailClose;

    public Action onInventoryOpen;
    public Action onInventoryClose;

    bool mousePoint;

    /// <summary>
    /// ������ â ǥ�ý� index ������ ����
    /// </summary>
    uint old;

    Stat_1 stat1;
    Stat_2 stat2;
    Stat_3 stat3;

    Button closeButton;


    private void Awake()
    {
        Transform child = transform.GetChild(0);
        slotsUI = child.GetComponentsInChildren<InvenSlotUI>();

        equipSlotsUI = FindObjectsOfType<EquipSlotUI>();

        child = transform.GetChild(1);
        closeButton = child.GetComponent<Button>();
        closeButton.onClick.AddListener(Close);

        //  ������ ��ȭ �� �Ǹ�
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
    /// �κ��丮 UI �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="playerInven">�� UI�� ����� �κ��丮</param>
    public void InitializeInventory(Inventory playerInven, Equipment equip)
    {
        inven = playerInven;

        // ���� �ʱ�ȭ(�ʱ�ȭ �Լ� ���� �� ��������Ʈ �����ϱ�)
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

            //  slotsUI[i].onItemClick += OnItemDetailOn;
            //  slotsUI[i].onItemRightClick += OnItemDetailOff;
        }

        // �ӽ� ���� �ʱ�ȭ
        tempSlotUI.InitializeSlot(inven.TempSlot);
        tempSlotUI.onTempSlotOpenClose += OnDetailPause;
        
        // �� ����â �ݾ� ����
        detail.Close();
        
        
        // ���ʿ� �Ӵ� �г� �����ϱ�
        Owner.onMoneyChange += moneyPanel.Refresh;
        moneyPanel.Refresh(Owner.Money);
        
        
        //������ �� �κ��丮 ����ä�� ����
        //Close();
        
    }

    /// <summary>
    /// ����UI���� �巡�װ� ���۵Ǹ� ����� �Լ�
    /// </summary>
    /// <param name="index">�巡�װ� ���۵� ������ �ε���</param>
    private void OnItemMoveBegin(uint index)
    {
        inven.MoveItem(index, tempSlotUI.Index);    // ���� ���Կ��� �ӽ� �������� ������ �ű��
        EquipSlotUI.dragStartSlotIndex = index;
        tempSlotUI.Open();                          // �ӽ� ���� ����
    }

    /// <summary>
    /// ����UI���� �巡�װ� ������ �� ����� �Լ�
    /// </summary>
    /// <param name="index">�巡�װ� ���� ������ �ε���</param>
    /// <param name="isSuccess">�巡�װ� ���������� ����</param>
    private void OnItemMoveEnd(uint index, bool isSuccess)
    {
        uint finalIndex = index;                        // ���� ����� �ε���(�⺻�����δ� �Ķ���ͷ� ���� �ε���)
        if (!isSuccess)
        {
            finalIndex = EquipSlotUI.dragStartSlotIndex;
        }

        inven.MoveItem(tempSlotUI.Index, finalIndex);   // �ӽ� ���Կ��� ���� �������� ������ �ű��
        stat1.Status();
        stat2.Status();
        stat3.Status();
        if (tempSlotUI.InvenSlot.IsEmpty)               // ����ٸ�(���� ������ �������� �� �Ϻθ� ���� ��찡 ���� �� �����Ƿ�)
        {
            tempSlotUI.Close();                         // �ӽ� ���� �ݱ�
        }
    }

    /// <summary>
    /// ���콺 �����Ͱ� ���������� Ŭ���Ǿ����� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="index">�ö� ������ �ε���</param>
    private void OnItemDetailClickOn(uint index)
    {
        if (mousePoint == true)
        {
            detail.Open(slotsUI[index].InvenSlot.ItemData); // ������â ����
        }
        else if (mousePoint == false)
        {
            detail.Close(); // ������â �ݱ�
        }
    }

    private void OnItemDetailClickOff(uint index)
    {
        detail.Close();
    }

    /// <summary>
    /// ���콺 �����Ͱ� �������� �ö���� �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="index"></param>
    private void OnItemDetailOn(uint index)
    {
        mousePoint = true;
    }

    /// <summary>
    /// ���콺 �����Ͱ� ���������� ������ �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="index">���� ������ �ε���</param>
    private void OnItemDetailOff(uint index)
    {
        mousePoint = false;
    }


    /// <summary>
    /// �ӽ� ������ ������ ������â�� �Ͻ� �����ϰ�, ������ �Ͻ� ������ Ǫ�� �Լ�
    /// </summary>
    /// <param name="isPause">true�� �Ͻ� ����, false ����</param>
    private void OnDetailPause(bool isPause)
    {
        detail.IsPause = isPause;
    }

    /// <summary>
    /// ����UI�� ���콺�� Ŭ���� �Ǿ��� �� ����� �Լ�
    /// </summary>
    /// <param name="index">Ŭ���� ������ �ε���</param>
    private void OnSlotClick(uint index)
    {
        if (!tempSlotUI.InvenSlot.IsEmpty)
        {
            // �ӽ� ���Կ� �������� ���� �� Ŭ���� �Ǿ�����
            OnItemMoveEnd(index, true); // Ŭ���� �������� ������ �̵�
        }
    }

    /// <summary>
    /// �κ��丮�� ���� �Լ�
    /// </summary>
    public void Open()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        onInventoryOpen?.Invoke();
    }

    /// <summary>
    /// �κ��丮�� ���� �Լ�
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
