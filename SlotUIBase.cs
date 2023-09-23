using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI_Base : MonoBehaviour
{
    /// <summary>
    /// �� UI�� ǥ���� ����(����� ����)
    /// </summary>
    InvenSlot invenSlot;

    EquipSlot equipSlot;

    /// <summary>
    /// ���� Ȯ�ο� ������Ƽ
    /// </summary>
    public InvenSlot InvenSlot => invenSlot;

    public EquipSlot EquipSlot => equipSlot;

    /// <summary>
    /// ������ ���° �������� Ȯ���ϱ� ���� ������Ƽ
    /// </summary>
    public uint Index => invenSlot.Index;

    public uint EIndex => equipSlot.Index;


    /// <summary>
    /// ������ ������ ǥ�ÿ� �̹���
    /// </summary>
    Image itemIcon;


    protected virtual void Awake()
    {
        // ��ӹ��� Ŭ�������� �߰����� �ʱ�ȭ�� �ʿ��ϱ� ������ �����Լ��� ����
        Transform child = transform.GetChild(0);
        itemIcon = child.GetComponent<Image>();
    }

    /// <summary>
    /// ���� �ʱ�ȭ�� �Լ�
    /// </summary>
    /// <param name="slot">�� UI�� ������ ����</param>
    public virtual void InitializeSlot(InvenSlot slot)
    {
        invenSlot = slot;                       // ���� ����
        invenSlot.onSlotItemChange = Refresh;   // ���Կ� ��ȭ�� ���� �� ����� �Լ� ���
        Refresh();                              // �ʱ� ��� ����
    }

    /// <summary>
    /// ��񽽷� �ʱ�ȭ�� �Լ�
    /// </summary>
    /// <param name="slot">�� UI�� ������ ����</param>
    public virtual void EInitializeSlot(EquipSlot slot)
    {
        equipSlot = slot;                       // ���� ����
        equipSlot.onSlotItemChange = ERefresh;   // ���Կ� ��ȭ�� ���� �� ����� �Լ� ���
        ERefresh();                              // �ʱ� ��� ����
    }

    /// <summary>
    /// ������ ���̴� ����� �����ϴ� �Լ�
    /// </summary>
    private void Refresh()
    {
        if (InvenSlot.IsEmpty)
        {
            // ���������
            itemIcon.color = Color.clear;   // ������ �Ⱥ��̰� ����ȭ
            itemIcon.sprite = null;         // �����ܿ��� �̹��� ����
        }
        else
        {
            // �������� ���������
            itemIcon.sprite = InvenSlot.ItemData.itemIcon;      // �����ܿ� �̹��� ����
            itemIcon.color = Color.white;                       // �������� ���̵��� ���� ����
        }

        OnRefresh();        // ��ӹ��� Ŭ�������� ������ �����ϰ� ���� �ڵ� ����
    }

    /// <summary>
    /// ������ ���̴� ����� �����ϴ� �Լ�
    /// </summary>
    private void ERefresh()
    {
        if (EquipSlot.IsEmpty)
        {
            // ���������
            itemIcon.color = Color.clear;   // ������ �Ⱥ��̰� ����ȭ
            itemIcon.sprite = null;         // �����ܿ��� �̹��� ����
        }
        else
        {
            // �������� ���������
            itemIcon.sprite = InvenSlot.ItemData.itemIcon;      // �����ܿ� �̹��� ����
            itemIcon.color = Color.white;                       // �������� ���̵��� ���� ����
        }

        OnRefresh();        // ��ӹ��� Ŭ�������� ������ �����ϰ� ���� �ڵ� ����
    }

    /// <summary>
    /// ��ӹ��� Ŭ�������� ���������� �����ϰ� ���� �ڵ带 ��Ƴ��� �Լ�
    /// </summary>
    protected virtual void OnRefresh()
    {
    }
}

