using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    //References to the rooms it is connected to
    public Room[] rooms;

    public int objectsToSee;

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
        //Attempt to look for next unexplored room
        for (int i = 0; i < rooms.Length; i++)
        {
            if(!rooms[i].explored)
            {
                return rooms[i].transform.position;
            }
        }

        //If we don't find an unexplored room directly connected we need to look elsewhere
        for (int j = 0; j < rooms.Length; j++)
        {
            return rooms[j].GetNextRoom();
        }

        //Recurring code here potenitally
        //For now just return null Vector

        return Vector3.up;
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
