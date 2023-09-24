using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Stat_2 : MonoBehaviour
{
    TextMeshProUGUI[] status;
    InvenSlotUI[] slots;

    int[] Str;
    int[] Agi;
    int[] Int;
    int[] HP;
    int[] MP;
    int[] Speed;


    private void Awake()
    {
        status = new TextMeshProUGUI[6];
        Str = new int[3];
        Agi = new int[3];
        Int = new int[3];
        HP = new int[3];
        MP = new int[3];
        Speed = new int[3];
        Transform child = transform.GetChild(0);
        Transform grandChild = child.transform.GetChild(0);
        status[0] = grandChild.GetComponent<TextMeshProUGUI>();    // Str Text

        child = transform.GetChild(1);
        grandChild = child.transform.GetChild(0);
        status[1] = grandChild.GetComponent<TextMeshProUGUI>();    // Agi Text

        child = transform.GetChild(2);
        grandChild = child.transform.GetChild(0);
        status[2] = grandChild.GetComponent<TextMeshProUGUI>();    // Int Text

        child = transform.GetChild(3);
        grandChild = child.transform.GetChild(0);
        status[3] = grandChild.GetComponent<TextMeshProUGUI>();    // HP Text

        child = transform.GetChild(4);
        grandChild = child.transform.GetChild(0);
        status[4] = grandChild.GetComponent<TextMeshProUGUI>();    // MP Text

        child = transform.GetChild(5);
        grandChild = child.transform.GetChild(0);
        status[5] = grandChild.GetComponent<TextMeshProUGUI>();    // Speed Text

        slots = new InvenSlotUI[3];  // 장비 슬롯 찾기
        Transform parent = transform.parent;
        child = parent.transform.GetChild(2);
        slots[0] = child.GetComponent<InvenSlotUI>();  // 2번 캐릭터 무기

        child = parent.transform.GetChild(3);
        slots[1] = child.GetComponent<InvenSlotUI>();  // 2번 캐릭터 상의

        child = parent.transform.GetChild(4);
        slots[2] = child.GetComponent<InvenSlotUI>();  // 2번 캐릭터 하의
    }

    private void Update()
    {
        status[0].text = $"Str {(Str[0] + Str[1] + Str[2]).ToString()}";
        status[1].text = $"Agi {(Agi[0] + Agi[1] + Agi[2]).ToString()}";
        status[2].text = $"Int {(Int[0] + Int[1] + Int[2]).ToString()}";
        status[3].text = $"HP {(HP[0] + HP[1] + HP[2]).ToString()}";
        status[4].text = $"MP {(MP[0] + MP[1] + MP[2]).ToString()}";
        status[5].text = $"Speed {(Speed[0] + Speed[1] + Speed[2]).ToString()}";
    }

    public void Status()
    {
        for (int i = 0; i < 3; i++)
        {
            if (slots[i].InvenSlot.ItemData != null)
            {
                Str[i] = slots[i].InvenSlot.ItemData.beforeStr;
                Agi[i] = slots[i].InvenSlot.ItemData.beforeAgi;
                Int[i] = slots[i].InvenSlot.ItemData.beforeInt;
                HP[i] = slots[i].InvenSlot.ItemData.beforeHP;
                MP[i] = slots[i].InvenSlot.ItemData.beforeMP;
                Speed[i] = slots[i].InvenSlot.ItemData.speed;
            }
            else if (slots[i].InvenSlot.ItemData == null)
            {
                Str[i] = 0;
                Agi[i] = 0;
                Int[i] = 0;
                HP[i] = 0;
                MP[i] = 0;
                Speed[i] = 0;
            }
        }
    }
}
