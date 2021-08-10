using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Wanderer : SteeringBehaviour
{
    public float radius = 1;
    public float distance = 2;
    public float jitter = 0.5f;
    private Vector3 pointOnCircle;

    private float RandomJitter => Random.Range(-jitter, jitter);


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward * distance, radius - jitter);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + transform.forward * distance, radius + jitter);
    }

    void Update()
    {
        Wander();
    }

    private void Wander()
    {
        pointOnCircle += new Vector3(RandomJitter, 0, RandomJitter);
        pointOnCircle = pointOnCircle.normalized * radius;
        Seek(transform.position + pointOnCircle + distance * transform.forward);
    }
}

public class TargetedSteer : SteeringBehaviour
{
    public Transform target;
    NavMeshAgent targetAgent;
    protected Vector3 toTarget => target.position - transform.position;

    protected override void Awake()
    {
        base.Awake();
        if (target != null)
        {
            targetAgent = target.GetComponent<NavMeshAgent>();
        }
    }

    protected void Pursuit(Transform pursuitTarget)
    {
        float relativeHeading = Vector3.Dot(transform.forward, pursuitTarget.forward);
        if (relativeHeading < -0.95) ;
        {
            Seek(pursuitTarget.position);
        }

        float targetSpeed = targetAgent.speed;
        float timeToCollision = toTarget.magnitude / (speed + targetSpeed);
        Vector3 predictedPosition = pursuitTarget.position + pursuitTarget.forward * (targetSpeed * timeToCollision);
        Seek(predictedPosition);
    }

    public void Evade(Transform evadeTarget)
    {
        var targetSpeed = targetAgent.speed;
        float timeToCollision = toTarget.magnitude / (speed + targetSpeed);
        Vector3 predictedPosition = evadeTarget.position + evadeTarget.forward * (targetSpeed * timeToCollision);
        Flee(predictedPosition);
    }
}

[RequireComponent(typeof(NavMeshAgent))]
public class SteeringBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    protected float speed => agent.speed;
    private Vector3 destination = new Vector3();
    public float repelRate = -0.5f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destination, 0.5f);
        Gizmos.DrawLine(transform.position, destination);
    }

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected void Seek(Vector3 targetPosition)
    {
        destination = targetPosition;
        setDestination(destination);
    }

    protected void Flee(Vector3 targetPosition)
    {
        Vector3 toTarget = targetPosition - transform.position;
        destination = transform.position - toTarget;
        setDestination(destination);
    }

    protected virtual void setDestination(Vector3 agentDistination)
    {
        //create empty path
        NavMeshPath navMeshPath = new NavMeshPath();
        //create path and check if it can be done
        // and check if navMeshAgent can reach its target
        if (agent.CalculatePath(agentDistination, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            //move to target
            agent.SetPath(navMeshPath);
        }
        else
        {
            NavMeshHit nearest;
            if (NavMesh.SamplePosition(transform.position, out nearest,100, -1))
            {
                Vector3 nearestPosition = nearest.position;
                Debug.DrawLine(transform.position+Vector3.up, nearest.position, Color.cyan);

                NavMeshHit blockedHit;
                if (NavMesh.Raycast(transform.position, transform.forward,out blockedHit, NavMesh.AllAreas))
                {
                    Debug.DrawLine(transform.position,blockedHit.position, Color.green);
                }
                setDestination(Repel(nearest.position));
            }
        }
    }

    private Vector3 Repel(Vector3 nearestPosition)
    {
        Vector3 toNearestPoint = nearestPosition - transform.position;
        return nearestPosition += repelRate * toNearestPoint;
    }
}

public static class MyExtensions
{
    public static Vector3 RandomXY(this Vector3 vec, float range)
    {
        return new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
    }
    public static Vector3 RandomXY(float range)
    {
        return new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
    }
}