using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : Kinematic
{
    PathFollow myMoveType;
    Face myRotateType;

	public float goalDistance = 0.2f;

	public GameObject[] targets;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new PathFollow();
        myMoveType.character = this;
        myMoveType.target = targets[0];
        myMoveType.targets = targets;
        myMoveType.goalDistance = goalDistance;

        myRotateType = new Face();
        myRotateType.character = this;
        myRotateType.target = targets[0];
		myRotateType.maxRotation = maxAngularVelocity;
    }

    // Update is called once per frame
    protected override void Update()
    {
		myRotateType.target = myMoveType.target;

        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
