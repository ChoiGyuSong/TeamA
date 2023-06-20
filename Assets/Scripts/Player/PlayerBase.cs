using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : CharacterBase
{
    Enemy1 enemy1;
    Enemy2 enemy2;
    Enemy3 enemy3;
    GameObject enemy;
    PlayerInputAction inputAction;

<<<<<<< Updated upstream
    private void Awake()
=======
    Vector3 mousePosition;      // ���콺 ��ġ 

    float skill = 0.0f;

    public float ASkillCoefficient = 5.0f;
    public float BSkillCoefficient = 1.5f;

    private CharacterBase targetObject;
    GameObject clickObject = null;

    public int costA = 20;
    public int costB = 30;
    bool onActive = true;

    protected override void Awake()
>>>>>>> Stashed changes
    {
        enemy1 = FindObjectOfType<Enemy1>();
        enemy2 = FindObjectOfType<Enemy2>();
        enemy3 = FindObjectOfType<Enemy3>();
        inputAction = new PlayerInputAction();
        onActive = false;
    }

    private void Start()
    {
<<<<<<< Updated upstream

=======
        StartCoroutine(StartAttack());
    }
    public void Update()
    {
        
    }

    IEnumerator StartAttack()
    {
        onActive = true;
        if (Input.GetMouseButtonDown(0))    // ���콺 Ŭ���Ǿ�����
        {
            mousePosition = Input.mousePosition;    // ���콺 �������� ����
            Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);    // ���콺 Ŭ�� ��ġ�� ī�޶� ��ġ�� �°� ����

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);    // collider�� hit�� ����
            if (choiceAction != 0)      // ĳ���� �ൿ ������ 0�� �ƴҽ�
            {
                if (hit.collider != null)   // �ݶ��̴� ������ null�� �ƴҽ�(Ŭ���� �ݶ��̴��� ������)
                {
                    clickObject = hit.collider.gameObject;
                    targetObject = clickObject.GetComponent<CharacterBase>();
                    if (hit.collider.gameObject.name == "Enemy1")
                    {
                        ChoiceAction(targetObject, choiceAction);
                    }
                    else if (hit.collider.gameObject.name == "Enemy2")
                    {
                        ChoiceAction(targetObject, choiceAction);
                    }
                    else if (hit.collider.gameObject.name == "Enemy3")
                    {
                        ChoiceAction(targetObject, choiceAction);
                    }
                    choiceAction = 0;   //�ʱ�ȭ
                }
            }
        }
        onActive = false;
        yield return null;
>>>>>>> Stashed changes
    }
    private void OnEnable()     // inputAction Ȱ��ȭ
    {
<<<<<<< Updated upstream
        inputAction.Player.Enable();
        inputAction.Player.B1.performed += B1;    // �⺻���� ���� Ȱ��ȭ
        inputAction.Player.B2.performed += B2;      // ��ų ���� Ȱ��ȭ
        inputAction.Player.B3.performed += B3;  // �� ���� Ȱ��ȭ
=======
        if(onActive)
        {
            inputAction.Player.Enable();
            inputAction.Player.B1.performed += B1;  // �⺻���� ���� Ȱ��ȭ
            inputAction.Player.B2.performed += B2;  // ��ų ���� Ȱ��ȭ
            inputAction.Player.B3.performed += B3;  // �� ���� Ȱ��ȭ
        }
>>>>>>> Stashed changes
    }



    private void OnDisable()    // inputAction ��Ȱ��ȭ
    {
<<<<<<< Updated upstream
        inputAction.Player.B1.performed -= B1;  // �� ���� ��Ȱ��ȭ
        inputAction.Player.B2.performed -= B2;              // ��ų ���� ��Ȱ��ȭ
        inputAction.Player.B3.performed -= B3;         // �⺻���� ���� ��Ȱ��ȭ
        inputAction.Player.Disable();
=======
        if(onActive)
        {
            inputAction.Player.B1.performed -= B1;  // �� ���� ��Ȱ��ȭ
            inputAction.Player.B2.performed -= B2;  // ��ų ���� ��Ȱ��ȭ
            inputAction.Player.B3.performed -= B3;  // �⺻���� ���� ��Ȱ��ȭ
            inputAction.Player.Disable();
        }
>>>>>>> Stashed changes
    }

    private void B1(InputAction.CallbackContext value)    // �Ϲݰ��� ���ý�(Ű���� 1)
    {
        Debug.Log(1);
        enemy1.getDemage(5, 0);
    }

    private void B2(InputAction.CallbackContext value)     // ��ų���� ���ý�(Ű���� 2)
    {
        Debug.Log(2);
        enemy2.getDemage(5, 0);
    }

    private void B3(InputAction.CallbackContext value)
    {
        Debug.Log(3);
        enemy3.getDemage(5, 0);
    }
}
