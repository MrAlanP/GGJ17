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
    GameObject enemy;
    PassCollisionsToGameobject child;
    SpriteRenderer visability;
    Collider playerColl;
    Collider enemyColl;

    public Animator animator;
    public Animator auxAnimator;
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
            if(useable)
            {
                UIManager.Instance.inObject = true;
            }
            if (echo && inObject)
            {
                visability.enabled = true;
                PlayerManager.Instance.inObject = false;
                UIManager.Instance.useable = false;
                UIManager.Instance.inObject = false;
                inObject = false;
            }
            else if (echo && useable && !inObject)
            {
                visability.enabled = false;
                inObject = true;
                PlayerManager.Instance.moveToObject = transform.position;
                PlayerManager.Instance.inObject = true;
                UIManager.Instance.inObject = true;
                if (useable)
                {
                    UIManager.Instance.useable = true;
                }
            }
            if (foxtrot && visability.enabled == false && useable)
            {
                useable = false;
                animator.SetTrigger("Play");
                auxAnimator.SetTrigger("Play");
                UIManager.Instance.useable = false;
            }
            Debug.Log("Player!");
        }

        if (eProxy && isPlaying)
        {
            Debug.Log("what are you?");
            if (trapType == TrapType.Lethal)
            {
                Debug.Log("enemy script scare");
                enemy.GetComponent<Enemy>().deathTrap = true;
            }
            if (trapType == TrapType.NonLethal)
            {
                Debug.Log("trap script fear");
                enemy.GetComponent<Enemy>().fearTrap = true;
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
           enemy = other.gameObject;
           eProxy = true;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other == playerColl)
        {

        }
        else if (other)
        {
            enemy = other.gameObject;
            eProxy = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other == playerColl)
        {
            UIManager.Instance.inObject = false;
            UIManager.Instance.useable = false;
            pProxy = false;
        }
        else if (other)
        {
            eProxy = false;
        }
    }
}
