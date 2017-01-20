using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttainedInformation {


    public Room playerSeenRoom;
    public float playerSeenTime = 0;
    public List<Trap> usedTraps = new List<Trap>();
    public List<Room> enteredRooms = new List<Room>();
    public List<Room> fullyExploredRooms = new List<Room>();
    public List<Enemy> deadEnemies = new List<Enemy>();

    public AttainedInformation()
    {

    }

    public void OnPlayerSeen()
    {
        playerSeenRoom = PlayerManager.Instance.currentRoom;
        playerSeenTime = Time.realtimeSinceStartup;
    }

    public void OnEnemySeen(Enemy enemy)
    {
        if(!enemy.isAlive && !deadEnemies.Contains(enemy))
        {
            deadEnemies.Add(enemy);
        }
    }

    public void OnTrapSeen(Trap trap)
    {
        if (trap.used && !usedTraps.Contains(trap))
        {
            //Add to list of used traps
            usedTraps.Add(trap);
        }
    }

    public void OnRoomExplored(Room room)
    {
        if (!enteredRooms.Contains(room))
        {
            enteredRooms.Add(room);
        }

        //TODO - check if the room has been completely explored
        if (!fullyExploredRooms.Contains(room))
        {
            fullyExploredRooms.Add(room);
        }
    }

    //When we've returned to safety
    public void ReportInformation()
    {
        HiveMindManager.Instance.ReportInformation(this);
    }

    //Take in most relevant information into collated information - used for hivemanager's information
    public void MergeInformation(AttainedInformation otherInfo)
    {
        //Entered Rooms
        for(int i = 0; i<otherInfo.enteredRooms.Count; i++)
        {
            if (!enteredRooms.Contains(otherInfo.enteredRooms[i]))
            {
                enteredRooms.Add(otherInfo.enteredRooms[i]);
            }
        }

        //Fully explored Rooms
        for (int i = 0; i < otherInfo.fullyExploredRooms.Count; i++)
        {
            if (!fullyExploredRooms.Contains(otherInfo.fullyExploredRooms[i]))
            {
                fullyExploredRooms.Add(otherInfo.fullyExploredRooms[i]);
            }
        }

        //Used Traps
        for (int i = 0; i < otherInfo.usedTraps.Count; i++)
        {
            if (!usedTraps.Contains(otherInfo.usedTraps[i]))
            {
                usedTraps.Add(otherInfo.usedTraps[i]);
            }
        }

        //Dead enemies
        for (int i = 0; i < otherInfo.deadEnemies.Count; i++)
        {
            if (!deadEnemies.Contains(otherInfo.deadEnemies[i]))
            {
                deadEnemies.Add(otherInfo.deadEnemies[i]);
            }
        }

        //More recently seen player info
        if(otherInfo.playerSeenTime > playerSeenTime)
        {
            playerSeenRoom = otherInfo.playerSeenRoom;
        }

    }


}
