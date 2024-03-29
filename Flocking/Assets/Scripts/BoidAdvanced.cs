using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAdvanced : Kinematic
{
	PrioritizationSteering myMoveType;

	public float flockDistance = 20f;
	public float visionAngle = 45f;
	public float avoidDistance = 5f;

	Separation separate;
	public float weightSeparate = 0.1f;

	Align2 align;
	public float weightAlign = 0.2f;

	Arrive2 arrive;
	public float weightArrive = 0.4f;

	LookWhereGoing lookWhereGoing;
	public float weightLookWhereGoing = 0.4f;

	Seek seek;
	public float weightSeek = 0.2f;
	public GameObject seekTarget;

	ObstacleAvoidance2 obstacleAvoidance;
	private float weightObstacleAvoidance = 1f;


	// Start is called before the first frame update
	void Start()
	{
		separate = new Separation();
		separate.character = this;

		align = new Align2();
		align.character = this;

		arrive = new Arrive2();
		arrive.character = this;

		lookWhereGoing = new LookWhereGoing();
		lookWhereGoing.character = this;
		lookWhereGoing.maxRotation = maxAngularVelocity;

		seek = new Seek();
		seek.character = this;
		seek.flee = false;

		obstacleAvoidance = new ObstacleAvoidance2();
		obstacleAvoidance.character = this;


		separate.targets = null;
		align.target = Vector3.zero;
		arrive.target = Vector3.zero;
		
		seek.target = seekTarget;


		BlendedSteering.BehaviorAndWeight[] behaviorsAvoidance = {
			new BlendedSteering.BehaviorAndWeight{
				behavior = obstacleAvoidance,
				weight = weightObstacleAvoidance
			},
		};

		BlendedSteering.BehaviorAndWeight[] behaviorsFlock = {
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
			new BlendedSteering.BehaviorAndWeight{
				behavior = seek,
				weight = weightSeek
			},
		};


		PrioritizationSteering.BehaviorAndWeight[] behaviors = {
			new PrioritizationSteering.BehaviorAndWeight{
				behavior = new BlendedSteering {
					behaviors = behaviorsAvoidance
				}
			},
			new PrioritizationSteering.BehaviorAndWeight{
				behavior = new BlendedSteering {
					behaviors = behaviorsFlock
				}
			},
		};

		myMoveType = new PrioritizationSteering
		{
			behaviors = behaviors,
		};
	}

	// Update is called once per frame
	protected override void Update()
	{
		List<Kinematic> flock = new List<Kinematic>();
		List<Kinematic> avoid = new List<Kinematic>();
		Vector3 center = Vector3.zero;
		Vector3 heading = Vector3.zero;

		GameObject[] objs = GameObject.FindGameObjectsWithTag("Boid");
		foreach (var obj in objs)
		{
			Vector3 directionToTarget = obj.transform.position - transform.position;
			if (directionToTarget.magnitude < avoidDistance)
			{
				avoid.Add(obj.GetComponent<Kinematic>());
			}
			if (directionToTarget.magnitude < flockDistance && Mathf.Abs(Vector3.Angle(transform.forward, directionToTarget)) < visionAngle)
			{
				avoid.Add(obj.GetComponent<Kinematic>());
				flock.Add(obj.GetComponent<Kinematic>());
				center += obj.transform.position;
				heading += obj.transform.eulerAngles;
			}
		}

		if (flock.Count > 0) {
			center /= flock.Count;
			heading /= flock.Count;


			separate.targets = avoid.ToArray();
			align.target = heading;
			arrive.target = center;


			steeringUpdate = myMoveType.getSteering();
			steeringUpdate.linear.y = 0;
			base.Update();
		}
		else {
			steeringUpdate = new SteeringOutput();
			base.Update();
		}
	}
}
