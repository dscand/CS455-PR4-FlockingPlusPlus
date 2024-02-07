using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : Kinematic
{
	BlendedSteering myMoveType;
	public BoidFlock flock;

	Separation separate;
	public float weightSeparate = 0.1f;

	Align align;
	public float weightAlign = 0.2f;

	Arrive arrive;
	public float weightArrive = 0.4f;

	LookWhereGoing lookWhereGoing;
	public float weightLookWhereGoing = 0.4f;


	// Start is called before the first frame update
	void Start()
	{
		separate = new Separation();
		separate.character = this;

		align = new Align();
		align.character = this;

		arrive = new Arrive();
		arrive.character = this;

		lookWhereGoing = new LookWhereGoing();
		lookWhereGoing.character = this;
		lookWhereGoing.maxRotation = maxAngularVelocity;


		var targets = flock.boids;
		//targets.Remove(gameObject.GetComponent<Kinematic>());

		separate.targets = targets.ToArray();
		align.target = flock.centerOfMass;
		arrive.target = flock.centerOfMass;

		BlendedSteering.BehaviorAndWeight[] behaviors = {
			new BlendedSteering.BehaviorAndWeight{
				behavior = separate,
				weight = weightSeparate
			},
			new BlendedSteering.BehaviorAndWeight{
			    behavior = align,
				weight = weightAlign
			},
			new BlendedSteering.BehaviorAndWeight{
			    behavior = arrive,
				weight = weightArrive
			},
			new BlendedSteering.BehaviorAndWeight{
			    behavior = lookWhereGoing,
				weight = weightLookWhereGoing
			},
		};

		myMoveType = new BlendedSteering
		{
			behaviors = behaviors,
		};
	}

	// Update is called once per frame
	protected override void Update()
	{
		steeringUpdate = myMoveType.getSteering();
		steeringUpdate.linear.y = 0;
		base.Update();
	}
}
