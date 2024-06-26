using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float growthAmount = 0.05f; // Pre-determined growth amount
    public float HP = 50;
    public float critChance = 10f; // Percentage chance of a critical hit

    private Rigidbody2D rb;
    public Sprite upSprite, downSprite, leftSprite, rightSprite,
     upWalkSprite, downWalkSprite, leftWalkSprite, rightWalkSprite;
    public SpriteRenderer spriteRenderer;

    public bool alternateSprite = false;
    public float alternateAnimTime = 0.1f;
    public int coins = 0;
    public GameObject slashPrefab;
    public TextMeshProUGUI coinText;
    public GameObject progressBarPrefab;

    public PlayerThief playerThief;
    public GameObject bombPrefab;
    public Camera cam;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerThief = GetComponent<PlayerThief>();
        InvokeRepeating("AlternateTimer", alternateAnimTime, alternateAnimTime);

        cam = Camera.main;
    }

    private void Update()
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

        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            SlashSword();
        }

        // Bomb
        if(Input.GetKeyDown(KeyCode.E))
        {
            // Check cooldown
            if (Time.time < playerThief.bombCooldownTime)
            {
                // Cooldown not over
                Debug.Log("Cooldown not over!");
                return;
            }
            // Check if player has bombs
            if(playerThief.bombs > 0)
            {
                // Use bomb
                playerThief.bombs--;
                // Create bomb prefab
                GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
                // Launch rigidbody like a throwing knife (rotation and velocity)
                Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();
                // Direct towards mouse direction from center
                Vector2 screenCenter2 = new Vector2(Screen.width / 2, Screen.height / 2);
                Vector2 direction2 = (Input.mousePosition - new Vector3(screenCenter2.x, screenCenter2.y, 0)).normalized;

                // Set the bomb's rotation to face the direction of the mouse
                bombRb.transform.right = direction2;

                // Set the velocity of the bomb to move in the direction of the mouse
                float vel = 14f;
                bombRb.velocity = direction2 * vel;  // Use 'direction' here, not 'transform.right'

                // Add torque to make the bomb spin
                float torque = 18f;
                bombRb.AddTorque(torque);

                // Run cooldown timer
                playerThief.BombCooldown();
            }
        }

        // Update coin text
        coinText.text = coins.ToString();
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

        // 1 hit from enemy is death
        if (col.gameObject.tag == "Enemy")
        {
            // Game over
            Debug.Log("Dead!");
            // Restart game, temporary solution
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void TakeDamage(float dmgTaken)
    {
        HP -= dmgTaken;
        if (HP <= 0)
        {
            // Game over
            Debug.Log("Dead!");
        }
    }

    public void SlashSword()
    {
        // Create a slash prefab
        GameObject slash = Instantiate(slashPrefab, transform.position, Quaternion.identity);
        // Crit
        if (UnityEngine.Random.Range(0, 100) < critChance)
        {
            slash.GetComponent<DamageDealer>().isCrit = true;
        }
        // Launch rigidbody like a throwing knife (rotation and velocity)
        Rigidbody2D swordRb = slash.GetComponent<Rigidbody2D>();
        // Direct towards mouse direction from center
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 direction = (Input.mousePosition - new Vector3(screenCenter.x, screenCenter.y, 0)).normalized;

        // Set the sword's rotation to face the direction of the mouse
        swordRb.transform.right = direction;

        // Set the velocity of the sword to move in the direction of the mouse
        float vel = 14f;
        swordRb.velocity = direction * vel;  // Use 'direction' here, not 'transform.right'

        // Add torque to make the sword spin
        float torque = 18f;
        swordRb.AddTorque(torque);
    }

}
