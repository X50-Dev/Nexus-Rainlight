using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    public cs_PlayerHandler playerHandler;
    public cs_MovementSystem MovementSystem;
    public cs_JumpSystem JumpSystem;
    //public cs_PlayerAnimation playerAnimation;
    //public cs_PlayerAttack playerAttack;
    //public cs_PlayerStats playerStats;
    void Start()
    {
        playerHandler = GetComponent<cs_PlayerHandler>();
        MovementSystem = GetComponent<cs_MovementSystem>();
        JumpSystem = GetComponent<cs_JumpSystem>();
    }

    void Update()
    {
        if (MovementSystem != null)
        {
            Vector2 moveInput = playerHandler.GetMoveInput();
            MovementSystem.Move(moveInput);
        }
        if (JumpSystem != null)
        {
            int bOnJump = playerHandler.GetJumpInput();
            JumpSystem.Jumping(bOnJump);
        }
    }
}
