using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    /// <summary>
    /// �� ������Ʈ�� ǥ���� �������� ����
    /// </summary>
    ItemData data = null;
    public ItemData ItemData
    {
        get => data;
        set
        {
            if (data == null)    // ���丮���� �ѹ� ���������ϵ���
            {
                data = value;
            }
        }
    }
}
