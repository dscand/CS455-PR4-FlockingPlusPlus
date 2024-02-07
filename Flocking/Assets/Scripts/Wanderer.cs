using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Kinematic
{
    Wander myMoveType;
    LookWhereGoing myWanderRotateType;

    public float wanderRate = 2f;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new Wander();
        myMoveType.character = this;
		myMoveType.wanderRate = wanderRate;

        myWanderRotateType = new LookWhereGoing();
        myWanderRotateType.character = this;
		myWanderRotateType.maxRotation = maxAngularVelocity;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = myMoveType.getSteering();
		//steeringUpdate.angular = myWanderRotateType.getSteering().angular;
        base.Update();
    }
}
