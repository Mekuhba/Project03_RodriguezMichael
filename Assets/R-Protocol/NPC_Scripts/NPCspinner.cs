using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCSpin
{
    Spinning,
    Stopped,
}

public class NPCspinner : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float WithinDistance = 5f; // Distance at which the NPC stops moving

    private NPCSpin currentState = NPCSpin.Spinning;
    private float rotationSpeed = 720f; // Speed at which the NPC rotates when the player is outside the distance

    // Update is called once per frame
    void Update()
    {
        // Check if the target (player) is set
        if (target != null)
        {
            // Calculate the distance between the NPC and the player
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            // Handle NPC behavior based on current state
            switch (currentState)
            {
                case NPCSpin.Spinning:
                    SpinFast(distanceToPlayer);
                    break;
                case NPCSpin.Stopped:
                    StopMoving(distanceToPlayer);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Target (player) not set for NPCrun script!");
        }
    }

    void SpinFast(float distanceToPlayer)
    {
        // Check if the player is within the run distance
        if (distanceToPlayer <= WithinDistance)
        {
            currentState = NPCSpin.Spinning;
        }
        else
        {
            // Calculate the direction from the NPC to the player
            Vector3 direction = (target.position - transform.position).normalized;

            // Rotate the NPC rapidly around its Y axis
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    void StopMoving(float distanceToPlayer)
    {
        // Check if the player is outside the stop distance
        if (distanceToPlayer > WithinDistance)
        {
            currentState = NPCSpin.Stopped;

            // Stop the rotation when the player is within distance
            transform.rotation = Quaternion.identity;
        }
    }
}
