using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public Animator animator;          
    public Transform targetCharacter;  
    public float proximityThreshold = 0.25f;  

    private bool isInRange = false;    

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();  
        }
    }

    void Update()
    {
        // distance
        float distance = Vector3.Distance(transform.position, targetCharacter.position);

        //(to switch to Attack)
        if (distance <= proximityThreshold && !isInRange)
        {
            SetInRange(true);
        }
        // (to switch back to Idle)
        else if (distance > proximityThreshold && isInRange)
        {
            SetInRange(false);
        }
    }

    void SetInRange(bool value)
    {
        animator.SetBool("InRange", value);  
        isInRange = value;  
    }
}
