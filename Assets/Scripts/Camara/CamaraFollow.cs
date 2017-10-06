using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour {

    public float follow_Height = 8f;
    public float follow_Distance = 6f;

    private Transform player;

    private float target_Height;
    private float current_Height;
    private float curren_Rotation;



    // Use this for initialization
    void Awake () {
        
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
            target_Height = player.position.y + follow_Height;
            curren_Rotation = transform.eulerAngles.y;
            current_Height = Mathf.Lerp(transform.position.y, target_Height, 0.9f * Time.deltaTime);

            Quaternion euler = Quaternion.Euler(0f, curren_Rotation, 0f);

            Vector3 target_Position = player.position - (euler * Vector3.forward) * follow_Distance;

            target_Position.y = current_Height;
            transform.position = target_Position;
            transform.LookAt(player);
        }

    }
}
