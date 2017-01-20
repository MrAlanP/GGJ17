using UnityEngine;
using System.Collections;

public class PlayerManager : Singleton<PlayerManager> {

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
	// Use this for initialization
	void Start () {
        speed = 1.0f;
        move = new Vector3(0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (echo)
        {
            // interact code here
        }
        if (left)
        {
            move = new Vector3(-1, 0);
        }
        else if (right)
        {
            move = new Vector3(1, 0);
        }
        else if (up)
        {
            move = new Vector3(0, 1);
        }
        else if (down)
        {
            move = new Vector3(0, -1);
        }
        else
        {
            move = new Vector3(0, 0);
        }
        this.transform.position += move * speed * Time.deltaTime;
    }
}
