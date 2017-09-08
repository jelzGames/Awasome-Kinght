using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float damageAmount = 10f;
    private Transform playerTarget;
    private Animator anim;
    private bool finnishedAttack = true;
    private float damageDistance = 2f;

    private PlayerHealth playerHealth;

	// Use this for initialization
	void Awake () {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        playerHealth = playerTarget.GetComponent<PlayerHealth>();

    }
	
	// Update is called once per frame
	void Update () {
        if (finnishedAttack)
        {
            DealDamage(CheckIsAttacking());
        }
        else
        {
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                finnishedAttack = true;
            }
        }
	}

    bool CheckIsAttacking()
    {
        bool isAttacking = false;

        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Atk1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Atk2"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                isAttacking = true;
                finnishedAttack = false;
            }
        }
        return isAttacking;
    }

    void DealDamage (bool isAttacking)
    {
        if (isAttacking)
        {
            if (Vector3.Distance(transform.position, playerTarget.position) <= damageDistance)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
