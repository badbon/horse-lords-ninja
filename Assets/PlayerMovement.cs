using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float growthAmount = 0.05f; // Pre-determined growth amount
    private Rigidbody2D rb;
    public Sprite upSprite, downSprite, leftSprite, rightSprite, upWalkSprite, downWalkSprite, leftWalkSprite, rightWalkSprite;
    public SpriteRenderer spriteRenderer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 direction = (Input.mousePosition - new Vector3(screenCenter.x, screenCenter.y, 0)).normalized;

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

    private void AdjustSprite(Sprite spriteDir)
    {
        spriteRenderer.sprite = spriteDir;
    }

    // Growing / food collision
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Food")
        {
            // Increase the player size by a consistent amount
            transform.localScale += new Vector3(growthAmount, growthAmount, 0);

            // Destroy the food
            Destroy(col.gameObject);
        }

        // Player collision
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player collision!");
            if (transform.localScale.x > col.gameObject.transform.localScale.x)
            {
                // Current player is larger and can consume the other player
                float consumeRate = 1.05f; // Adjust as necessary
                transform.localScale *= consumeRate;

                // Destroy the smaller player or handle respawning
                Destroy(col.gameObject);
            }
        }
    }
}
