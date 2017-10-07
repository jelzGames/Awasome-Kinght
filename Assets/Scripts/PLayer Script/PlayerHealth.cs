using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

    public float health = 100f;
    private bool isShielded;

    private Animator anim;

    private Image health_Img;

    public bool shielded
    {
        get
        {
            return isShielded;
        }
        set
        {
            isShielded = value;
        }
    }

    public void TakeDamage(float amount)
    {
		if (!isLocalPlayer)
		{
			return;
		}

        if (!isShielded)
        {
            health -= amount;

            health_Img.fillAmount = health / 100f;

            if (health <= 0f)
            {
                anim.SetBool("Death", true);
                if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95)
                {
                   
                    Destroy(gameObject);
                    SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
                }
            }
        }
       
    }


    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();

        health_Img = GameObject.Find("Health Icon").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HealPlayer(float healAmount)
    {
		if (!isLocalPlayer)
		{
			return;
		}

        health += healAmount;

        if (health > 100f)
        {
            health = 100f;
        }

        health_Img.fillAmount = health / 100f;
    }

    
}
