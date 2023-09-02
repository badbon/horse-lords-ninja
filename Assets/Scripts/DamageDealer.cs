using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // For objects that deal damage
    private float defaultDamage = 15;
    public float damage = 15;
    [SerializeField] float lifeTime = 5f;
    public bool isCrit = false; // Used for text color purposes
    public float critMultiplier = 3f; // Multiplier for critical hits

    public void Start()
    {
        defaultDamage = damage;
        StartCoroutine(DelayDestroy(lifeTime));
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the object collides with something that has a health component
        if (collision.gameObject.GetComponent<EnemyController>())
        {
            // Deal damage to the object
            if(isCrit)
            {
                Debug.Log("Crit!");
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage * critMultiplier);
                damage = defaultDamage;
            }
            else
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            }
            
            StartCoroutine(DelayDestroy(0.1f));
        }
    }

    public IEnumerator DelayDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
