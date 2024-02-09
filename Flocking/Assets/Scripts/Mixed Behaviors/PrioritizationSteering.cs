using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritizationSteering : BlendedSteering
{
    public new struct BehaviorAndWeight
    {
        public BlendedSteering behavior;
        //public float weight;
    }

    public new BehaviorAndWeight[] behaviors;

    public float gamma = 0.1f;

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput() {
            linear = new Vector3(0,0,0),
            angular = 0f,
        };

        foreach (BehaviorAndWeight b in behaviors)
        {
            SteeringOutput output = b.behavior.getSteering();
            if (output.linear.magnitude > gamma || Mathf.Abs(output.angular) > gamma) {
                result = output;
                break;
            }
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
