using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public Transform[] positions;
    public double counterTime = 4;

    private int index = 0;
    private double counter = 4;

    void Start()
    {
        counter = counterTime;
         transform.position = positions[0].position;
    }

    void FixedUpdate()
    {
        if (counter <= 0) {
            index++;
            if (index > positions.Length - 1) {
                index = 0;
            }
            transform.position = positions[index].position;
            counter = counterTime;
        }
        else {
            counter -= Time.deltaTime;
        }
    }
}
