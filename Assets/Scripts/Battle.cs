using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public Animator animator;          // Reference to the Animator
    public Transform targetCharacter;  // Reference to the other character (target)
    public float proximityThreshold = 0.25f;  // Proximity distance to switch to Attack

    private bool isInRange = false;    // Flag for detecting if the character is in range

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();  // Get Animator if not assigned
        }
    }

    void Update()
    {
        // Check distance between the current character and the target
        float distance = Vector3.Distance(transform.position, targetCharacter.position);

        // If they are close enough, set InRange to true (to switch to Attack)
        if (distance <= proximityThreshold && !isInRange)
        {
            SetInRange(true);
        }
        // If they are far away, set InRange to false (to switch back to Idle)
        else if (distance > proximityThreshold && isInRange)
        {
            SetInRange(false);
        }
    }

    void SetInRange(bool value)
    {
        animator.SetBool("InRange", value);  // Set the InRange parameter in the Animator
        isInRange = value;  // Update the local flag
    }
}
