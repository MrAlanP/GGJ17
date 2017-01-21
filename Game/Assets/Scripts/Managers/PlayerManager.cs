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
        if (sprint && sprintCurrent > 0.0f)
        {
            speed = sprintingSpeed;
            sprintCurrent -= 0.1f * Time.deltaTime;
        }
        else
        {
            speed = 1.0f;
            if(sprintCurrent <= sprintMax)
            sprintCurrent += 0.1f * Time.deltaTime;
        }

        move = new Vector3(x, 0, z);

        this.transform.position += move * speed * Time.deltaTime;
    }
}
