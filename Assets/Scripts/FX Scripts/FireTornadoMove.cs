using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornadoMove : MonoBehaviour {

	public LayerMask enemyLayer;
    public float radius = 0.5f;
    public float damageCount = 10f;

    public GameObject fireExplosion;
    private bool colided;

    private EnemyHealth enemyHealth;

    private float speed = 3f;


    // Use this for initialization
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.LookRotation(player.transform.forward);

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForDemage();
    }

    void Move()
    {
        transform.Translate(Vector3.forward * (speed *Time.deltaTime));
    }

    void CheckForDemage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider c in hits)
        {
            enemyHealth = c.gameObject.GetComponent<EnemyHealth>();
            colided = true;
        }
        if (colided)
        {
            enemyHealth.TakeDamage(damageCount);
            Vector3 temp = transform.position;
            temp.y += 2f;
            Instantiate(fireExplosion, temp, Quaternion.identity);
            Destroy(gameObject);

        }

    }
}
