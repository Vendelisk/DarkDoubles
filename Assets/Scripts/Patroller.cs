using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroller : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] patrolPoints;
    public float idleTime = 2f;
    private float baseSpeed;
    private bool waiting = false;
    private bool chasing = false;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
        baseSpeed = agent.speed;
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.isDead)
            agent.isStopped = true;
        else if (enemy.remCD <= 0)
            agent.isStopped = false;

        if (!chasing)
        {
            //print("moving");
            enemy.Move(agent.velocity.magnitude);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                StartCoroutine(NextDestination(idleTime));
            }
        }
    }

    private IEnumerator NextDestination(float wait)
    {
        if (!waiting)
        {
            waiting = true;
            yield return new WaitForSeconds(wait);
            if (!chasing)
            {
                agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
                waiting = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !PlayerCombat.Instance.dead && !enemy.isDead) {
            chasing = true;
            waiting = false;

            if (Vector3.Distance(transform.position, other.transform.position) <= enemy.GetAtkRange())
            {
                agent.isStopped = true;
                enemy.Attack(other.transform.position);
            }
            else
            {
                agent.destination = other.transform.position;
                agent.speed = baseSpeed * 3;
                enemy.Move(agent.velocity.magnitude * 2, other.transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            agent.speed = baseSpeed;
            chasing = false;
        }
    }
}
