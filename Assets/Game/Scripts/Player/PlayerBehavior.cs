using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float moveAccel;
    [SerializeField] protected float maxSpeed;

    [Header("Score")]
    [SerializeField] private float scoringRatio;

    [Header("Jump")]
    [SerializeField] private float jumpAccel;
    protected bool isJumping;
    protected bool isOnGround;

    [Header("Ground Raycast")]
    [SerializeField] protected float groundRaycastDistance;
    [SerializeField] protected LayerMask groundLayerMask;

    [Header("Audio")]
    [SerializeField] private AudioClip jump;

    [Header("Game Over")]
    [SerializeField] private float fallPositionY;

    private float lastPositionX;
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audioPlayer;

    protected void Init()
    {
        lastPositionX = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
    }

    protected void Move()
    {
        var velocity = rb.velocity;

        if (isJumping)
        {
            velocity.y += jumpAccel;
            isJumping = false;
        }

        velocity.x = Mathf.Clamp(velocity.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);

        rb.velocity = velocity;

        int distancePassed = Mathf.FloorToInt(transform.position.x - lastPositionX);
        int scoreIncrement = Mathf.FloorToInt(distancePassed / scoringRatio);

        if (scoreIncrement > 0)
        {
            MainManager.Instance.AddScore(scoreIncrement);
            lastPositionX += distancePassed;
        }
    }

    protected void CheckGameOver()
    {
        if (transform.position.y < fallPositionY)
        {
            MainManager.Instance.onGameOver?.Invoke();
        }
    }

    protected void ChangeAnimation()
    {
        anim.SetBool("isOnGround", isOnGround);
    }

    protected void PlayJump()
    {
        audioPlayer.PlayOneShot(jump);
    }

    protected void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
        if (hit)
        {
            if (!isOnGround && rb.velocity.y <= 0)
            {
                isOnGround = true;
            }
        } else
        {
            isOnGround = false;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down * groundRaycastDistance), Color.white);
    }
}
