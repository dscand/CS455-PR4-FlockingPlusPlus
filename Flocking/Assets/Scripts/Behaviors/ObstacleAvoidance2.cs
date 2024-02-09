﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance2 : Seek2
{
    // The min distance to a wall
    float avoidDistance = 1f;

    // The distance to look ahead for a collision
    float lookahead = 2f;

    public override SteeringOutput getSteering()
    {
        Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * lookahead, Color.red);
        // 1. Calculate the target to delegate to seek
        // Calculate the collision ray vector.
        RaycastHit hit;
        if (Physics.Raycast(character.transform.position, character.linearVelocity, out hit, lookahead)) {
            // 2. Create a target and delegate to Seek.
            target = hit.point + hit.normal * avoidDistance;
            return base.getSteering();
        }
        else {
            return new SteeringOutput();
        }
    }
}
