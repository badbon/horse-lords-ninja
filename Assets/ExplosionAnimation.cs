using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    public float switchSpriteTime = 0.1f;
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    public int currentSprite = 0;

    void Start()
    {
        
    }

    void Update()
    {
        StartCoroutine(SwitchSprite());
    }

    IEnumerator SwitchSprite()
    {
        yield return new WaitForSeconds(switchSpriteTime);
        if (currentSprite < sprites.Length)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[currentSprite];
            currentSprite++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
