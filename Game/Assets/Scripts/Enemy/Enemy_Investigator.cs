using UnityEngine;
using System.Collections;

public class Enemy_Investigator : Enemy {


    bool investigated = false;

    static float investigationTime = 1;
    float investigationTimer = 0;

    float playerSightCooldown = 2;

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        if (isAlive)
        {
            base.Update();

            if (nMAgent.desiredVelocity.x > 0)
            {
                animator.SetInteger("direction", 90);
            }
            else
            {
                animator.SetInteger("direction", -90);
            }

            if(playerWitnessed)
            {
                playerSightCooldown -= Time.deltaTime;

                if (playerSightCooldown < 0)
                {
                    playerWitnessed = false;
                }

            }

            switch (curState)
            {
                case EnemyState.Investigating:


                    //If current rooms has already been explored or has been 
                    if (currentRoom && (currentRoom.explored || investigated))
                    {
                        if (targetRoom)
                            Debug.Log(name + " Says: " + targetRoom.name + " room is explored, moving to next room");

                        investigated = false;

                        //Has the Investigator done all the exploring they can?
                        if (roomsExplored.Count >= explorationTotal)
                        {
                            print(name + "Says I'm done exploring, time to leave");
                            OnFinishExploring();
                        }
                        //Have all the rooms been explored
                        else if (roomsExplored.Count >= RoomManager.Instance.rooms.Length)
                        {
                            print(name + " Says: The buildings been explored, leaving");
                            OnFinishExploring();
                        }
                        //If not then find the next room
                        else
                        {
                            curState = EnemyState.SearchingForNewRoom;
                        }
                    }
                    else
                    {
                        //If in the centre of room start exploration
                        if (nMAgent.desiredVelocity.magnitude < 1 && Vector3.Distance(transform.position, nMAgent.destination) < 2)
                        {
                            investigationTimer += Time.deltaTime * EnemyManager.Instance.investigationSpeed;

                            if (investigationTimer > investigationTime)
                            {
                                investigationTimer = 0;
                                print("Finished investigating");
                                InvestigatorManager.Instance.roomsClaimed.Remove(currentRoom);
                                roomsExplored.Add(targetRoom);
                                investigated = true;
                            }
                        }
                    }

                    Scan();
                    break;

                case EnemyState.SearchingForNewRoom:

                    FindNewRoom();
                    Scan();
                    break;

                case EnemyState.Leaving:

                    if (currentRoom == RoomManager.Instance.rooms[0])
                    {
                        OnSafetyReached();
                    }
                    break;

                case EnemyState.ToNewRoom:

                    if (currentRoom == targetRoom)
                    {
                        curState = EnemyState.Investigating;
                    }
                    Scan();

                    break;

            }

            if (drawRaysDebug)
            {
                Debug.DrawRay(transform.position, nMAgent.desiredVelocity, Color.blue);
            }

            //Sean triggers for death and scare
            if (fearTrap)
            {
                Debug.Log("enemy script fear");
                OnTrapScare();
                fearTrap = false;
            }
            if (deathTrap)
            {
                Debug.Log("enemy script death");
                OnTrapDeath();
                deathTrap = false;
            }

            ScareState();
        }
    }

    protected override void OnFinishExploring()
    {
        LeaveHouse();
    }
    protected override void OnSafetyReached()
    {
        RoomManager.Instance.AddExploredRooms(roomsExplored);
        roomsExplored.Clear();
        InvestigatorManager.Instance.OnEnemyFinish(this, true);
        curState = EnemyState.Left;
    }

    public override void ScareState()
    {
        base.ScareState();
        switch (scareCount)
        {
            case 1:
                animator.SetBool("sad", true);
                fearSpeedMultiplier = 2;
                explorationTotal--;
                Debug.Log("i'm a sad panda");
                // investagtor scared enemy images
                // stuff
                break;

            case 2:
                fearSpeedMultiplier = 4;
                LeaveHouse();
                // investagtor override set alertness value to 0
                // OnFinishExploring();   (?)
                break;
        }
    }

    public override void OnTrapDeath()
    {
        base.OnTrapDeath();
    }

    protected override void OnPlayerSeen()
    {
        if (!playerWitnessed)
        {
            playerWitnessed = true;
            EnemyManager.Instance.alertness += .6f;
            print(name + " Says: I've seen the Ghost, oh crumbs");
            OnTrapScare();
            targetRoom = RoomManager.Instance.GetNextRoom(roomsExplored);
            //Look for new Room
            nMAgent.SetDestination(targetRoom.transform.position);

            if (targetRoom.transform.position != RoomManager.Instance.startingRoom)
            {
                print(name + " Says: Found room, claiming");
                InvestigatorManager.Instance.roomsClaimed.Add(targetRoom, this);
            }

            curState = EnemyState.ToNewRoom;
        }
    }
}
