using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5.0f;
    public float growthAmount = 0.05f; // Pre-determined growth amount
    private Rigidbody2D rb;
    public Sprite upSprite, downSprite, leftSprite, rightSprite, upWalkSprite, downWalkSprite, leftWalkSprite, rightWalkSprite;
    public SpriteRenderer spriteRenderer;
    public bool alternateSprite = false;
    public float alternateAnimTime = 0.1f;

    public PlayerMovement playerTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(playerTarget == null)
            playerTarget = FindObjectOfType<PlayerMovement>();

        InvokeRepeating("AlternateTimer", alternateAnimTime, alternateAnimTime);
    }

    private void FixedUpdate()
    {
        // Enemy directions/pathfinding
        Vector2 direction = Vector2.zero;

        // Simple chase
        direction = (playerTarget.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;
        

        // Generate current direction (up, down, left, right)
        if (direction.x > 0 && direction.y > 0)
        {
            // Up and right
            AdjustSprite(upSprite);
        }
        else if (direction.x > 0 && direction.y < 0)
        {
            // Down and right
            AdjustSprite(rightSprite);
        }
        else if (direction.x < 0 && direction.y > 0)
        {
            // Up and left
            AdjustSprite(leftSprite);
        }
        else if (direction.x < 0 && direction.y < 0)
        {
            // Down and left
            AdjustSprite(downSprite);
        }
        else if (direction.x > 0 && direction.y == 0)
        {
            // Right
            AdjustSprite(rightSprite);
        }
        else if (direction.x < 0 && direction.y == 0)
        {
            // Left
            AdjustSprite(leftSprite);
        }
        else if (direction.x == 0 && direction.y > 0)
        {
            // Up
            AdjustSprite(upSprite);
        }
        else if (direction.x == 0 && direction.y < 0)
        {
            // Down
            AdjustSprite(downSprite);
        }
    }

    private void AlternateTimer()
    {
        alternateSprite = !alternateSprite;
    }

    private void AdjustSprite(Sprite spriteDir)
    {
        // Walk cycle animation (alternate between walk and regular sprite)
        if (alternateSprite)
        {
            spriteRenderer.sprite = spriteDir;
        }
        else
        {
            if (spriteDir == upSprite)
            {
                spriteRenderer.sprite = upWalkSprite;
            }
            else if (spriteDir == downSprite)
            {
                spriteRenderer.sprite = downWalkSprite;
            }
            else if (spriteDir == leftSprite)
            {
                spriteRenderer.sprite = leftWalkSprite;
            }
            else if (spriteDir == rightSprite)
            {
                spriteRenderer.sprite = rightWalkSprite;
            }
        }
    }
}
