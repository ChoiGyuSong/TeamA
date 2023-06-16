using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleField : MonoBehaviour
{
    PlayerInputAction inputAction;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
    }

    private void OnEnable()
    {
        inputAction.Player.Enable();
        inputAction.Player.Attack.performed += OnAttack;
        inputAction.Player.Skill.performed += OnSkill;
    }
    private void OnDisable()
    {
        inputAction.Player.Skill.performed -= OnSkill;
        inputAction.Player.Attack.performed -= OnAttack;
        inputAction.Player.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
    }

    private void OnSkill(InputAction.CallbackContext context)
    {
    }
}
