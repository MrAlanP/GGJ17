using UnityEngine;
using System.Collections;

public class Enemy_Swat : Enemy {

    bool investigated = false;

    static float investigationTime = 2;
    float investigationTimer = 0;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    protected override void Update()
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

            Scan();

            if(!PlayerManager.Instance.alive)
            {
                curState = EnemyState.Leaving;
                nMAgent.destination = RoomManager.Instance.rooms[0].transform.position;
            }

            switch (curState)
            {
                case EnemyState.Investigating:

                    //If current rooms has already been explored or has been 
                    if (investigated)
                    {
                        if (targetRoom)
                        {
                            Debug.Log(name + " Says: " + targetRoom.name + " room is explored, moving to next room");
                        }

                        investigated = false;

                        //Has the Investigator done all the exploring they can?
                        if (roomsExplored.Count >= RoomManager.Instance.roomsToExplore)
                        {
                            print(name + "Made first sweep, going again");
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
                            print("Time to look for the next room");
                            curState = EnemyState.SearchingForNewRoom;
                        }
                    }
                    else
                    {
                        //If in the centre of room start exploration
                        if (nMAgent.desiredVelocity.magnitude < 1 && Vector3.Distance(transform.position, nMAgent.destination) < 2)
                        {
                            //print("investigating");
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

                    break;

                case EnemyState.SearchingForNewRoom:

                    FindNewRoom();
                    break;

                case EnemyState.Leaving:

                    if (currentRoom == RoomManager.Instance.rooms[0])
                    {
                        //OnSafetyReached();
                    }
                    break;

                case EnemyState.ToNewRoom:

                    if (currentRoom == targetRoom)
                    {
                        curState = EnemyState.Investigating;
                    }
                    break;

                case EnemyState.Engaging:

                    nMAgent.destination = PlayerManager.Instance.rb.position;

                    if(Vector3.Distance(transform.position, nMAgent.destination) < .5f)
                    {
                        PlayerManager.Instance.OnDeath(this.transform);
                        nMAgent.destination = RoomManager.Instance.rooms[0].transform.position;

                        curState = EnemyState.Leaving;
                    }
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

    public override void FindNewRoom()
    {
        if (!targetRoom || targetRoom.explored)
        {
            targetRoom = RoomManager.Instance.GetNextRoomSwat(roomsExplored);
            //Look for new Room
            nMAgent.SetDestination(targetRoom.transform.position);

            print(name + " Says: Found room, claiming");
            InvestigatorManager.Instance.roomsClaimed.Add(targetRoom, this);

            curState = EnemyState.ToNewRoom;
        }
    }

    protected override void OnFinishExploring()
    {
        roomsExplored.Clear();
        curState = EnemyState.SearchingForNewRoom;
    }

    public override void ScareState()
    {
        base.ScareState();
        if(scareCount == 3)
        {
            // Exit
        }
    }

    protected override void OnPlayerSeen()
    {
        print(name + "Says: I've seen the Ghost Engaging");
        nMAgent.destination = PlayerManager.Instance.rb.position;
        playerWitnessed = true;
        curState = EnemyState.Engaging;
        base.OnPlayerSeen();
    }
}
