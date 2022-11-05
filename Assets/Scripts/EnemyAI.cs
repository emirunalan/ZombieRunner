using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;
    EnemyHealth health;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        
    }

    void Update()
    {
        
        if(health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
        }
        
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        
        
        if(isProvoked)
        {
            EngageTarget();
        }

        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
        
        
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack",true);

    }

    private void ChaseTarget()
    {
        if(!GetComponent<EnemyHealth>().IsDead())
        {
            GetComponent<Animator>().SetBool("attack",false);
            GetComponent<Animator>().SetTrigger("move");
            navMeshAgent.SetDestination(target.position);
        }
        
    }

    private void FaceTarget()
    {
        if(!GetComponent<EnemyHealth>().IsDead())
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }


}
