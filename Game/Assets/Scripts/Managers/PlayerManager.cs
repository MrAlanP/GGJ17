using UnityEngine;
using System.Collections;

public class PlayerManager : Singleton<PlayerManager> {

    public Room currentRoom { get; private set; }
    public Animator animator;

    bool echo
    {
        get { return Input.GetKeyDown("e"); }
    }
    bool left
    {
        get { return Input.GetKey("a"); }
    }
    bool right
    {
        get { return Input.GetKey("d"); }
    }
    bool up
    {
        get { return Input.GetKey("w"); }
    }
    bool down
    {
        get { return Input.GetKey("s"); }
    }

    bool sprint
    {
        get { return Input.GetKey("left shift"); }
    }
    bool cooldown;
    float speed;
    float sprintMax;
    float sprintCurrent;
    float sprintingSpeed;
    Vector3 move;
    int x;
    int z;
    
	// Use this for initialization
	void Start () {
        speed = 1.0f;
        sprintingSpeed = 3.5f;
        sprintMax = 2.0f;
        sprintCurrent = sprintMax;
        move = new Vector3(0, 0);
        x = 0;
        z = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (echo)
        {
            // interact code here
        }

        if (left)
        {
            x = -1;
            animator.SetInteger("direction", -90);
        }
        else if (right)
        {
            x = 1;
            animator.SetInteger("direction", 90);
        }
        else
        {
            x = 0;
        }

        if (up)
        {
            z = 1;
        }
        else if (down)
        {
            z = -1;
        }
        else
        {
            z = 0;
        }
        // apply sprint speed if enoght sprint energy (sprintCurrent) is left
        // current unlimited need to look into more
        if (sprint && sprintCurrent > 0.0f && !cooldown)
        {
            speed = sprintingSpeed;
            sprintCurrent -= 1.5f * Time.deltaTime;
        }
        else
        {
            speed = 1.0f;
            if (sprintCurrent < sprintMax)
            {
                sprintCurrent += 0.25f * Time.deltaTime;
            }
        }

        if (cooldown && sprintCurrent >= 0.5f)
        {
            cooldown = false;
        }

        if (sprintCurrent >= sprintMax)
        {
            sprintCurrent = sprintMax;
        }
        else if (sprintCurrent < 0.0f)
        {
            sprintCurrent = 0.0f;
            cooldown = true;
        }
        
        move = new Vector3(x, 0, z);

        this.transform.position += move * speed * Time.deltaTime;
    }
}
