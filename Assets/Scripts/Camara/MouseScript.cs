using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;


public class MouseScript : NetworkBehaviour {

    public Texture2D cursorTexture;
    private CursorMode mode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;

    public GameObject mousePoint;
    private GameObject instatiatedMouse;

    public EventSystem eventSystem;

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

		if (!player)
		{
			return;
		}
        	

        Cursor.SetCursor(cursorTexture, hotSpot, mode);

        if (Input.GetMouseButtonUp(0) && !eventSystem.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Ray ray =  GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider is TerrainCollider)
                {
                    Vector3 temp = hit.point;
                    temp.y = 0.25f;

                    if (instatiatedMouse != null)
                    {
                        Destroy(instatiatedMouse);
                    }       
                    instatiatedMouse = Instantiate(mousePoint) as GameObject;
                    instatiatedMouse.transform.position = temp;
                    
                    
                }
            }
        }
    }
}
