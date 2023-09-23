using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool<T> : MonoBehaviour where T : PooledObject
{
    //Ǯ�� ��� ���� ������Ʈ�� ������
    public GameObject origianlPrefab;

    // Ǯ�� ũ��. ó���� �����ϴ� ������Ʈ�� ����. ������ 2^n���� ��� ���� ����.
    public int poolSize = 64;

    // Ǯ�� ������ ��� ������Ʈ�� ����ִ� �迭
    T[] pool;

    //��밡����(��Ȱ��ȭ�Ǿ��ִ�) ������Ʈ���� ����ִ� ť
    Queue<T> readyQueue;

    public void Initialize()
    {
        if (pool == null)
        {
            pool = new T[poolSize];                     // Ǯ ��ü ũ��� �迭 �Ҵ�
            readyQueue = new Queue<T>(poolSize);        // ����ť ����(capacity�� poolSize�� ����

            // readyQueue.Count;               // ������ ����ִ� ����
            // readyQueue.capatity;            // ���� �̸��غ��� ���� ����

            GenerateObjects(0, poolSize, pool);
        }
        else
        {
            // �ι�° ���� �ҷ����� �̹� Ǯ�� ������� �ִ� ��Ȳ
            foreach (T obj in pool)
            {
                obj.gameObject.SetActive(false);    // ���� ��Ȱ��ȭ
            }
        }

    }


    /// <summary>
    /// Ǯ���� ������Ʈ�� �ϳ� ���� �� �����ִ� �Լ�
    /// </summary>
    /// <returns> ����ť���� ������ Ȱ��ȭ ��Ų ������Ʈ</returns>
    public T GetObject()
    {
        if (readyQueue.Count > 0)
        {
            // ����������
            T comp = readyQueue.Dequeue();          // �ϳ� ������
            comp.gameObject.SetActive(true);        // Ȱ��ȭ��Ų ������
            return comp;                            // ���� �� ����

        }
        else
        {
            // ���� ������Ʈ�� ������
            ExpandPool();           // Ǯ Ȯ���Ű��
            return GetObject();     // �ٽ� ��û
        }
    }

    //Ǯ�� �ι�� Ȯ���Ű�� �Լ�
    private void ExpandPool()
    {
        Debug.LogWarning($"{gameObject.name} Ǯ ������ ����. {poolSize} -> {poolSize * 2}");

        int newSize = poolSize * 2;                         // ���ο� ũ�� ���ϱ�
        T[] newPool = new T[newSize];                       // ���ο� ũ�⸸ŭ �� �迭 �����
        for (int i = 0; i < poolSize; i++)                  // ���� �迭�� �ִ� ���� �� �迭�� ����
        {
            newPool[i] = pool[i];
        }

        GenerateObjects(poolSize, newSize, newPool);        // �� �迭�� ���� �κп� ������Ʈ �����ؼ� ����
        pool = newPool;                                     // �� �迭�� pool�� ����
        poolSize = newSize;                                 // �� ũ�⸦ ũ��� ����
    }


    /// <summary>
    /// ������Ʈ �����ؼ� �迭�� �߰����ִ� �Լ�
    /// </summary>
    /// <param name="start">�迭�� ���� �ε���</param>
    /// <param name="end">�迭�� ������ �ε���-1</param>
    /// <param name="newArray">������ ������Ʈ�� �� �迭</param>
    private void GenerateObjects(int start, int end, T[] newArray)
    {
        for (int i = start; i < end; i++)                               // ���� ������� ũ�⸸ŭ �ݺ�
        {
            GameObject obj = Instantiate(origianlPrefab, transform);    // �����ϰ� ���� �θ� ���� ������Ʈ�� �����
            obj.name = $"{origianlPrefab.name}_{i}";                    // �̸� ���еǵ��� ���� 

            T comp = obj.GetComponent<T>();                             // PooledObject ������Ʈ �޾ƿͼ�
            comp.onDisable += () => readyQueue.Enqueue(comp);           // PooledObject�� disable�� �� ����ť�� �ǵ�����

            newArray[i] = comp;                                             // Ǯ �迭�� ����
            obj.SetActive(false);                                       // ������ ���� ������Ʈ ��Ȱ��ȭ(=>��Ȱ��ȭ �Ǹ鼭 ����ť���� �߰��ȴ�)

        }
    }
}