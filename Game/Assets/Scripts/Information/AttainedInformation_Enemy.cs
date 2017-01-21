using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttainedInformation_Enemy : AttainedInformation{


    public AttainedInformation_Enemy() : base()
    {

    }

    //When the player has been seen Massive increase to alertness Investigators runs to other room
    public void OnPlayerSeen()
    {
        playerSeenRoom = PlayerManager.Instance.currentRoom;
        playerSeenTime = Time.realtimeSinceStartup;
    }

    public void OnEnemySeen(Enemy enemy)
    {
        if (!enemy.isAlive && !deadEnemies.Contains(enemy))
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
        else
        {
            traps.Add(trap);
        }
    }

    public void OnWallSeen(Item wall)
    {
        walls.Add(wall); 
    }

    public void OnRoomEntered(Room room)
    {
        if (!enteredRooms.Contains(room))
        {
            enteredRooms.Add(room);
        }        
    }

    public void OnRoomFullyExplored(Room room)
    {
        if (!fullyExploredRooms.Contains(room))
        {
            fullyExploredRooms.Add(room);
            //Increase alertness
        }
    }

    //When we've returned to safety
    public void ReportInformation()
    {
        HiveMindManager.Instance.ReportInformation(this);
    }

}
