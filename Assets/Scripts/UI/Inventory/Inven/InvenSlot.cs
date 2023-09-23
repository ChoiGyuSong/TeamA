using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenSlot
{
    /// <summary>
    /// �κ��丮������ �ε���
    /// </summary>
    uint slotIndex;

    /// <summary>
    /// �κ��丮������ �ε����� Ȯ���ϱ� ���� ������Ƽ
    /// </summary>
    public uint Index => slotIndex;

    /// <summary>
    /// �� ���Կ� ����ִ� �������� ����
    /// </summary>
    public ItemData slotItemData = null;

    public ItemData tempData ;

    /// <summary>
    /// �� ���Կ� ����ִ� �������� ������ Ȯ���ϱ� ���� ������Ƽ(����� private)
    /// </summary>
    public ItemData ItemData
    {
        get => slotItemData;
        private set
        {
            if (slotItemData != value)      // ������ ����� ����
            {
                slotItemData = value;       // ���� �۾� ó��
                tempData = value;
                onSlotItemChange?.Invoke(); // ������ ������ ����Ǿ��ٰ� �˶� ������
            }   
        }
    }

    /// <summary>
    /// ���Կ� ����ִ� �������� ����, ����, ��� ���ΰ� ����Ǿ��ٰ� �˸��� ��������Ʈ
    /// </summary>
    public Action onSlotItemChange;

    /// <summary>
    /// ���Կ� �������� �ִ��� ������ Ȯ���ϴ� ������Ƽ(true�� ����ְ�, false�� �������� ����ִ�.)
    /// </summary>
    public bool IsEmpty => slotItemData == null;

    /// <summary>
    /// �� ������ �������� ���Ǿ����� ����
    /// </summary>
    bool isEquipped = false;

    /// <summary>
    /// �� ������ ��񿩺θ� Ȯ���ϱ� ���� ������Ƽ
    /// </summary>
    public bool IsEquipped
    {
        get => isEquipped;
        set
        {
            isEquipped = value;
            onSlotItemChange?.Invoke(); // ������ ����Ǿ��ٰ� �˸�
        }
    }

    public event Action<ItemData> TempDataChanged;

    private ItemData _tempData;
    public ItemData TempData
    {
        get { return _tempData; }
        set
        {
            _tempData = value;
            TempDataChanged?.Invoke(_tempData);
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="index">�� ������ �ε���(�κ��丮���� ���° ��������)</param>
    public InvenSlot(uint index)
    {
        slotIndex = index;      // slotIndex�� ���ķ� ���� ���ϸ� �ȵȴ�.
        IsEquipped = false;
    }

    /// <summary>
    /// �� ���Կ� �������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="data">������ ������ ����</param>
    public void AssignSlotItem(ItemData data)
    {
        if (data != null)
        {
            ItemData = data;
            IsEquipped = false;
            //Debug.Log($"�κ��丮 {slotIndex}�� ���Կ� \"{ItemData.itemName}\" �������� {ItemCount}�� ����");
        }
        else
        {
            ClearSlotItem();    // data�� null�̸� �ش� ������ �ʱ�ȭ
        }
    }

    /// <summary>
    /// �� ������ ���� �Լ�
    /// </summary>
    public void ClearSlotItem()
    {
        ItemData = null;
        IsEquipped = false;
        //Debug.Log($"�κ��丮 {slotIndex}�� ������ ���ϴ�.");
    }
}
