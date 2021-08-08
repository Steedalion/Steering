using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float radius=1;
    public float distance=2;
    public float jitter=0.5f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position+transform.forward*distance,radius-jitter);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position+transform.forward*distance,radius+jitter);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
