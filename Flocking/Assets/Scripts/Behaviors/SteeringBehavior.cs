using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class SteeringBehavior
{
    public abstract SteeringOutput getSteering();
}
