using UnityEngine;

public class Seeker : SteeringBehaviour
{
    public Transform target;
    
    void Update()
    {
        Seek(target.position);
    }

 
}