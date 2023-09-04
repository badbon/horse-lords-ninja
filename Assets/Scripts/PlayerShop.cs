using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShop : MonoBehaviour
{
    // Player can bring up shop (upgrades, items, etc)
    public GameObject shopObj;
    public PlayerThief playerThief;
    
    void Start()
    {
        // Get player thief script - contains money related functions
        playerThief = GetComponent<PlayerThief>();
        shopObj.SetActive(false);
    }


    void Update()
    {
        // Listen for input
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Open shop
            if(shopObj.activeSelf == false)
            {
                shopObj.SetActive(true);
            }
            // Close shop
            else
            {
                shopObj.SetActive(false);
            }
        }
    }
}
