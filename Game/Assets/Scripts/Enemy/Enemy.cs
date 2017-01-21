using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public bool drawRaysDebug = false;

    public bool isAlive { get; private set; }

    protected int scareCount = 0; //How many times we've been scared

    private float movementSpeed = 2;

    private float viewDistance = 25;
    private float degreesBetweenRaycast = 2.0f;

    //All colliders hit in scan
    public List<Collider> hitColliders = new List<Collider>();

    static float investigationTime = 1;
    float investigationTimer = 0;

    //Ref to NavMeshAgent
    private NavMeshAgent nMAgent;

    //Ref to current room
    public Room currentRoom;

    public EnemyState curState;

    public int explorationTotal = 1;

    public List<Room> roomsExplored = new List<Room>();

    bool investigated = false;

	// Use this for initialization
	protected virtual void Start () {

        nMAgent = GetComponent<NavMeshAgent>();

        curState = EnemyState.Searching;
	}

    // Update is called once per frame
    protected virtual void Update ()
    {
        switch (curState)
        {
            case EnemyState.Searching:

                if (currentRoom && (currentRoom.explored || investigated))
                {
                    investigated = false;
                    //attainedInformation.OnRoomExplored(currentRoom);

                    //Want Investigator to leave when they have explored enough or finished the whole house
                    if (roomsExplored.Count >= explorationTotal ||
                        roomsExplored.Count >= RoomManager.Instance.rooms.Length)
                    {
                        print(name + " Says: The buildings been explored, leaving");
                        nMAgent.SetDestination(RoomManager.Instance.startingRoom);
                        curState = EnemyState.Leaving;
                    }
                    else
                    {
                        Debug.Log(name + " Says: This room is explored, moving to next room");
                        nMAgent.SetDestination(RoomManager.Instance.GetNextRoom(roomsExplored));

                        curState = EnemyState.ToNewRoom;
                    }
                }
                else
                {
                    //If in the centre of room start exploration
                    if (nMAgent.desiredVelocity.magnitude < 1)
                    {
                        investigationTimer += Time.deltaTime;

                        if (investigationTimer > investigationTime)
                        {
                            roomsExplored.Add(currentRoom);
                            investigated = true;
                        }
                    }
                }

                //Scan();

                break;

            case EnemyState.Leaving:

                if (currentRoom == RoomManager.Instance.rooms[0])
                {
                    OnSafetyReached();
                }
                break;

        }

        if(drawRaysDebug)
        {
            Debug.DrawRay(transform.position, nMAgent.desiredVelocity, Color.blue);
        }
	}

    public void Spawn()
    {
        nMAgent = GetComponent<NavMeshAgent>();
    }

    //Once the enemy leaves the house with a number of information
    private void OnSafetyReached()
    {
        RoomManager.Instance.AddExploredRooms(roomsExplored);
        roomsExplored.Clear();
        InvestigatorManager.Instance.OnInvestigatorFinish(this);
        curState = EnemyState.Left;
    }

    public void OnEnterNewRoom(Room newRoom)
    {
        currentRoom = newRoom;
        nMAgent.speed = 2;

        if (curState != EnemyState.Leaving)
        {
            curState = EnemyState.Searching;
        }
    }

    //Raycasts for vision
    private void Scan()
    {
        hitColliders.Clear();

        for(float i = 0; i< 360; i += degreesBetweenRaycast)
        {
            float angleRadians = (i) * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angleRadians), 0, Mathf.Sin(angleRadians));

            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction, out hit, viewDistance))
            {
                Vector3 targetDir = hit.point - transform.position;
                float angle = Vector3.Angle(targetDir, nMAgent.desiredVelocity);

                if (angle < 45.0f)
                {
                    if (hit.collider != null && !hitColliders.Contains(hit.collider) &&
                        hit.collider.bounds.Intersects(currentRoom.GetComponent<BoxCollider>().bounds))
                    {
                        hitColliders.Add(hit.collider);
                    }

                    if (drawRaysDebug)
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.green);
                    }
                }
                else
                {
                    if (drawRaysDebug)
                    {
                        Debug.DrawRay(transform.position, direction * viewDistance, Color.red);
                    }
                }
            }
            else
            {
                //if (drawRaysDebug)
                //{
                //    Debug.DrawRay(transform.position, direction * viewDistance, Color.red);
                //}
            }
        }

        //Iterate over found colliders
        for(int i = 0; i< hitColliders.Count; i++)
        {
        }
    }
}
