using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager> {
    bool quebec
    {
        get { return Input.GetKey("q"); }
    }
    float maxZoom;
    float defaultZoom;
    // Use this for initialization
    void Start () {
        maxZoom = 30;
        defaultZoom = 6;
    }
	
	// Update is called once per frame
	void Update () {
        if (quebec)
        {
            if (transform.position.y < maxZoom)
            {
                transform.Translate(0, Time.deltaTime * 17, 0, Space.World);
            }
            if (transform.position.y > maxZoom)
            {
                transform.position = new Vector3(transform.position.x, maxZoom, transform.position.z);
            }
        }
        else
        {
            if (transform.position.y > defaultZoom)
            {
                transform.Translate(0, -Time.deltaTime * 22, 0, Space.World);
            }
            if (transform.position.y < defaultZoom)
            {
                transform.position = new Vector3(transform.position.x, defaultZoom, transform.position.z);
            }
        }
	}
}
