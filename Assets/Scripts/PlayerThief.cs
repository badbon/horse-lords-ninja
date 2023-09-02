using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThief : MonoBehaviour
{
    // Pickpocketing mechanics
    public float pickpocketRange = 2f;
    public float pickpocketCooldownTime = 1f;
    public float pickPocketDetection = 15f; // Chance of detection
    public int minMoney = 1;
    public int maxMoney = 12;
    public bool pickPocketcooldown = false;
    public float pickpocketTime = 1f; //seconds it takes to pickpocket

    public PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        // Controls
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check anyone in range
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickpocketRange);
            if(colliders.Length == 1) // Only player in range
            {
                // No one in range
                Debug.Log("No one in range!");
                return;
            }
            // Check cooldown
            if (Time.time <  + pickpocketCooldownTime)
            {
                // Cooldown not over
                Debug.Log("Cooldown not over!");
                return;
            }
            // Pickpocket
            if(pickPocketcooldown == false)
            {
                // Run progress bar
                GameObject progressBar = Instantiate(playerMovement.progressBarPrefab, transform.position, Quaternion.identity);
                ProgressBar progressBarComponent = progressBar.GetComponent<ProgressBar>();
                progressBarComponent.timeToFill = pickpocketTime;
                // Place above player head (worldspace)
                progressBarComponent.trackTransform = transform;
                // Run timers
                StartCoroutine(PickpocketTimer());
                StartCoroutine(PickpocketCooldown());
            }
        }
    }

    private IEnumerator PickpocketTimer()
    {
        yield return new WaitForSeconds(pickpocketTime);
        Pickpocket();
    }

    private IEnumerator PickpocketCooldown()
    {
        pickPocketcooldown = true;
        yield return new WaitForSeconds(pickpocketCooldownTime);
        pickPocketcooldown = false;
    }

    bool Pickpocket(EnemyController enemy = null)
    {
        // Random roll detection chance
        float detectionRoll = Random.Range(0, 100);
        if (detectionRoll < pickPocketDetection)
        {
            // Detected
            Debug.Log("Detected!");
            return false;
        }
        // Not detected, action success
        Debug.Log("Success!");
        // Pickpocket
        playerMovement.coins += Random.Range(minMoney, maxMoney);
        return false;
    }
}
