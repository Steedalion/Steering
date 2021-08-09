using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : SteeringBehaviour
{
    public Transform target;
    
    void Update()
    {
        Seek(target.position);
    }

 
}