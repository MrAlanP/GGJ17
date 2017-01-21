using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttainedInformation_HiveMind : AttainedInformation
{


    public AttainedInformation_HiveMind() : base()
    {

    }

    //Take in most relevant information into collated information - used for hivemanager's information
    public void MergeInformation(AttainedInformation otherInfo)
    {
        //Entered Rooms
        for (int i = 0; i < otherInfo.enteredRooms.Count; i++)
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
        if (otherInfo.playerSeenTime > playerSeenTime)
        {
            playerSeenRoom = otherInfo.playerSeenRoom;
        }

    }

}