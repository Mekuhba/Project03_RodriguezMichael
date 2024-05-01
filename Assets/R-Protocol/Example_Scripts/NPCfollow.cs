using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCfollow : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float moveSpeed = 3f; // Speed at which the NPC moves towards the player

    // Update is called once per frame
    void Update()
    {
        // Check if the target (player) is set
        if (target != null)
        {
            // Calculate the direction from the NPC to the player
            Vector3 direction = (target.position - transform.position).normalized;

            // Move the NPC towards the player
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Optionally, you can rotate the NPC to face the player
            // Remove the comment below if you want the NPC to rotate towards the player
            // transform.LookAt(target);
        }
        else
        {
            Debug.LogWarning("Target (player) not set for NPCFollow script!");
        }
    }
}