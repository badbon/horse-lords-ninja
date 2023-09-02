using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image bar;
    public float fillAmount;
    public float timeToFill = 1f; 
    public bool fillUp = true; // Will the bar fill up, or down?
    public Transform trackTransform; // Optional - will follow this transform if exists

    void Start()
    {
        // Attach to WorldSpace canvas
        transform.SetParent(GameObject.Find("WorldSpace").transform);

        if(fillUp)
        {
            fillAmount = 0f;
        }
        else
        {
            fillAmount = 1f;
        }
    }

    void Update()
    {
        if(trackTransform)
        {
            transform.position = trackTransform.position + new Vector3(0, 0.65f, 0);
        }
        if(fillUp)
        {
            if(fillAmount < 1f)
            {
                fillAmount += Time.deltaTime / timeToFill;
                // Detect completion
                if(fillAmount >= 1f)
                {
                    StartCoroutine(HoldDestroy());
                }
            }
        }
        else
        {
            if(fillAmount > 0f)
            {
                fillAmount -= Time.deltaTime / timeToFill;
                // Detect completion
                if(fillAmount <= 0f)
                {
                    StartCoroutine(HoldDestroy());
                }
            }
        }

        bar.fillAmount = fillAmount;


    }

    private IEnumerator HoldDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
