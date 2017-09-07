using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

   
    public float health = 100f;

    private Image health_Img;

    public void TakeDamage(float amount)
    {
        health -= amount;

        health_Img.fillAmount = health / 100f;

        
        Debug.Log(health);
        if (amount <= 0)
        {

        }
    }


	// Use this for initialization
	void Awake () {
		if (tag == "Boss")
        {
            health_Img = GameObject.Find("Health Foreground Boss").GetComponent<Image>();
        }
        else
        {
            health_Img = GameObject.Find("Health Foreground").GetComponent<Image>();

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
