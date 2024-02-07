using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendedSteering : SteeringBehavior
{
    [Serializable]
    public struct BehaviorAndWeight
    {
        public SteeringBehavior behavior;
        public float weight;
    }

    public BehaviorAndWeight[] behaviors;

    // The overall maximum acceleration and rotation.
    public float maxAcceleration = 100f;
    public float maxRotation = 45f; // maxAngularVelocity

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput() {
            linear = new Vector3(0,0,0),
            angular = 0f,
        };

        // Accumulate all acceleration
        foreach (BehaviorAndWeight b in behaviors)
        {
            SteeringOutput output = b.behavior.getSteering();
            //Debug.Log("output (" + b.weight + ") - " + output.linear + " : " + output.angular);
            result.linear += b.weight * output.linear;
            if (!float.IsNaN(output.angular)) result.angular += b.weight * output.angular;
            //Debug.Log("result - " + result.linear + " : " + result.angular);
        }

        if (result.linear.magnitude > maxAcceleration) {
            result.linear.Normalize();
            result.linear *= maxAcceleration;
        }
        
        float angular = Mathf.Abs(result.angular);
        if (angular > maxRotation)
        {
            result.angular /= angular;
            result.angular *= maxRotation;
        }

        return result;
    }
}
