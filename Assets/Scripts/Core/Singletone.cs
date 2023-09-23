using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private bool initialized = false;

    /// <summary>
    /// �̹� ����ó���� ������ Ȯ���ϱ� ���� ����
    /// </summary>
    private static bool isShutDown = false;

    /// <summary>
    /// �̱����� ��ü.
    /// </summary>
    private static T instance;

    /// <summary>
    /// �̱����� ��ü�� �б� ���� ������Ƽ. ��ü�� ��������� �ʾ����� ���� �����.
    /// </summary>
    public static T Inst
    {
        get
        {
            if (isShutDown)      // ����ó���� �� ��Ȳ�̸�
            {
                Debug.LogWarning($"{typeof(T).Name} �̱����� �̹� ���� ���̴�.");    // ��� ����ϰ�
                return null;    // null ����
            }
            if (instance == null)
            {
                // instance�� ������ ���� �����.

                T sigleton = FindObjectOfType<T>();         // ������ �̱��� ã�ƺ���
                if (sigleton == null)                       // ���� �̱����� �ִ��� Ȯ��
                {
                    // ������ �̱����� ����.
                    GameObject gameObj = new GameObject();  // �������Ʈ �����
                    gameObj.name = $"{typeof(T).Name} Singleton";   // �̸� �����ϰ�
                    sigleton = gameObj.AddComponent<T>();   // �̱��� ������Ʈ �߰�  
                }
                instance = sigleton;                        // instance�� ã�Ұų� ������� ��ü ����
                DontDestroyOnLoad(instance.gameObject);     // ���� ����� �� ���ӿ�����Ʈ�� �������� �ʵ��� ����
            }

            return instance;    // instance����(�̹� �ְų� ���� �������.)
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            // ���� ��ġ�Ǿ� �ִ� ù��° �̱��� ���ӿ�����Ʈ
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            // ù��° �̱��� ���� ������Ʈ�� ������� ���Ŀ� ������� �̱��� ���� ������Ʈ
            if (instance != this)
            {
                Destroy(this.gameObject);   // ù��° �̱���� �ٸ� ���� ������Ʈ�� ����
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!initialized)
        {
            OnPreInitialize();
        }
        if (mode != LoadSceneMode.Additive)    // �׳� �ڵ����� ���ε��� ���� 4���� ����
        {
            OnInitialize();
        }
    }

    /// <summary>
    /// ���α׷��� ����� �� ����Ǵ� �Լ�
    /// </summary>
    private void OnApplicationQuit()
    {
        isShutDown = true;  // ���� ǥ��
    }

    /// <summary>
    /// �̱����� ������� �� �� �ѹ��� ȣ��� �ʱ�ȭ �Լ�
    /// </summary>
    protected virtual void OnPreInitialize()
    {
        initialized = true;
    }

    /// <summary>
    /// �̱����� ��������� ���� Single�� �ε�� ������ ȣ��� �ʱ�ȭ �Լ�
    /// </summary>
    protected virtual void OnInitialize()
    {
    }


}
