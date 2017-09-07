using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLayerAttack : MonoBehaviour {

    public Image fillWaitImage_1;
    public Image fillWaitImage_2;
    public Image fillWaitImage_3;
    public Image fillWaitImage_4;
    public Image fillWaitImage_5;
    public Image fillWaitImage_6;

    private int[] fadeImages = new int[] { 0, 0, 0, 0, 0, 0 };

    private Animator anim;
    private bool canAttack = true;

    private PlayerMove playerMOve;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        playerMOve = GetComponent<PlayerMove>();

    }
	
	// Update is called once per frame
	void Update () {
		if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        CheckToFade();
        CheckInput();
    }

    void CheckInput()
    {
        if (anim.GetInteger("Atk") == 0)
        {
            playerMOve.FinnishedMovement = false;
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
            {
                playerMOve.FinnishedMovement = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerMOve.TargetPosition = transform.position;

            if (playerMOve.FinnishedMovement && fadeImages[0] != 1 && canAttack)
            {
                fadeImages[0] = 1;
                anim.SetInteger("Atk", 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerMOve.TargetPosition = transform.position;

            if (playerMOve.FinnishedMovement && fadeImages[1] != 1 && canAttack)
            {
                fadeImages[1] = 1;
                anim.SetInteger("Atk", 2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerMOve.TargetPosition = transform.position;

            if (playerMOve.FinnishedMovement && fadeImages[2] != 1 && canAttack)
            {
                fadeImages[2] = 1;
                anim.SetInteger("Atk", 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerMOve.TargetPosition = transform.position;

            if (playerMOve.FinnishedMovement && fadeImages[3] != 1 && canAttack)
            {
                fadeImages[3] = 1;
                anim.SetInteger("Atk", 4);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerMOve.TargetPosition = transform.position;

            if (playerMOve.FinnishedMovement && fadeImages[4] != 1 && canAttack)
            {
                fadeImages[4] = 1;
                anim.SetInteger("Atk", 5);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            playerMOve.TargetPosition = transform.position;

            if (playerMOve.FinnishedMovement && fadeImages[5] != 1 && canAttack)
            {
                fadeImages[5] = 1;
                anim.SetInteger("Atk", 6);
            }
        }
        else
        {
            anim.SetInteger("Atk", 0);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 targetPos = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(targetPos - transform.position),
                15.0f * Time.deltaTime);

            }

        }
    }

    void CheckToFade()
    {
        if (fadeImages[0] == 1)
        {
            if (FadeAndWait(fillWaitImage_1, 1.0f))
            {
                fadeImages[0] = 0;
            }
        }
        else if (fadeImages[1] == 1)
        {
            if (FadeAndWait(fillWaitImage_2, 0.7f))
            {
                fadeImages[1] = 0;
            }
        }
        else if (fadeImages[2] == 1)
        {
            if (FadeAndWait(fillWaitImage_3, 0.1f))
            {
                fadeImages[2] = 0;
            }
        }
        else if (fadeImages[3] == 1)
        {
            if (FadeAndWait(fillWaitImage_4, 0.2f))
            {
                fadeImages[3] = 0;
            }
        }
        else if (fadeImages[4] == 1)
        {
            if (FadeAndWait(fillWaitImage_5, 0.3f))
            {
                fadeImages[4] = 0;
            }
        }
        else if (fadeImages[5] == 1)
        {
            if (FadeAndWait(fillWaitImage_6, 0.08f))
            {
                fadeImages[5] = 0;
            }
        }
    }

    bool FadeAndWait(Image fadeImg, float fadeTime)
    {
        bool faded = false;

        if (fadeImg == null)
        {
            return faded;
        }

        //check if image is enable in gui
        if (!fadeImg.gameObject.activeInHierarchy)
        {
            fadeImg.gameObject.SetActive(true);
            fadeImg.fillAmount = 1.0f;
        }
        

        fadeImg.fillAmount -= fadeTime * Time.deltaTime;

        if (fadeImg.fillAmount <= 0.0f)
        {
            fadeImg.gameObject.SetActive(false);
            faded = true;
        }

        return faded;
    }

}
