using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    /// <summary>
    /// �� ������ PlayerBase �迭
    /// </summary>
    PlayerBase[] playerBase;

    /// <summary>
    /// �� ������ EnemyBase �迭
    /// </summary>
    EnemyBase[] enemyBase;

    /// <summary>
    /// �÷��̾� �ൿ��
    /// </summary>
    PlayerBase PB;

    /// <summary>
    /// �÷��̾�� PlayerInputAction ���� �Է¹޾� �ൿ ����
    /// </summary>
    PlayerInputAction inputAction;

    /// <summary>
    /// �÷��̾� �ൿ �Է�(1.�⺻����, 2.��ų����, 3.��ų����)
    /// </summary>
    public int choiceAction = 0;

    Queue<CharacterBase> turnQueue; // �� ������ �����ϴ� ť
    public CharacterBase currentCharacter; // ���� ���� ���� ĳ����

    GameObject clickObject = null;   // ���콺�� Ŭ���� ���ӿ�����Ʈ
    CharacterBase targetObject;      // ������Ʈ ã�Ƴ���
    Vector3 mousePosition;           // ���콺 ��ġ 

    private void Start()
    {
        playerBase = FindObjectsOfType<PlayerBase>();
        enemyBase = FindObjectsOfType<EnemyBase>();
        PB = FindObjectOfType<PlayerBase>();

        turnQueue = new Queue<CharacterBase>(); // ť �ʱ�ȭ
        int maxCount = playerBase.Length + enemyBase.Length;    // �÷��̾� ������Ʈ ���� + �� ������Ʈ ����

        
        for (int i = 0; i < maxCount; i++)  // �� ������Ʈ ������ŭ(�÷��̾� + ���ʹ�) �ݺ�
        {
            // �÷��̾�� ���� �����ư��� ť�� �߰�
            if (i < playerBase.Length)  // �÷��̾� ������Ʈ ������ŭ
            {
                turnQueue.Enqueue(playerBase[i]);   // ť�� �÷��̾� �߰�
            }

            if (i < enemyBase.Length)   // �� ������Ʈ ������ŭ
            {
                turnQueue.Enqueue(enemyBase[i]);    // ť�� �� �߰�
            }
        }

        // ù ��° �� ����
        StartTurn();
    }

    private void Awake()
    {
        inputAction = new PlayerInputAction();  // inputAction �ʱ�ȭ
    }
    private void Update()
    {
        // ���� ���� ���� ĳ���Ͱ� �ൿ�� �Ϸ��ϸ� ���� ������ �Ѿ
        if (currentCharacter != null && currentCharacter.isTurnComplete)
        {
            StartTurn();    // ������ ����
            Debug.Log("���� �� ����.");
        }
    }

    private void OnEnable()     // �Է� Ȱ��ȭ
    {
        inputAction.Player.Enable();
        inputAction.Player.B1.performed += B1;
        inputAction.Player.B2.performed += B2;
        inputAction.Player.B3.performed += B3;
    }

    private void OnDisable()    // �Է� ��Ȱ��ȭ
    {
        inputAction.Player.B3.performed -= B3;
        inputAction.Player.B2.performed -= B2;
        inputAction.Player.B1.performed -= B1;
        inputAction.Player.Disable();
    }

    /// <summary>
    /// ���� ���۵Ǹ� ȣ��Ǵ� �Լ�
    /// </summary>
    private void StartTurn()
    {
        if (turnQueue.Count == 0)   // ť�� ����ִ��� Ȯ��
        {
            Debug.Log("ť�� ������ϴ�.");
            return;
        }

        currentCharacter = turnQueue.Dequeue(); // ť���� �ϳ��� ������ currentCharcter�� ����
        Debug.Log($"{currentCharacter.name}�� ���Դϴ�");

        StartCoroutine(ExecuteTurn(currentCharacter));  // ���� ť�� ������ currentCharcter�� �� ����
    }

    /// <summary>
    /// ���� �� �����ϴ� �ڷ�ƾ
    /// </summary>
    /// <param name="characterBase"> ���� ���� �������� ĳ����(currentCharacter) </param>
    /// <returns></returns>
    public IEnumerator ExecuteTurn(CharacterBase characterBase)
    {
        characterBase.StartTurn();  // �� ó�� ����(characterBase�� isTurnCompleted = false)

        if (characterBase.isAlive == false) // ť�� ������ ĳ���Ͱ� �׾��ٸ� ���� �ǳʶٱ�
        {
            StartTurn();    // ������ ����(ť�� �ٽ� ���� ����)
            yield break;
        }
        else
        {
            //��������� �� ����
            if (characterBase is PlayerBase)    // ������ ť�� PlayerBase�� ���
            {
                PlayerBase player = characterBase as PlayerBase; //PlayerBase�� �ƴ϶�� null

                inputAction.Player.Mouse.Enable();          // �÷��̾��̹Ƿ� ���콺 Ȱ��ȭ�ؼ� �ൿ����
                inputAction.Player.Mouse.performed += Mouse;
                while (player.isTurnComplete == false)      // isTurnComplete�� true�ɶ����� ����ϴ� �ݺ���
                {
                    yield return null;
                }
                inputAction.Player.Mouse.performed -= Mouse;// �÷��̾� �ൿ�� �Ϸ�����Ƿ� ���콺 ��Ȱ��ȭ
                inputAction.Player.Mouse.Disable();
            }
            else if (characterBase is EnemyBase)    // ������ ť�� EnemyBase�� ���
            {
                EnemyBase enemy = characterBase as EnemyBase;   //EnemyBase�� �ƴ϶�� null
                enemy.EnemyAction();        // �� �ൿ

                characterBase.isTurnComplete = true;    // �ൿ �Ϸ� �˸�
            }
            yield return null;

            turnQueue.Enqueue(characterBase);    // ���� ������ ť�� �ٽ� �߰�

            characterBase.EndTurn();    // ���� ó�� ����
        }
    }

    /// <summary>
    /// Ű����� �ൿ �Է�
    /// </summary>
    /// <param name="_"></param>
    private void B1(InputAction.CallbackContext _)
    {
        choiceAction = 1;       // �÷��̾� �ൿ ����
        Debug.Log("�⺻���� ����");
    }
    private void B2(InputAction.CallbackContext _)
    {
        choiceAction = 2;       // �÷��̾� �ൿ ����
        Debug.Log("1�� ��ų ����");
    }
    private void B3(InputAction.CallbackContext _)
    {
        choiceAction = 3;       // �÷��̾� �ൿ ����
        Debug.Log("2�� ��ų ����");
    }
    /// <summary>
    /// Ű����� �ൿ �Է� �� ���콺�� �ൿ ������ �� Ŭ��
    /// </summary>
    /// <param name="_"></param>
    private void Mouse(InputAction.CallbackContext _    )
    {
        StartCoroutine(PlayerAttack());     
    }

    /// <summary>
    /// ���콺�� Ŭ�������� ����Ǵ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayerAttack()
    {
        mousePosition = Input.mousePosition;    // ���콺 �������� ����
        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);    // ���콺 Ŭ�� ��ġ�� ī�޶� ��ġ�� �°� ����
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);    // collider�� hit�� ����

        if (choiceAction != 0)      // ĳ���� �ൿ ������ 0�� �ƴҽ�
        {
            if (hit.collider != null)   // �ݶ��̴� ������ null�� �ƴҽ�(Ŭ���� �ݶ��̴��� ������)
            {
                clickObject = hit.collider.gameObject;      // clickObject�� hit�� ���ϵ� collider �� ���� ������Ʈ�� ����
                targetObject = clickObject.GetComponent<CharacterBase>();   //targetObject�� <CharacterBase>�� clickObject�� ����
                if (hit.collider.gameObject.name == "Enemy1")       // Ŭ���� collider�� ���ӿ�����Ʈ �̸��� Enemy1�̶��
                {
                    PB.ChoiceAction(targetObject, choiceAction);    // targetObject���� choiceAction�� PlayerBase�� ChoiceAction���� 
                }
                else if (hit.collider.gameObject.name == "Enemy2")  // Ŭ���� collider�� ���ӿ�����Ʈ �̸��� Enemy2�̶��
                {
                    PB.ChoiceAction(targetObject, choiceAction);    // targetObject���� choiceAction�� PlayerBase�� ChoiceAction���� 
                }
                else if (hit.collider.gameObject.name == "Enemy3")  // Ŭ���� collider�� ���ӿ�����Ʈ �̸��� Enemy3�̶��
                {
                    PB.ChoiceAction(targetObject, choiceAction);    // targetObject���� choiceAction�� PlayerBase�� ChoiceAction���� 
                }
                else if (hit.collider.gameObject.name == "Boss")    // Ŭ���� collider�� ���ӿ�����Ʈ �̸��� Boss���
                {
                    PB.ChoiceAction(targetObject, choiceAction);    // targetObject���� choiceAction�� PlayerBase�� ChoiceAction���� 
                }
                yield return null;
                currentCharacter.isTurnComplete = true;     // �� �Ϸ� �˸�
            }
        }
        else
        {
            Debug.Log("�ൿ�� ���õ��� �ʾҽ��ϴ�");
        }
    }
}


