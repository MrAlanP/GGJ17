using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    //References to the rooms it is connected to
    public Room[] rooms;

    public bool explored;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Finds the next room that's not explored
    public Vector3 GetNextRoom()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if(!rooms[i].explored)
            {
                return rooms[i].transform.position;
            }
        }

        //Recurring code here potenitally
        //For now just return null Vector

        return Vector3.zero;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Enemy>())
        {
            Debug.Log("Investigator entered this room");

            if (!this.explored)
            {
                collider.gameObject.GetComponent<Enemy>().OnEnterNewRoom(this);
            }

            //Code
        }
    }
}
