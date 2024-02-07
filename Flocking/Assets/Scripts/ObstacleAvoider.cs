using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoider : Kinematic
{
    ObstacleAvoidance myMoveType;
    LookWhereGoing myRotateType;
    public GameObject myTarget;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new ObstacleAvoidance();
        myMoveType.character = this;
        myMoveType.target = myTarget;

        myRotateType = new LookWhereGoing();
        myRotateType.character = this;
		myRotateType.maxRotation = maxAngularVelocity;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();

        SteeringOutput avoidance = myMoveType.getSteering();
        if (avoidance != null) {
            steeringUpdate.linear = myMoveType.getSteering().linear;
        }

        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
