using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProximityDetector : MonoBehaviour
{
    public Animator animator;          // Reference to the Animator
    public Transform targetCharacter;  // Reference to the other character (target)
    public float proximityThreshold = 0.25f;  // Proximity distance to switch to Attack

    private bool isAttacking = false;  // Flag to check if already in attack mode

    void Start()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();  // Get Animator if not assigned
        }
    }

    void Update()
    {
        // Check distance between the current character and the target
        float distance = Vector3.Distance(transform.position, targetCharacter.position);

        // If they are close enough, switch to Attack state
        if (distance <= proximityThreshold && !isAttacking)
        {
            TriggerAttack();
        }
        // If they are far away, go back to Idle state
        else if (distance > proximityThreshold && isAttacking)
        {
            TriggerIdle();
        }
    }

    void TriggerAttack()
    {
        animator.SetTrigger("Attack");
        isAttacking = true;  // Set attacking state
    }

    void TriggerIdle()
    {
        animator.SetTrigger("Idle");
        isAttacking = false;  // Set idle state
    }
}
