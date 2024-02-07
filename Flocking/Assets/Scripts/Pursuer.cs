using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : Kinematic
{
    Pursue myMoveType;
    Face myPursueRotateType;
    LookWhereGoing myFleeRotateType;

    public bool flee = false;
    public GameObject myTarget;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new Pursue();
        myMoveType.character = this;
        myMoveType.target = myTarget;
        myMoveType.flee = flee;

        myPursueRotateType = new Face();
        myPursueRotateType.character = this;
        myPursueRotateType.target = myTarget;
		myPursueRotateType.maxRotation = maxAngularVelocity;

        myFleeRotateType = new LookWhereGoing();
        myFleeRotateType.character = this;
        myFleeRotateType.target = myTarget;
		myFleeRotateType.maxRotation = maxAngularVelocity;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = flee ? myFleeRotateType.getSteering().angular : myPursueRotateType.getSteering().angular;
        base.Update();
    }
}
