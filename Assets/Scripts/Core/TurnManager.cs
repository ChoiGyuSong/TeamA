using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    /// <summary>
    /// 턴 구현용 PlayerBase 배열
    /// </summary>
    PlayerBase[] playerBase;

    /// <summary>
    /// 턴 구현용 EnemyBase 배열
    /// </summary>
    EnemyBase[] enemyBase;

    /// <summary>
    /// 플레이어 행동용
    /// </summary>
    PlayerBase PB;

    /// <summary>
    /// 플레이어는 PlayerInputAction 으로 입력받아 행동 실행
    /// </summary>
    PlayerInputAction inputAction;

    /// <summary>
    /// 플레이어 행동 입력(1.기본공격, 2.스킬공격, 3.스킬공격)
    /// </summary>
    public int choiceAction = 0;

    Queue<CharacterBase> turnQueue; // 턴 순서를 저장하는 큐
    public CharacterBase currentCharacter; // 현재 턴을 가진 캐릭터

    GameObject clickObject = null;   // 마우스가 클릭한 게임오브젝트
    CharacterBase targetObject;      // 오브젝트 찾아놓기
    Vector3 mousePosition;           // 마우스 위치 

    private void Start()
    {
        playerBase = FindObjectsOfType<PlayerBase>();
        enemyBase = FindObjectsOfType<EnemyBase>();
        PB = FindObjectOfType<PlayerBase>();

        turnQueue = new Queue<CharacterBase>(); // 큐 초기화
        int maxCount = playerBase.Length + enemyBase.Length;    // 플레이어 오브젝트 갯수 + 적 오브젝트 갯수

        
        for (int i = 0; i < maxCount; i++)  // 총 오브젝트 갯수만큼(플레이어 + 에너미) 반복
        {
            // 플레이어와 적을 번갈아가며 큐에 추가
            if (i < playerBase.Length)  // 플레이어 오브젝트 갯수만큼
            {
                turnQueue.Enqueue(playerBase[i]);   // 큐에 플레이어 추가
            }

            if (i < enemyBase.Length)   // 적 오브젝트 갯수만큼
            {
                turnQueue.Enqueue(enemyBase[i]);    // 큐에 적 추가
            }
        }

        // 첫 번째 턴 시작
        StartTurn();
    }

    private void Awake()
    {
        inputAction = new PlayerInputAction();  // inputAction 초기화
    }
    private void Update()
    {
        // 현재 턴을 가진 캐릭터가 행동을 완료하면 다음 턴으로 넘어감
        if (currentCharacter != null && currentCharacter.isTurnComplete)
        {
            StartTurn();    // 다음턴 실행
            Debug.Log("다음 턴 실행.");
        }
    }

    private void OnEnable()     // 입력 활성화
    {
        inputAction.Player.Enable();
        inputAction.Player.B1.performed += B1;
        inputAction.Player.B2.performed += B2;
        inputAction.Player.B3.performed += B3;
    }

    private void OnDisable()    // 입력 비활성화
    {
        inputAction.Player.B3.performed -= B3;
        inputAction.Player.B2.performed -= B2;
        inputAction.Player.B1.performed -= B1;
        inputAction.Player.Disable();
    }

    /// <summary>
    /// 턴이 시작되면 호출되는 함수
    /// </summary>
    private void StartTurn()
    {
        if (turnQueue.Count == 0)   // 큐가 비어있는지 확인
        {
            Debug.Log("큐가 비었습니다.");
            return;
        }

        currentCharacter = turnQueue.Dequeue(); // 큐에서 하나를 꺼낸후 currentCharcter에 저장
        Debug.Log($"{currentCharacter.name}의 턴입니다");

        StartCoroutine(ExecuteTurn(currentCharacter));  // 꺼낸 큐를 저장한 currentCharcter의 턴 실행
    }

    /// <summary>
    /// 실제 턴 실행하는 코루틴
    /// </summary>
    /// <param name="characterBase"> 현재 턴을 진행중인 캐릭터(currentCharacter) </param>
    /// <returns></returns>
    public IEnumerator ExecuteTurn(CharacterBase characterBase)
    {
        characterBase.StartTurn();  // 턴 처리 시작(characterBase의 isTurnCompleted = false)

        if (characterBase.isAlive == false) // 큐를 꺼내온 캐릭터가 죽었다면 턴을 건너뛰기
        {
            StartTurn();    // 다음턴 진행(큐를 다시 넣지 않음)
            yield break;
        }
        else
        {
            //살아있으면 턴 실행
            if (characterBase is PlayerBase)    // 꺼내온 큐가 PlayerBase일 경우
            {
                PlayerBase player = characterBase as PlayerBase; //PlayerBase가 아니라면 null

                inputAction.Player.Mouse.Enable();          // 플레이어이므로 마우스 활성화해서 행동진행
                inputAction.Player.Mouse.performed += Mouse;
                while (player.isTurnComplete == false)      // isTurnComplete가 true될때까지 대기하는 반복문
                {
                    yield return null;
                }
                inputAction.Player.Mouse.performed -= Mouse;// 플레이어 행동이 완료됬으므로 마우스 비활성화
                inputAction.Player.Mouse.Disable();
            }
            else if (characterBase is EnemyBase)    // 꺼내온 큐가 EnemyBase일 경우
            {
                EnemyBase enemy = characterBase as EnemyBase;   //EnemyBase가 아니라면 null
                enemy.EnemyAction();        // 적 행동

                characterBase.isTurnComplete = true;    // 행동 완료 알림
            }
            yield return null;

            turnQueue.Enqueue(characterBase);    // 턴이 끝나면 큐에 다시 추가

            characterBase.EndTurn();    // 종료 처리 종료
        }
    }

    /// <summary>
    /// 키보드로 행동 입력
    /// </summary>
    /// <param name="_"></param>
    private void B1(InputAction.CallbackContext _)
    {
        choiceAction = 1;       // 플레이어 행동 선택
        Debug.Log("기본공격 선택");
    }
    private void B2(InputAction.CallbackContext _)
    {
        choiceAction = 2;       // 플레이어 행동 선택
        Debug.Log("1번 스킬 선택");
    }
    private void B3(InputAction.CallbackContext _)
    {
        choiceAction = 3;       // 플레이어 행동 선택
        Debug.Log("2번 스킬 선택");
    }
    /// <summary>
    /// 키보드로 행동 입력 후 마우스로 행동 실행할 적 클릭
    /// </summary>
    /// <param name="_"></param>
    private void Mouse(InputAction.CallbackContext _    )
    {
        StartCoroutine(PlayerAttack());     
    }

    /// <summary>
    /// 마우스를 클릭했을때 실행되는 코루틴
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayerAttack()
    {
        mousePosition = Input.mousePosition;    // 마우스 포지션을 저장
        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);    // 마우스 클릭 위치를 카메라 위치에 맞게 변경
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);    // collider를 hit에 리턴

        if (choiceAction != 0)      // 캐릭터 행동 선택이 0이 아닐시
        {
            if (hit.collider != null)   // 콜라이더 선택이 null이 아닐시(클릭을 콜라이더에 했을때)
            {
                clickObject = hit.collider.gameObject;      // clickObject에 hit에 리턴된 collider 의 게임 오브젝트를 저장
                targetObject = clickObject.GetComponent<CharacterBase>();   //targetObject에 <CharacterBase>의 clickObject를 저장
                if (hit.collider.gameObject.name == "Enemy1")       // 클릭된 collider의 게임오브젝트 이름이 Enemy1이라면
                {
                    PB.ChoiceAction(targetObject, choiceAction);    // targetObject에게 choiceAction을 PlayerBase의 ChoiceAction실행 
                }
                else if (hit.collider.gameObject.name == "Enemy2")  // 클릭된 collider의 게임오브젝트 이름이 Enemy2이라면
                {
                    PB.ChoiceAction(targetObject, choiceAction);    // targetObject에게 choiceAction을 PlayerBase의 ChoiceAction실행 
                }
                else if (hit.collider.gameObject.name == "Enemy3")  // 클릭된 collider의 게임오브젝트 이름이 Enemy3이라면
                {
                    PB.ChoiceAction(targetObject, choiceAction);    // targetObject에게 choiceAction을 PlayerBase의 ChoiceAction실행 
                }
                else if (hit.collider.gameObject.name == "Boss")    // 클릭된 collider의 게임오브젝트 이름이 Boss라면
                {
                    PB.ChoiceAction(targetObject, choiceAction);    // targetObject에게 choiceAction을 PlayerBase의 ChoiceAction실행 
                }
                yield return null;
                currentCharacter.isTurnComplete = true;     // 턴 완료 알림
            }
        }
        else
        {
            Debug.Log("행동이 선택되지 않았습니다");
        }
    }
}


