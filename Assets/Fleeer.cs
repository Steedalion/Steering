using UnityEngine;

public class Fleeer : TargetedSteer
{
    
    void Update()
    {
        Flee(target.position);
    }
}