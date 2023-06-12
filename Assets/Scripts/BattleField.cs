using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    // 변수 만들어서 캐릭터 저장
    // 
    public PoolObjectType warriorObj;
    List<GameObject> objects = new List<GameObject>();
    Mage mage;
    PlayerInputAction choiceCharater;
    int choiceClass = 0;

    private void Start()
    {
         // int EnemyClass = Random.Range(4, 7);
         // ChoiceClass();
    }

    private void Awake()
    {
        choiceCharater = new PlayerInputAction();
        mage = FindObjectOfType<Mage>();
        mage.getDemage(5f, 0);
    }

    private void OnEnable()
    {
        choiceCharater.Player.Enable();
        choiceCharater.Player.Warrior.performed += OnWarrior;
        choiceCharater.Player.Archer.performed += OnArcher;
        choiceCharater.Player.Mage.performed += OnMage;
    }
 

    private void OnDisable()
    {
        choiceCharater.Player.Mage.performed -= OnMage;
        choiceCharater.Player.Archer.performed -= OnArcher;
        choiceCharater.Player.Warrior.performed -= OnWarrior;
        choiceCharater.Player.Disable();
    }

    private void OnWarrior(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ChoiceClass(1);
    }

    private void OnArcher(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ChoiceClass(2);
    }
    private void OnMage(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ChoiceClass(3);
    }

    private void ChoiceClass(int choiceClass)
    {
        if (choiceClass == 1)
        {
            Debug.Log($"Warrior{choiceClass}");
            GameObject test = Factory.Inst.GetObject(warriorObj);
            test.transform.position = transform.position;
            objects.Add(test);
        }

        else if (choiceClass == 2)
        {
            Debug.Log($"Archer{choiceClass}");
        }

        else if (choiceClass == 3)
        {
            Debug.Log($"Mage{choiceClass}");
        }

        else
        {
            Debug.Log("캐릭터 재선택");
        }
    }
}
