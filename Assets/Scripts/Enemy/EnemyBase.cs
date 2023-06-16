using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBase : CharacterBase
{
<<<<<<< Updated upstream
=======
    PlayerInputAction inputActions;
    Player1 player1;
    Player2 player2;
    Player3 player3;

    protected override void Awake()
    {
        base.Awake();
        player1 = FindObjectOfType<Player1>();
        player2 = FindObjectOfType<Player2>();
        player3 = FindObjectOfType<Player3>();
        inputActions = new PlayerInputAction();
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.B1.performed += test1;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.B1.performed -= test1;
    }

    private void test1(InputAction.CallbackContext context)
    {
        OnAttack();
    }

    private void OnAttack()
    {
        switch(Random.Range(0, 3))
        {
            case 0:Attack(player1, 0);
                break;
            case 1:Attack(player2, 0);
                break;
            case 2:Attack(player3, 0);
                break;
        }
    }
>>>>>>> Stashed changes
}
