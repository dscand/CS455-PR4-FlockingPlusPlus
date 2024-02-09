﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek2 : SteeringBehavior
{
    public Kinematic character;
    public Vector3 target;

    float maxAcceleration = 100f;

    public bool flee = false;

    protected virtual Vector3 getTargetPosition()
    {
        return target;
    }

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
        Vector3 targetPosition = getTargetPosition();
        if (targetPosition == Vector3.positiveInfinity)
        {
            return null;
        }

        // Get the direction to the target
        if (flee)
        {
            //result.linear = character.transform.position - target.transform.position;
            result.linear = character.transform.position - targetPosition;
        }
        else
        {
            //result.linear = target.transform.position - character.transform.position;
            result.linear = targetPosition - character.transform.position;
        }

        // give full acceleration along this direction
        result.linear.y = 0;
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }
}
