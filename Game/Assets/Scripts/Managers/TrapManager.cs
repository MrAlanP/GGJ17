using UnityEngine;
using System.Collections;

public class TrapManager : MonoBehaviour {
        bool echo
    {
        get { return Input.GetKeyDown("e"); }
    }
    bool foxtrot
    {
        get { return Input.GetKeyDown("f"); }
    }

    bool useable;
    bool inObject;
    bool pProxy;
    bool eProxy;
    bool isPlaying;
    public TrapType trapType;
    GameObject player;
    PassCollisionsToGameobject child;
    SpriteRenderer visability;
    Collider playerColl;
    Collider enemyColl;

    public Animator animator;
    public enum TrapType { Lethal, NonLethal };


    // Use this for initialization
    void Start () {
        useable = true;
        inObject = false;
        child = this.GetComponentInChildren<PassCollisionsToGameobject>();
        player = GameObject.FindGameObjectWithTag("Player");
        visability = player.GetComponent<SpriteRenderer>();
        playerColl = player.GetComponent<Collider>();

    }
	
	// Update is called once per frame
	void Update () {
        isPlaying = child.isPlaying;
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
        if (eProxy && isPlaying)
        {
            Debug.Log("what are you?");
            if (trapType == TrapType.Lethal)
            {
                Debug.Log("needs to DIE");
            }
            if (trapType == TrapType.NonLethal)
            {
                Debug.Log("Needs to leave");
            }
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
