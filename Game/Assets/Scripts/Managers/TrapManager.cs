using UnityEngine;
using System.Collections;

public class TrapManager : MonoBehaviour {
    bool useable;
    bool inObject;
    float y;
    GameObject player;
    SpriteRenderer visability;
    Collider playerColl;
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
	}

    void OnTriggerStay(Collider playerColl)
    {
        if (PlayerManager.Instance.action && useable && !inObject)
        {
            print("Entered object");
            visability.enabled = false; 
            inObject = true;
            PlayerManager.Instance.inObject = true;
        }
        if (PlayerManager.Instance.action && inObject)
        {
            print("Exiting object");
            visability.enabled = true;
            PlayerManager.Instance.inObject = false;
            inObject = false;
        }

        if (PlayerManager.Instance.fire && visability.enabled == false && useable)
        {
            print("activating object");
            useable = false;
            animator.SetTrigger("Play");
        }
    }
}
