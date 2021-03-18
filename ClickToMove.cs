using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [Header("STATS")]
    public float attack_distance;
    public float attack_rate;

    private float next_attack;
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private Transform targetedEnemy;
    private bool enemy_clicked;
    private bool walking;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetButtonDown("Fire2"))
        {
            navMeshAgent.ResetPath();
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.tag == "Enemy")
                {
                    targetedEnemy = hit.transform;
                    enemy_clicked = true;
                }
                else
                {
                    walking = true;
                    enemy_clicked = false;
                    navMeshAgent.isStopped = false;
                    navMeshAgent.destination = hit.point;
                    anim.SetBool("isAttacking", false);
                }
            }
        }

        if (enemy_clicked)
        {
            MoveAndAttack();
        }
        else 
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                walking = false;
            }
            else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                walking = true;
            }
        }
        anim.SetBool("isWalking", walking);
    }

    public void MoveAndAttack()
    {
        if (targetedEnemy == null) 
        {
            return;
        }

        navMeshAgent.destination = targetedEnemy.position;

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance > attack_distance)
        {
            navMeshAgent.isStopped = false;
            walking = true;
            anim.SetBool("isAttacking", false);
        }
        else if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= attack_distance)
        {
            transform.LookAt(targetedEnemy);
            Vector3 dir_attack = targetedEnemy.transform.position - transform.position;

            if (Time.time > next_attack)
            {
                next_attack = Time.time + attack_rate;
                anim.SetBool("isAttacking", true);
            }

            navMeshAgent.isStopped = true;
            walking = false;
        }
    }
}
