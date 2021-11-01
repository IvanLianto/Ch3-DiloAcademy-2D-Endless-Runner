using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerBehavior
{
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !PlayerData.GAME_OVER)
        {
            if (isOnGround)
            {
                isJumping = true;
                PlayJump();
            }
        }

        ChangeAnimation();

        CheckGameOver();
    }

    private void FixedUpdate()
    {
        CheckGround();

        if (!PlayerData.GAME_OVER)
        {
            Move();
        }
        
    }
}
