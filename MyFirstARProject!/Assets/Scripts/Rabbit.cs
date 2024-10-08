using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProximityDetector : MonoBehaviour
{
    public Animator animator;           // Reference to the Animator
    public Transform targetCharacter;   // Reference to the other character (target)
    public float proximityThreshold = 0.25f; // Proximity distance to switch to Attack
    public float attackDuration = 3.0f; // Duration in seconds before switching to Die state

    private bool isAttacking = false;   // Flag to check if already in attack mode
    private bool isDead = false;        // Flag to check if character is dead
    private float attackTime = 0f;      // Timer for tracking attack duration

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

        // If they are close enough, switch to Attack state (if not already attacking or dead)
        if (distance <= proximityThreshold && !isAttacking && !isDead)
        {
            TriggerAttack();
        }
        // If they are far away, go back to Idle state (if not dead)
        else if (distance > proximityThreshold )
        {
            TriggerIdle();
        }

        // If the character is attacking, track the duration
        if (isAttacking)
        {
            attackTime += Time.deltaTime;

            // If the attack time exceeds the threshold, trigger the Die state
            if (attackTime >= attackDuration && distance <= proximityThreshold)
            {
                TriggerDie();
            }
        }
    }

    void TriggerAttack()
    {
        animator.SetBool("InRange", true);
        animator.SetBool("isHit",false);
         isAttacking = true;  // Set attacking state
        attackTime = 0f;     // Reset the attack timer
    }

    void TriggerIdle()
    {
         animator.SetBool("InRange", false);
         animator.SetBool("isHit",false);
        isAttacking = false;  // Set idle state
        isDead=false;
        attackTime = 0f;      // Reset the attack timer
    }

    void TriggerDie()
    {
        animator.SetBool("isHit",true);
        isAttacking = false;  // Stop attacking
        isDead = true;        // Set dead state
    }
}
