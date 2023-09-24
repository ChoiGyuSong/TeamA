using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// �κ��丮�� ���Կ� UI.
/// </summary>
public class InvenSlotUI : SlotUI_Base, IDragHandler, IBeginDragHandler, IEndDragHandler,
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

    public EquipType equipType;

    public static uint dragStartSlotIndex;

    /// <summary>
    /// ���콺 �����Ͱ� ���� ������ �����δٰ� �˸��� ��������Ʈ. �Ķ���ʹ� ���콺 �������� ��ũ�� ��ǥ
    /// </summary>
    public Action<Vector2> onPointerMove;

    public bool ifDragStartInven = false;

    protected override void Awake()
    {
        base.Awake();
        tempSlotUI = FindObjectOfType<TempSlotUI>();
    }

    /// <summary>
    /// �� ���� UI�� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    /// <param name="slot">�� UI�� ����� ����</param>
    public override void InitializeSlot(InvenSlot slot)
    {
        // ��������Ʈ�� �ʱ�ȭ
        onDragBegin = null;
        onDragEnd = null;
        onClick = null;
        onPointerEnter = null;
        onPointerExit = null;

        onItemLeftClick = null;

        onPointerMove = null;

        base.InitializeSlot(slot);
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
            onDragBegin?.Invoke(Index);     // ��������Ʈ�� �˶� ������
        }
    }

    /// <summary>
    /// �巡�װ� ����� �� ����� �Լ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        // �巡�װ� ���������� Ȯ��
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;    // ���콺 �ִ� ��ġ�� ���� ������Ʈ�� �ִ���
        if (obj != null)
        {
            // ���콺 ��ġ�� � ������Ʈ�� �ִ�.
            InvenSlotUI endSlot = obj.GetComponent<InvenSlotUI>();  // ���콺 ��ġ�� �ִ� ������Ʈ�� ����UI���� Ȯ��

            if (endSlot != null)
            {
                // ����UI��.
                onDragEnd?.Invoke(endSlot.Index, true); // ���������� �ִ� ������ �ε����� ���������� �����ٰ� �˶� ������
            }
            else
            {
                Debug.Log("��� �Ǵ� �κ� ����UI�� �ƴϴ�.");
                onDragEnd?.Invoke(Index, false);
            }
        }
        else
        {
            Debug.Log("����UI�� �ƴϴ�.");
            onDragEnd?.Invoke(Index, false);        // ���� �巡�װ� ������ �ε����� ������������ �����ٰ� �˶� ������
        }
    }

    /// <summary>
    /// ����UI�� ���콺�� Ŭ���Ǿ��� �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"�ε��� ��ȣ : {this.Index}");
        if(tempSlotUI.tempSlot)
        {
            onClick?.Invoke(Index);
        }
        else if(!tempSlotUI.tempSlot)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                onItemLeftClick?.Invoke(Index);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                onItemRightClick?.Invoke(Index);
            }
        }
    }

    /// <summary>
    /// ����UI�� ���콺�� ������ �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke(Index);
    }

    /// <summary>
    /// ����UI�� ���콺�� ������ �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit?.Invoke(Index);
    }

    /// <summary>
    /// ����UI���� ���콺�� ������ �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerMove(PointerEventData eventData)
    {
        onPointerMove?.Invoke(eventData.position);
    }
}
