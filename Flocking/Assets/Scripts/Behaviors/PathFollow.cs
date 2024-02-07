using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : Pursue
{
    public float goalDistance = 1f;
	public GameObject[] targets;
	int targetIndex = 0;
	
	protected override Vector3 getTargetPosition()
    {
		Vector3 directionToTarget = target.transform.position - character.transform.position;
        float distanceToTarget = directionToTarget.magnitude;

		if (distanceToTarget <= goalDistance) {
			if (targetIndex >= targets.Length - 1) {
				targetIndex = 0;
			}
			else {
				targetIndex++;
			}
			target = targets[targetIndex];
		}

        return base.getTargetPosition();
    }
}
