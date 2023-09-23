using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.GridLayoutGroup;

public class TempSlotUI : SlotUI_Base
{
    /// <summary>
    /// 이 인벤토리를 가진 플레이어(아이템 드랍 때문에 필요)
    /// </summary>
    GameManager owner;

    /// <summary>
    /// 임시 슬롯이 열리고 닫힐 때 실행되는 함수
    /// </summary>
    public Action<bool> onTempSlotOpenClose;

    public bool tempSlot = false;

    private void Update()
    {
        // 임시 슬롯은 대부분 꺼져 있을 거라 부담이 적음
        transform.position = Mouse.current.position.ReadValue();    // 임시 슬롯은 마우스 위치를 따라 움직임
    }

    /// <summary>
    /// 임시 슬롯 초기화하는 함수
    /// </summary>
    /// <param name="slot">이 임시 슬롯과 연결된 인벤 슬롯</param>
    public override void InitializeSlot(InvenSlot slot)
    {
        onTempSlotOpenClose = null;                 // 델리게이트 초기화

        base.InitializeSlot(slot);

        owner = GameManager.Inst.InvenUI.Owner;     // 오너 미리 가지고 있기

        Close();                                    // 시작할 때 자동으로 닫히기
    }

    /// <summary>
    /// 임시 슬롯 초기화하는 함수
    /// </summary>
    /// <param name="slot">이 임시 슬롯과 연결된 인벤 슬롯</param>
    public override void EInitializeSlot(EquipSlot slot)
    {
        onTempSlotOpenClose = null;                 // 델리게이트 초기화

        base.EInitializeSlot(slot);

        owner = GameManager.Inst.InvenUI.Owner;     // 오너 미리 가지고 있기

        Close();                                    // 시작할 때 자동으로 닫히기
    }

    /// <summary>
    /// 임시 슬롯을 여는 함수
    /// </summary>
    public void Open()
    {
        transform.position = Mouse.current.position.ReadValue();    // 위치를 마우스 위치로 조정
        onTempSlotOpenClose?.Invoke(true);                          // 열렸다고 신호 보내고
        gameObject.SetActive(true);                                 // 활성화 시키기(보이게 만들기)
        tempSlot = true;
    }

    /// <summary>
    /// 임시 슬롯을 닫는 함수
    /// </summary>
    public void Close()
    {
        onTempSlotOpenClose?.Invoke(false);     // 닫혔다고 신호 보내고
        gameObject.SetActive(false);            // 비활성화 시키기(안보이게 만들기)
        tempSlot = false;
    }
}
