using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    IDLE,
    WALK,
    RUN,
    PAUSE,
    GOBACK,
    ATTACK,
    DEATH
}

public class EnemyController : MonoBehaviour {

    private float attack_Distance = 1.5f;
    private float alert_Attack_Distance = 8f;
    private float follwDistance = 15f;

    private float enemyToPlayerDistance;

    [HideInInspector]
    public EnemyState enemyCurrentState = EnemyState.IDLE;
    private EnemyState enemyLaststate = EnemyState.IDLE;

    private Transform playerTarget;
    private Vector3 initialPosition;

    private float move_Speed = 2f;
    private float walk_Speed = 1f;

    private CharacterController charController;
    private Vector3 whereTo_move = Vector3.zero;

    //attack
    private float currentAttackTime;
    private float waitAttackTime = 1f;

    private Animator anim;
    private bool finnished_Animation = true;
    private bool finnished_Mmovement = true;

    private NavMeshAgent navAgent;
    private Vector3 whereTo_Navigate;

    private EnemyHealth enemyHealth;

    // Use this for initialization
    void Awake () {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        initialPosition = transform.position;
        whereTo_Navigate = transform.position;

        enemyHealth = GetComponent<EnemyHealth>();

    }
	
	// Update is called once per frame
	void Update () {
		
        if (enemyHealth.health <= 0f)
        {
            enemyCurrentState = EnemyState.DEATH;
        }

        if (enemyCurrentState != EnemyState.DEATH)
        {
            enemyCurrentState = SetEnemyState(enemyCurrentState, enemyLaststate, enemyToPlayerDistance);

            if (finnished_Mmovement)
            {
                GetStateControl(enemyCurrentState);
            }
            else
            {
                if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    finnished_Mmovement = true;
                }
                else if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsTag("Atk1") ||
                    anim.GetCurrentAnimatorStateInfo(0).IsTag("Atk2"))
                {
                    anim.SetInteger("Atk", 0);
                }
            }
        }
        else
        {
            anim.SetBool("Death", true);
            charController.enabled = false;
            navAgent.enabled = false;
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                Destroy(gameObject, 2f);
            }
        }

	}

    EnemyState SetEnemyState(EnemyState curstate, EnemyState lastState, float enemyToPlayerDis)
    {
        enemyToPlayerDis = Vector3.Distance(transform.position, playerTarget.position);

        float initialDistance = Vector3.Distance(initialPosition, transform.position);

        if (initialDistance > follwDistance)
        {
            lastState = curstate;
            curstate = EnemyState.GOBACK;
        }
        else  if (enemyToPlayerDis <= attack_Distance)
        {
            lastState = curstate;
            curstate = EnemyState.ATTACK;
        }
        else  if (enemyToPlayerDis >= alert_Attack_Distance && lastState == EnemyState.PAUSE || lastState == EnemyState.ATTACK)
        {
            lastState = curstate;
            curstate = EnemyState.PAUSE;
        }
        else  if (enemyToPlayerDis <= alert_Attack_Distance && enemyToPlayerDis > attack_Distance)
        {
            if (curstate != EnemyState.GOBACK || lastState == EnemyState.WALK)
            {
                lastState = curstate;
                curstate = EnemyState.PAUSE;
            }
        }
        else if (enemyToPlayerDis > alert_Attack_Distance && lastState != EnemyState.GOBACK && lastState != EnemyState.PAUSE)
        {
            lastState = curstate;
            curstate = EnemyState.WALK;
        }

        return curstate;
    }


    void GetStateControl(EnemyState curState)
    {
        if (curState == EnemyState.RUN || curState == EnemyState.PAUSE)
        {
            if (curState != EnemyState.ATTACK)
            {
                Vector3 targetPositon = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                if (Vector3.Distance(transform.position, targetPositon) >= 2.1f)
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", true);
                    navAgent.SetDestination(targetPositon);

                }
            }
        }
        else if (curState == EnemyState.ATTACK)
        {
            anim.SetBool("Run", false);
            whereTo_move.Set(0,0,0);
            navAgent.SetDestination(transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTarget.position - transform.position), 5f * Time.deltaTime);

            if (currentAttackTime >= waitAttackTime)
            {
                int atkRange = Random.Range(1,3);
                anim.SetInteger("Atk", atkRange);
                finnished_Animation = false;
                currentAttackTime = 0f;
            }
            else
            {
                anim.SetInteger("Atk", 0);
                currentAttackTime += Time.deltaTime;
            }
        }
        else if (curState == EnemyState.GOBACK)
        {
            anim.SetBool("Run", true);
            Vector3 tragetPosition = new Vector3(initialPosition.x, transform.position.y, initialPosition.z);
            navAgent.SetDestination(tragetPosition);

            if (Vector3.Distance(tragetPosition, initialPosition) <= 3.5f)
            {
                enemyLaststate = curState;
                curState = EnemyState.WALK;
            }
        }
        else if (curState == EnemyState.WALK)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Run", false);

            if (Vector3.Distance(transform.position, whereTo_move) <= 2f)
            {
                whereTo_Navigate.x = Random.Range(initialPosition.x -5f, initialPosition.x + 5f);
                whereTo_Navigate.z = Random.Range(initialPosition.z - 5f, initialPosition.z + 5f);
            }
            else
            {
                navAgent.SetDestination(whereTo_Navigate);
            }
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
            whereTo_move.Set(0,0,0);
            navAgent.isStopped = true;
        }

    }
}
