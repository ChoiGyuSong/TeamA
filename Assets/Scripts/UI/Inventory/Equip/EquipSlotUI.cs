using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// �κ��丮�� ���Կ� UI.
/// </summary>
public class EquipSlotUI : SlotUI_Base, IDragHandler, IBeginDragHandler, IEndDragHandler,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{

    TempSlotUI tempSlotUI;

    /// <summary>
    /// �巡�� ������ �˸��� ��������Ʈ. �Ķ���ʹ� �巡�� ������ ������ �ε���
    /// </summary>
    public Action<uint> onDragBegin;

    /// <summary>
    /// �巡�� ���Ḧ �˸��� ��������Ʈ. �Ķ���ʹ� �巡�װ� ���� ������ �ε����� �巡�װ� ���Կ��� �������� �˸��� bool(���Կ��� �������� true)
    /// </summary>
    public Action<uint, bool> onDragEnd;

    /// <summary>
    /// ���Կ� Ŭ���� �־��ٰ� �˸��� ��������Ʈ. �Ķ���ʹ� Ŭ���� ������ �ε���
    /// </summary>
    public Action<uint> onClick;

    /// <summary>
    /// ���콺 �����Ͱ� ���� ������ ���Դٰ� �˸��� ��������Ʈ. �Ķ���ʹ� ��� ������ �ε���
    /// </summary>
    public Action<uint> onPointerEnter;

    /// <summary>
    /// ���콺 �����Ͱ� ���� ������ �����ٰ� �˸��� ��������Ʈ. �Ķ���ʹ� ��� ������ �ε���
    /// </summary>
    public Action<uint> onPointerExit;

    public Action<uint> onItemLeftClick;
    public Action<uint> onItemRightClick;


    /// <summary>
    /// ���콺 �����Ͱ� ���� ������ �����δٰ� �˸��� ��������Ʈ. �Ķ���ʹ� ���콺 �������� ��ũ�� ��ǥ
    /// </summary>
    public Action<Vector2> onPointerMove;

    public bool ifDragStartInven = false;

    public static uint dragStartSlotIndex;

    public EquipType equipType;

    protected override void Awake()
    {
        base.Awake();
        tempSlotUI = FindObjectOfType<TempSlotUI>();

        
    }

    /// <summary>
    /// �� ���� UI�� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    /// <param name="slot">�� UI�� ����� ����</param>
    public override void EInitializeSlot(EquipSlot slot)
    {
        // ��������Ʈ�� �ʱ�ȭ
        onDragBegin = null;
        onDragEnd = null;
        onClick = null;
        onPointerEnter = null;  
        onPointerExit = null;

        onItemLeftClick = null;

        onPointerMove = null;

        base.EInitializeSlot(slot);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // OnBeginDrag�� OnEndDrag�� �ߵ���Ű�� ���� �߰��� ��. 
        // ���� �����ϴ� �ڵ� ����
    }

    /// <summary>
    /// �巡�װ� ���۵� �� ����� �Լ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        if (obj == this.gameObject)
        {
            onDragBegin?.Invoke(EIndex);     // ��������Ʈ�� �˶� ������
        }
    }

    /// <summary>
    /// �巡�װ� ����� �� ����� �Լ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        // �巡�װ� ���������� Ȯ��
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;    // ���콺 �ִ� ��ġ�� ���� ������Ʈ�� �ִ��� Ȯ��

        if (obj != null)
        {
            // ���콺 ��ġ�� � ������Ʈ�� �ִ�.
            InvenSlotUI invenSlot = obj.GetComponent<InvenSlotUI>();  // ���콺 ��ġ�� �ִ� ������Ʈ�� ����UI���� Ȯ��
            EquipSlotUI equipSlot = obj.GetComponent<EquipSlotUI>();    // ���콺 ��ġ�� �ִ� ������Ʈ�� ��� ����UI ���� Ȯ��
            if (invenSlot != null)
            {
                // ����UI��.
                invenSlot.onDragEnd?.Invoke(invenSlot.Index, true); // ���������� �ִ� ������ �ε����� ���������� �����ٰ� �˶� ������
            }
            else if(equipSlot != null)
            {
                // ��� ���� UI��
                onDragEnd?.Invoke(equipSlot.EIndex, true);
            }
            else
            {
                Debug.Log("��� �Ǵ� �κ� ����UI�� �ƴϴ�.");
                onDragEnd?.Invoke(EIndex, false);
            }
        }
        else
        {
            Debug.Log("����UI�� �ƴϴ�.");
            onDragEnd?.Invoke(EIndex, false);        // ���� �巡�װ� ������ �ε����� ������������ �����ٰ� �˶� ������
        }
    }

    /// <summary>
    /// ����UI�� ���콺�� Ŭ���Ǿ��� �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if(tempSlotUI.tempSlot)
        {
            onClick?.Invoke(EIndex);
        }
        else if(!tempSlotUI.tempSlot)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                onItemLeftClick?.Invoke(EIndex);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                onItemRightClick?.Invoke(EIndex);
            }
        }
    }

    /// <summary>
    /// ����UI�� ���콺�� ������ �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    /// <summary>
    /// ����UI�� ���콺�� ������ �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
    }

    /// <summary>
    /// ����UI���� ���콺�� ������ �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerMove(PointerEventData eventData)
    {
    }
}
