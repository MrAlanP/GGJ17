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
    float speed;
    Vector3 move;
    int x;
    int y;
    
	// Use this for initialization
	void Start () {
        speed = 1.0f;
        move = new Vector3(0, 0);
        x = 0;
        y = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (echo)
        {
            // interact code here
        }

        if (left)
        {
            //move = new Vector3(-1, 0);
            x = -1;
            animator.SetInteger("direction", -90);
        }
        else if (right)
        {
            //move = new Vector3(1, 0);
            x = 1;
            animator.SetInteger("direction", 90);
        }
        else
        {
            x = 0;
        }

        if (up)
        {
            //move = new Vector3(0, 1);
            y = 1;
        }
        else if (down)
        {
            //move = new Vector3(0, -1);
            y = -1;
        }
        else
        {
            y = 0;
        }

        //if (!left && !right && !up && !down)
        //{
        //    move = new Vector3(0, 0);
        //}

        move = new Vector3(x, y);

        this.transform.position += move * speed * Time.deltaTime;
    }
}
