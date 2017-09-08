using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFXDamage : MonoBehaviour {

    public LayerMask playerLayer;
    public float radius = 0.3f;
    public float demageCount = 10f;

    private PlayerHealth playerHealth;
    private bool collided;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, playerLayer);
        foreach (Collider c in hits)
        {
            playerHealth = c.gameObject.GetComponent<PlayerHealth>();
            collided = true;
        }

        if (collided)
        {
            playerHealth.TakeDamage(demageCount);
            enabled = false;
        }
    }
}
