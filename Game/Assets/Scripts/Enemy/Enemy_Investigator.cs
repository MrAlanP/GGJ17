using UnityEngine;
using System.Collections;

public class Enemy_Investigator : Enemy {

    public Animator animator;

    bool investigated = false;

    static float investigationTime = 1;
    float investigationTimer = 0;

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();

        if (nMAgent.desiredVelocity.x > 0)
        {
            animator.SetInteger("sirection", 90);
        }
        else
        {
            animator.SetInteger("direction", -90);
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

    private void OnFinishExploring()
    {
        nMAgent.SetDestination(RoomManager.Instance.startingRoom);
        curState = EnemyState.Leaving;
    }

    //Once the enemy leaves the house with a number of information
    private void OnSafetyReached()
    {
        RoomManager.Instance.AddExploredRooms(roomsExplored);
        roomsExplored.Clear();
        InvestigatorManager.Instance.OnEnemyFinish(this);
        curState = EnemyState.Left;
    }

    public override void ScareState()
    {
        base.ScareState();
        switch (scareCount)
        {
            case 1:
                animator.SetBool("sad", true);
                Debug.Log("i'm a sad panda");
                // investagtor scared enemy images
                // stuff
                break;

            case 2:
                // investagtor override set alertness value to 0
                // OnFinishExploring();   (?)
                break;
        }
    }
    public override void OnTrapDeath()
    {
        base.OnTrapDeath();
        animator.SetBool("alive", false);
    }
}
