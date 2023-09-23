using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary>
/// ����� �κ��丮(UI ����)
/// </summary>
public class Equipment
{
    /// <summary>
    /// �ӽý��Կ� �ε���
    /// </summary>
    public const uint TempSlotIndex = 999999999;

    /// <summary>
    /// �κ��丮 ������ �迭
    /// </summary>
    InvenSlot[] slots;

    EquipSlot[] equipSlots;

    /// <summary>
    /// �κ��丮 ���Կ� �����ϱ� ���� �ε���
    /// </summary>
    /// <param name="index">������ �ε���</param>
    /// <returns>����</returns>
    public EquipSlot this[uint index] => equipSlots[index];

    /// <summary>
    /// �κ��丮 ������ ����
    /// </summary>
    public int SlotCount => equipSlots.Length;

    /// <summary>
    /// �ӽ� ����(�巡�׳� ������ �и��۾��� �� �� ���)
    /// </summary>
    InvenSlot tempSlot;
    public InvenSlot TempSlot => tempSlot;

    public EquipSlot EtempSlot;
    public EquipSlot ETempSlot => EtempSlot;

    public static EquipSlot equipSlot;

    /// <summary>
    /// ������ ������ �޴���(������ ������ �����͸� Ȯ���� �� �ִ�.)
    /// </summary>
    ItemDataManager itemDataManager;

    /// <summary>
    /// �κ��丮 ������(���� �Ŵ���)
    /// </summary>
    GameManager owner;
    public GameManager Owner => owner;

    Inventory inven;

    /// <summary>
    /// �κ��丮, ���â�� ���� �� �ʱ�ȭ
    /// </summary>
    /// <param name="owner">�κ��丮 ������</param>
    /// <param name="size">�κ��丮�� ũ��</param>
    public Equipment(GameManager owner, uint size = 9)
    {
        inven = new Inventory(owner);

        slots = new InvenSlot[30];
        for (uint i = 0; i < size; i++)
        {
            slots[i] = new InvenSlot(i);                // ���� ���� ����
        }

        equipSlots = new EquipSlot[size];
        for (uint i = 0; i < size; i++)
        {
            equipSlots[i] = new EquipSlot(i);                // ���� ���� ����
        }

        EtempSlot = new EquipSlot(TempSlotIndex);
        tempSlot = new InvenSlot(TempSlotIndex);

        itemDataManager = GameManager.Inst.ItemData;    // ������ ������ �޴��� ĳ��
        this.owner = owner;                             // ������ ���
    }

    /// <summary>
    /// �κ��丮�� �������� from��ġ���� to��ġ�� �������� �̵���Ű�� �Լ�
    /// </summary>
    /// <param name="from">��ġ ������ ���۵Ǵ� �ε���</param>
    /// <param name="to">��ġ ������ ������ �ε���</param>
    public void MoveItem(uint from, uint to)
    {
        // from������ to������ �ٸ��� from�� to�� ��� valid�ؾ� �Ѵ�.
        if ((from != to) && IsValidIndex(from) && IsValidIndex(to))
        {
            // from�� TempSlotIndex(�ӽý���)�� �����ϸ� fromSlot�� ETempSlot(���â���� ������� �ӽý���), �������� �ʴٸ� ���â�� ��� ����
            EquipSlot fromSlot = (from == TempSlotIndex) ? ETempSlot : equipSlots[from];

            // tempSlot�� TempSlotIndex(�ӽý���)�� �����ϸ� fromSlot�� Inventroy.invenSlot(�κ��丮���� ������� �ӽý���), �������� ������ �κ��丮�� ��߽���
            tempSlot = (from == TempSlotIndex) ? Inventory.invenSlot : slots[from];

            if (!fromSlot.IsEmpty && tempSlot.IsEmpty)  // ���â���� ���� �ӽý��� or ��߽����� �����ϰ�, �κ��丮���� ������� �ӽý��� or ��߽����� ����ִ�
            {
                EquipSlot EtoSlot = (to == TempSlotIndex) ? ETempSlot : equipSlots[to];     // to�� �ӽý����̶��
                                                                                               
                if (EtoSlot != null)
                {
                    ItemData tempData = fromSlot.ItemData;      // ������ ������ ����
                    fromSlot.AssignSlotItem(EtoSlot.ItemData);  // fromSlotd�� �������� EtoSlot�� ����
                    EtoSlot.AssignSlotItem(tempData);           // EtoSlot�� �������� tempData�� ����

                    equipSlot = EtoSlot;    // �������� equipSlot�� EtoSlot������ ����(�κ��丮���� �̵���)
                }
            }

            else if(!tempSlot.IsEmpty && fromSlot.IsEmpty)  // �κ��丮���� ������� �ӽý����� �����ϰ�, ���â���� ���� �ӽý����� ����ִ�
            {
                EquipSlot EtoSlot = (to == TempSlotIndex) ? ETempSlot : equipSlots[to];

                if(EtoSlot != null)
                {
                    // �ٸ� ������ �������̸� ���� ����
                    ItemData tempData = tempSlot.ItemData;
                    tempSlot.AssignSlotItem(EtoSlot.ItemData);
                    EtoSlot.AssignSlotItem(tempData);
                }
            }
        }
    }

    /// <summary>
    /// ������ �ε������� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="index">Ȯ���� �ε���</param>
    /// <returns>true�� ������ �ε���, false�� ���� �ε���</returns>
    bool IsValidIndex(uint index) => (index < SlotCount) || (index == TempSlotIndex) || (index == TempSlotIndex);

}
