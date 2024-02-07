using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidFlock : MonoBehaviour
{
	public GameObject centerOfMass;

	public List<Kinematic> boids;

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log("Boids Count: " + boids.Count.ToString());
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 meanPosition = Vector3.zero;
		Vector3 meanRotation = Vector3.zero;
		foreach(Kinematic boid in boids)
		{
			meanPosition += boid.transform.position;
			meanRotation += boid.transform.eulerAngles;
		}
	
		centerOfMass.transform.position = meanPosition / boids.Count;
		centerOfMass.transform.eulerAngles = meanRotation / boids.Count;
	}
}
