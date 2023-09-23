using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʈ Ǯ�� �� ������Ʈ���� ��ӹ��� Ŭ����
/// </summary>
public class PooledObject : MonoBehaviour
{
    /// <summary>
    /// �� ���� ������Ʈ�� ��Ȱ��ȭ �� �� ����Ǵ� ��������Ʈ
    /// </summary>
    public Action onDisable;

    protected virtual void OnEnable()
    {
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    protected virtual void OnDisable()
    {
        onDisable?.Invoke();    // ��Ȱ��ȭ �Ǿ��ٰ� �˸�
    }

    /// <summary>
    /// ���� �ð� �Ŀ� �� ���ӿ�����Ʈ�� ��Ȱ��ȭ ��Ű�� �ڷ�ƾ
    /// </summary>
    /// <param name="delay">��Ȱ��ȭ�� �ɶ����� �ɸ��� �ð�(�⺻ = 0.0f)</param>
    /// <returns></returns>
    protected IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay); // delay��ŭ ����ϰ�
        gameObject.SetActive(false);            // ���� ������Ʈ ��Ȱ��ȭ
    }
}
