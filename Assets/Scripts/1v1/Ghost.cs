using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] float fleeRadius = 5f;
    NavMeshAgent agent;
    Animator anim;
    private bool isChasing = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckSpeed();
        if (isChasing)
        {
            Chase();
        }
        else
        {
            Flee();
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    private void Flee()
    {
        Vector3 position = player.transform.position;
        if (Vector3.Distance(position, transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (transform.position - position).normalized;
            Vector3 newgoal = transform.position + fleeDirection * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newgoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                agent.angularSpeed = 500;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("tag");
        if (other.gameObject.tag == "Player")
        {
            isChasing = !isChasing;
        }
    }

    private void CheckSpeed()
    {
        if (agent.velocity.magnitude > 0)
        {
            anim.SetTrigger("isFlying");
        }

        else
        {
            anim.SetTrigger("isIdle");
        }
    }

    public bool GetState()
    {
        return isChasing;
    }
}
