using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separator : Kinematic
{
    Separation myMoveType;
    LookWhereGoing mySeparationRotateType;
    LookWhereGoing myFleeRotateType;

    public bool flee = false;
	public Kinematic[] targets;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new Separation();
        myMoveType.character = this;
        myMoveType.targets = targets;
        //myMoveType.flee = flee;

        mySeparationRotateType = new LookWhereGoing();
        mySeparationRotateType.character = this;
        //mySeparationRotateType.target = myTarget;

        myFleeRotateType = new LookWhereGoing();
        myFleeRotateType.character = this;
        //myFleeRotateType.target = myTarget;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = flee ? myFleeRotateType.getSteering().angular : mySeparationRotateType.getSteering().angular;
        base.Update();
    }
}
