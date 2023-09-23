using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPanel : MonoBehaviour
{
    // Player�� ������ Money�� ����Ǹ� �� UI���� �������� �ݾ��� ����ȴ�.
    // �ݾ��� ���ڸ����� ,�� ǥ���Ѵ�.
    TextMeshProUGUI moneyText;

    private void Awake()
    {
        moneyText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Refresh(int money)
    {
        moneyText.text = $"{money:N0}";
    }
}
