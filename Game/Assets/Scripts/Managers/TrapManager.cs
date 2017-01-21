using UnityEngine;
using System.Collections;

public class TrapManager : MonoBehaviour {
    bool echo
    {
        get { return Input.GetKeyDown("e"); }
    }
    bool foxtrot
    {
        get { return Input.GetKey("f"); }
    }

    bool useable;
    bool inObject;
    bool pProxy;
    bool eProxy;
    float y;
    GameObject player;
    SpriteRenderer visability;
    Collider playerColl;
    Collider enemyColl;
    public Animator animator;

    // Use this for initialization
    void Start () {
        useable = true;
        inObject = false;
        y = transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player");
        visability = player.GetComponent<SpriteRenderer>();
        playerColl = player.GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(useable);
        if (pProxy)
        {
            if (echo && inObject)
            {
                print("Exiting object");
                visability.enabled = true;
                PlayerManager.Instance.inObject = false;
                inObject = false;
            }
            else if (echo && useable && !inObject)
            {
                print("Entered object");
                visability.enabled = false;
                inObject = true;
                PlayerManager.Instance.inObject = true;
            }
            if (foxtrot && visability.enabled == false && useable)
            {
                print("activating object");
                useable = false;
                animator.SetTrigger("Play");
            }
            Debug.Log("Player!");
        }
        if (eProxy)
        {
            Debug.Log("wtf");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other == playerColl)
        {
            pProxy = true;
        }
        else if (other)
        {
            eProxy = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other == playerColl)
        {
            pProxy = false;
        }
        else if (other)
        {
            eProxy = false;
        }
    }
}
