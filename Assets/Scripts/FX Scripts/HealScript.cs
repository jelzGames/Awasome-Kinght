using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : MonoBehaviour {

    public float healAmount = 20f;

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().HealPlayer(healAmount);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
