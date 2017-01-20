using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private float aimAngle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Scan();
	}

    //Raycasts for vision
    void Scan()
    {
        
    }
}
