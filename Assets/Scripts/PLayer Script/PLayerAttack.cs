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

    private int attack;


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
        if (Input.GetKeyDown(KeyCode.Alpha1) || attack == 1)
        {
            attack = 1;
            Attack(attack);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || attack == 2)
        {
            attack = 2;
            Attack(attack);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || attack == 3)
        {
            attack = 3;
            Attack(attack);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || attack == 4)
        {
            attack = 4;
            Attack(attack);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) || attack == 5)
        {
            attack = 5;
            Attack(attack);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) || attack == 6)
        {
            attack = 6;
            Attack(attack);
        }
        else
        {
            anim.SetInteger("Atk", attack);
        }

        //attack = 0;

        //if (Input.GetKey(KeyCode.Space))

        if (Input.GetMouseButton(0))
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

    public void PushAttack(int attack)
    {
        this.attack = attack;
    }

    public void Attack(int attack)
    {
        int index = attack - 1;

        if (playerMOve.FinnishedMovement && fadeImages[index] != 1 && canAttack)
        {
            fadeImages[index] = 1;
            anim.SetInteger("Atk", attack);

            playerMOve.TargetPosition = transform.position;
            RemoveCursorPoint();

        }
        this.attack = 0;
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

    public void RemoveCursorPoint()
    {
        GameObject cursorObj = GameObject.FindGameObjectWithTag("Cursor");
        if (cursorObj)
        {
            Destroy(cursorObj);
        
        }
    }

}
