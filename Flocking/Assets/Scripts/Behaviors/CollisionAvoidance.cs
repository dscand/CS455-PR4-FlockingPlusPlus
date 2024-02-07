using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SteeringBehavior
{
    public Kinematic character;
    public Kinematic[] targets;

    float maxAcceleration = 100f;

    // The collision radius of a character (assuming all characters have the same radius here).
    float radius = 0.5f;

    public override SteeringOutput getSteering()
    {
        // 1. Find the target that’s closest to collision
        // Store the first collision time.
        float shortestTime = Mathf.Infinity;

        // Store the target that collides then, and other data that we
        // will need and can avoid recalculating.
        Kinematic firstTarget = null;
        float firstMinSeparation = 0;
        float firstDistance = 0;
        Vector3 firstRelativePos = Vector3.zero;
        Vector3 firstRelativeVel = Vector3.zero;

        // Loop through each target
        foreach (Kinematic target in targets) {
            // Calculate the time to collision.
            Vector3 relativePos = character.transform.position - target.transform.position;
            Vector3 relativeVel = character.linearVelocity - target.linearVelocity;
            float relativeSpeed = relativeVel.magnitude;
            float timeToCollision = -Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);

            // Check if it is going to be a collision at all.
            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * timeToCollision;
            if (minSeparation > 2 * radius) {
                continue;
            }

            // Check if it is the shortest.
            if (timeToCollision > 0 && timeToCollision < 2f && timeToCollision < shortestTime) {
                // Store the time, target, and other data.
                shortestTime = timeToCollision;
                firstTarget = target;
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }
        }

        // 2. Calculate the steering
        // If we have no target, then exit.
        if (firstTarget == null) {
            return null;
        }


        Vector3 targetRelativePos;
        // If we're going to hit exactly, or if we're already
        // colliding, then do the steering based on current position.
        if (firstMinSeparation <= 0 || firstDistance < 2 * radius) {
            targetRelativePos = character.transform.position - firstTarget.transform.position;
        }

        // Otherwise calculate the future relative position.
        else {
            targetRelativePos = firstRelativePos + firstRelativeVel * shortestTime;
        }


        // Avoid the target.
        targetRelativePos.Normalize();

        SteeringOutput result = new SteeringOutput();
        result.linear = targetRelativePos * maxAcceleration;
        result.angular = 0;
        return result;
    }
}
