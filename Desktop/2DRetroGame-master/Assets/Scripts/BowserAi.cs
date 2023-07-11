using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowserAi : MonoBehaviour
{
    // The target that the AI will chase
    public Transform target;

    // The speed at which the AI will move
    public float speed = 5f;

    // The distance at which the AI will start chasing the target
    public float chaseDistance = 10f;

    // The distance at which the AI will stop chasing the target
    public float stopChasingDistance = 15f;

    // The distance at which the AI will attack the target
    public float attackDistance = 2f;

    // The amount of damage the AI will deal when it attacks
    public int attackDamage = 1;

    // The amount of time the AI will wait between attacks
    public float attackDelay = 1f;

    // The current time
    private float currentTime = 0f;

    // The Rigidbody component attached to the AI
    public Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Calculate the distance to the target
        float distance = Vector3.Distance(transform.position, target.position);

        // If the target is closer than the chase distance, start chasing
        if (distance < chaseDistance)
        {
            // Set the AI's velocity to move towards the target
            rb.velocity = (target.position - transform.position).normalized * speed;

            // If the target is closer than the attack distance, attack
            if (distance < attackDistance)
            {
                // If enough time has passed since the last attack, attack again
                if (currentTime > attackDelay)
                {
                    // Attack the target
                    target.SendMessage("TakeDamage", attackDamage);

                    // Reset the current time
                    currentTime = 0f;
                }
            }
        }
        // If the target is farther than the stop chasing distance, stop chasing
        else if (distance > stopChasingDistance)
        {
            // Stop moving
            rb.velocity = Vector3.zero;
        }

        // Increment the current time
        currentTime += Time.deltaTime;
    }
}