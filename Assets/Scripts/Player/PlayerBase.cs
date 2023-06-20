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
    Vector3 mousePosition;      // 마우스 위치 

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
        if (Input.GetMouseButtonDown(0))    // 마우스 클릭되었을때
        {
            mousePosition = Input.mousePosition;    // 마우스 포지션을 저장
            Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);    // 마우스 클릭 위치를 카메라 위치에 맞게 변경

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);    // collider를 hit에 리턴
            if (choiceAction != 0)      // 캐릭터 행동 선택이 0이 아닐시
            {
                if (hit.collider != null)   // 콜라이더 선택이 null이 아닐시(클릭을 콜라이더에 했을때)
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
                    choiceAction = 0;   //초기화
                }
            }
        }
        onActive = false;
        yield return null;
>>>>>>> Stashed changes
    }
    private void OnEnable()     // inputAction 활성화
    {
<<<<<<< Updated upstream
        inputAction.Player.Enable();
        inputAction.Player.B1.performed += B1;    // 기본공격 선택 활성화
        inputAction.Player.B2.performed += B2;      // 스킬 선택 활성화
        inputAction.Player.B3.performed += B3;  // 적 선택 활성화
=======
        if(onActive)
        {
            inputAction.Player.Enable();
            inputAction.Player.B1.performed += B1;  // 기본공격 선택 활성화
            inputAction.Player.B2.performed += B2;  // 스킬 선택 활성화
            inputAction.Player.B3.performed += B3;  // 적 선택 활성화
        }
>>>>>>> Stashed changes
    }



    private void OnDisable()    // inputAction 비활성화
    {
<<<<<<< Updated upstream
        inputAction.Player.B1.performed -= B1;  // 적 선택 비활성화
        inputAction.Player.B2.performed -= B2;              // 스킬 선택 비활성화
        inputAction.Player.B3.performed -= B3;         // 기본공격 선택 비활성화
        inputAction.Player.Disable();
=======
        if(onActive)
        {
            inputAction.Player.B1.performed -= B1;  // 적 선택 비활성화
            inputAction.Player.B2.performed -= B2;  // 스킬 선택 비활성화
            inputAction.Player.B3.performed -= B3;  // 기본공격 선택 비활성화
            inputAction.Player.Disable();
        }
>>>>>>> Stashed changes
    }

    private void B1(InputAction.CallbackContext value)    // 일반공격 선택시(키보드 1)
    {
        Debug.Log(1);
        enemy1.getDemage(5, 0);
    }

    private void B2(InputAction.CallbackContext value)     // 스킬공격 선택시(키보드 2)
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
