using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour {

    private PlayerHealth playerHealth;


	// Use this for initialization
	void Awake () {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>(); 

    }

    private void OnEnable()
    {
        playerHealth.shielded = true;
    }

    private void OnDisable()
    {
        playerHealth.shielded = false;
    }
}
