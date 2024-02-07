using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    //public Kinematic character;

	public float wanderRate = 2f;

    float maxAcceleration = 100f;

	private float randomBinomial()
	{
		return Random.Range(0f,1f) - Random.Range(0f,1f);
	}

    public override SteeringOutput getSteering()
    {
		SteeringOutput result = new SteeringOutput();

		result.angular = randomBinomial() * wanderRate;

		float theta = character.transform.eulerAngles.y * Mathf.Deg2Rad;
        result.linear = new Vector3(Mathf.Sin(theta), 0, Mathf.Cos(theta));

        // give full acceleration along this direction
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        return result;
    }
}
