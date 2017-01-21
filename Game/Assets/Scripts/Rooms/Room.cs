using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    //References to the rooms it is connected to
    public Room[] rooms;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Enemy_Investigator>())
        {
            Debug.Log("Investigator entered this room");
            //Code
        }
    }
}
