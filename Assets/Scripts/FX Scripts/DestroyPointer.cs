using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPointer : MonoBehaviour {

    private Transform player;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (!player)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            
        }
        else
        {
            if (Vector3.Distance(transform.position, player.position) <= 1.1f)
            {
                Destroy(gameObject);
            }
        }

    }

}
