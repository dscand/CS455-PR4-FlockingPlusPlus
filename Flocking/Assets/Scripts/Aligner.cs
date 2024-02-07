using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aligner : Kinematic
{
    Pursue myMoveType;
    Align myPursueRotateType;
    public GameObject myTarget;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new Pursue();
        myMoveType.character = this;
        myMoveType.target = myTarget;
        myMoveType.flee = false;

        myPursueRotateType = new Align();
        myPursueRotateType.character = this;
        myPursueRotateType.target = myTarget;
		myPursueRotateType.maxRotation = maxAngularVelocity;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myPursueRotateType.getSteering().angular;
        base.Update();
    }
}
