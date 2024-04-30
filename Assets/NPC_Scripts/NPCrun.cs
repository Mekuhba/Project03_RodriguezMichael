using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCState
{
    MovingAwayFromPlayer,
    Stopped,
}

public class NPCrun : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float moveSpeed = 3f; // Speed at which the NPC moves away from the player
    public float withinDistance = 5f; // Distance at which the NPC starts moving away from the player

    private NPCState currentState = NPCState.Stopped;

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
                case NPCState.MovingAwayFromPlayer:
                    MoveAwayFromPlayer(distanceToPlayer);
                    break;
                case NPCState.Stopped:
                    StopMoving(distanceToPlayer);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Target (player) not set for NPCrun script!");
        }
    }

    void MoveAwayFromPlayer(float distanceToPlayer)
    {
        // Check if the player is within the distance where the NPC should start moving
        if (distanceToPlayer <= withinDistance)
        {
            // Calculate the direction from the NPC to the player
            Vector3 direction = (transform.position - target.position).normalized;

            // Move the NPC away from the player
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Transition to the Stopped state if the player is outside the distance
            currentState = NPCState.Stopped;
        }
    }

    void StopMoving(float distanceToPlayer)
    {
        // Check if the player is within the distance where the NPC should start moving
        if (distanceToPlayer <= withinDistance)
        {
            // Transition back to the MovingAwayFromPlayer state if the player re-enters the distance
            currentState = NPCState.MovingAwayFromPlayer;
        }
    }
}