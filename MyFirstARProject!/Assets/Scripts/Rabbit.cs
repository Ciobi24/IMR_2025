using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProximityDetector : MonoBehaviour
{
    public Animator animator;           
    public Transform targetCharacter;   
    public float proximityThreshold = 0.25f; 
    public float attackDuration = 3.0f; 

    private bool isAttacking = false;   
    private bool isDead = false;        
    private float attackTime = 0f;      

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();  
        }
    }

    void Update()
    {
        //distance 
        float distance = Vector3.Distance(transform.position, targetCharacter.position);

        //switch to Attack state (if not already attacking or dead)
        if (distance <= proximityThreshold && !isAttacking && !isDead)
        {
            TriggerAttack();
        }
        // go back to Idle state 
        else if (distance > proximityThreshold )
        {
            TriggerIdle();
        }

        // the character is attacking, track the duration
        if (isAttacking)
        {
            attackTime += Time.deltaTime;

            // attack time exceeds the threshold, trigger Die state
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
        isAttacking = true;  
        attackTime = 0f;     
    }

    void TriggerIdle()
    {
        animator.SetBool("InRange", false);
        animator.SetBool("isHit",false);
        isAttacking = false;  
        isDead=false;
        attackTime = 0f;      
    }

    void TriggerDie()
    {
        animator.SetBool("isHit",true);
        isAttacking = false;  
        isDead = true;       
    }
}
