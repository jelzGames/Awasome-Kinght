using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class mobile : MonoBehaviour {

    private NetworkManager manager;
    private Canvas canvas;
    private InputField input;

	// Use this for initialization
	void Start () {

        manager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
        canvas = GameObject.Find("NetworkCanvas").GetComponent<Canvas>();
        input = GameObject.Find("InputMobile").GetComponent<InputField>();
        canvas.enabled = false;
    }
	
	// Update is called once per frame
	public void HideShow() {
		if (canvas.enabled)
		{
            canvas.enabled = false;
	    }
        else
		{
			canvas.enabled = true;

	    }
	}

	public void connect()
	{
        
        manager.networkAddress = input.text;
        manager.networkPort = 7777;
        manager.StartClient();
        canvas.enabled = false;

    }
}
