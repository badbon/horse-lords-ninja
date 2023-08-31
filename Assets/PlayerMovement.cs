using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float speed = 5.0f;
    private Rigidbody2D rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    // Growing / food collision
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Food")
        {
            // Increase the player size
            float growthRate = 1.1f; // or any value you deem fit
            transform.localScale *= growthRate;

            // Destroy the food
            Destroy(col.gameObject);
        }

        // Player collision
        if (col.gameObject.tag == "Player")
        {
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
