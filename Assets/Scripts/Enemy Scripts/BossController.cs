using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour {

    private Transform playerTarget;
    private BossStateChecker bossStateChecker;
    private NavMeshAgent navAgent;
    private Animator anim;

    private bool finnishedAttacking = true;
    private float currentAttackTime;
    private float waitAttackTime = 1f;

    
	// Use this for initialization
	void Awake() {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        bossStateChecker = GetComponent<BossStateChecker>();
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		if (finnishedAttacking)
        {
            GetStateControl();
        }
        else
        {
            anim.SetInteger("Atk", 0);

            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                finnishedAttacking = true;
            }
            
        }
	}

    void GetStateControl()
    {
        if (bossStateChecker.BossState == Boss_State.DEATH)
        {
            navAgent.isStopped = true;
            anim.SetBool("Death", true);
            Destroy(gameObject);
        }
        else
        {
            if (bossStateChecker.BossState == Boss_State.PAUSE)
            {
                navAgent.isStopped = false;
                anim.SetBool("Run", true);
                navAgent.SetDestination(playerTarget.position);
            }
            else if (bossStateChecker.BossState == Boss_State.ATTACK)
            {
                anim.SetBool("Run", false);
                Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTarget.position - transform.position), 5f * Time.deltaTime);

                if (currentAttackTime >= waitAttackTime)
                {
                    int atkRange = Random.Range(1, 5);
                    anim.SetInteger("Atk", atkRange);

                    currentAttackTime = 0f;
                    finnishedAttacking = false;
                }
                else
                {
                    anim.SetInteger("Atk", 0);
                    currentAttackTime += Time.deltaTime;

                }

            }
            else
            {
                navAgent.isStopped = true;
                anim.SetBool("Run", false);
            }
        }
    }
}
