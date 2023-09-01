using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // For objects that deal damage
    [SerializeField] int damage = 100;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the object collides with something that has a health component
        if (collision.gameObject.GetComponent<EnemyController>())
        {
            // Deal damage to the object
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
        StartCoroutine(DelayDestroy());
    }

    public IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
