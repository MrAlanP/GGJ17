using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : Singleton<RoomManager>
{

    public Room[] rooms;

    public Vector3 startingRoom
    {
        get
        {
            return rooms[0].transform.position;
        }
    }

    public bool allRoomsExplored
    {
        get
        {
            foreach(Room room in rooms)
            {
                if(!room.explored)
                {
                    return false;
                }
            }

            return true;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector3 GetNextRoom(List<Room> enemyRooms)
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if(!enemyRooms.Contains(rooms[i]) && !rooms[i].explored)
            {
                return rooms[i].transform.position;
            } 
        }

        return rooms[0].transform.position;
    }
    
    public void AddExploredRooms(List<Room> exploredRooms)
    {
        //Check all explored Rooms
        for(int i=0; i < exploredRooms.Count; i++)
        {
            exploredRooms[i].explored = true;
        }
    }
}
