using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{

    NavMeshAgent agent;

    Transform target;

    Animator animator;

    public GameObject cursorPrefab;

    private GameObject dest;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        this.animator.SetFloat("Speed", 0);
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);            
        }

        if (agent.isStopped || agent.transform.position == agent.destination)
        {
            Destroy(dest);
        }

        //UnityEngine.Debug.Log("Speed: " + Mathf.Clamp(agent.velocity.magnitude, 0, 1));
        this.animator.SetFloat("Speed", Mathf.Clamp(agent.velocity.magnitude, 0, 1));
    }

    public void Stop() {
        agent.isStopped = true;        
        StopFollowingTarget();

        agent.SetDestination(agent.transform.position);
        agent.isStopped = false;

        this.animator.SetInteger("Speed", 0);
    }

    public void MoveToPoint(Vector3 point)
    {
        Destroy(dest);

        agent.SetDestination(point);
        
        dest = Instantiate(cursorPrefab) as GameObject;
        dest.transform.position = point;
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius;

        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;

        target = null;
    }
}
