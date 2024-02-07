using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facer : Kinematic
{
    //Seek myMoveType;
    Face mySeekRotateType;
    public GameObject myTarget;

    // Start is called before the first frame update
    void Start()
    {
        //myMoveType = new Seek();
        //myMoveType.character = this;
        //myMoveType.target = myTarget;
        //myMoveType.flee = flee;

        mySeekRotateType = new Face();
        mySeekRotateType.character = this;
        mySeekRotateType.target = myTarget;
		mySeekRotateType.maxRotation = maxAngularVelocity;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        //steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = mySeekRotateType.getSteering().angular;
        base.Update();
    }
}
