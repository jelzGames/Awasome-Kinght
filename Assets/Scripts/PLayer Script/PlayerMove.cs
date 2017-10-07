using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {

    private Animator anim;
    private CharacterController charController;
    private CollisionFlags collisionFlags = CollisionFlags.None;

    private float moveSpeed = 5f;
    private bool canMove;
    private bool finished_Movement = true;
    private Vector3 target_Pos = Vector3.zero;
    private Vector3 player_Move = Vector3.zero;
    private float player_ToPointDistance;
    private float gravity = 9.8f;
    private float height;

    public EventSystem eventSystem;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //MoveThePlayer();
        //charController.Move(player_Move);
        if (!isLocalPlayer)
        {
            return;
        }

        if (!eventSystem)
        {
            eventSystem = GameObject.FindObjectOfType<EventSystem>();
        }
        else
        {
            CalculateHeight();
            CheckIfFinishedMovement();
        }
    }

    public override void OnStartLocalPlayer()
    {
        GetComponentInChildren<Camera>().enabled = true;
    }

    bool IsGround()
    {
        return collisionFlags == CollisionFlags.CollidedBelow ? true : false;
    }

    void CalculateHeight()
    {
        if (IsGround())
        {
            height = 0f;
        }
        else
        {
            height -= gravity * Time.deltaTime;
        }
    }

    void CheckIfFinishedMovement()
    {
        if (!finished_Movement)
        {

            // normalize time from 0 (start) to 1 (end)
            if(!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Stand") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                finished_Movement = true;
            }
        }
        else
        {
            MoveThePlayer();
            player_Move.y = height * Time.deltaTime;
            collisionFlags = charController.Move(player_Move);
        }
    }

    void MoveThePlayer()
    {

        if (Input.GetMouseButtonUp (0) && !eventSystem.IsPointerOverGameObject()) {
            // calculate where need to go
            //Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            Ray ray = GameObject.FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider is TerrainCollider) {

					player_ToPointDistance = Vector3.Distance (transform.position, hit.point);

					if (player_ToPointDistance >= 1.0f) {
						canMove = true;
						target_Pos = hit.point;
					}
						
				}
			}
 		} // if mouse button down

		if (canMove) {
 			anim.SetFloat ("Walk", 1.0f);

			Vector3 target_Temp = new Vector3 (target_Pos.x, transform.position.y, target_Pos.z);
		
			transform.rotation = Quaternion.Slerp (transform.rotation, 
				Quaternion.LookRotation (target_Temp - transform.position), 
				15.0f * Time.deltaTime);

			player_Move = transform.forward * moveSpeed * Time.deltaTime;

			if (Vector3.Distance (transform.position, target_Pos) <= 0.1f) {
				canMove = false;
			}

		} else {
			player_Move.Set (0f, 0f, 0f);
			anim.SetFloat ("Walk", 0f);
		}

    }


    public bool FinnishedMovement
    {
        get
        {
            return finished_Movement;
        }
        set
        {
            finished_Movement = value;
        }
    }

    public Vector3 TargetPosition
    {
        get
        {
            return target_Pos;
        }
        set
        {
            target_Pos = value;
        }
    }


}
