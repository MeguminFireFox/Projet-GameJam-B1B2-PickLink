using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public List<Transform> wayPoint; 

    NavMeshAgent navMeshAgent;
    Rigidbody rb;

    public int currentWaypointIndex = 0;

    [SerializeField]
    private Transform player;

    [SerializeField]
    bool SeePlayer;

    bool PQTUBUGFDP = true;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (SeePlayer == false)
        {
            if (PQTUBUGFDP == false)
            {
                rb.velocity = Vector3.zero;
                PQTUBUGFDP=true;
            }
            Walking();
        }
        else
        {
            FollowPlayer();
            PQTUBUGFDP = false;
        }
    }

    private void Walking()
    {
        if (wayPoint.Count == 0)
        {
     
            return;
        }

     
        float distanceToWaypoint = Vector3.Distance(wayPoint[currentWaypointIndex].position, transform.position);

        if (distanceToWaypoint <= 2)
        {
         
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoint.Count;
        }

        navMeshAgent.SetDestination(wayPoint[currentWaypointIndex].position);
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        navMeshAgent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (SeePlayer) return;
            SeePlayer = true;
            player = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (!SeePlayer) return;
            SeePlayer = false;
            player = null;
        }
    }
}
