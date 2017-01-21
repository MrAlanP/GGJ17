using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public bool drawRaysDebug = false;

    public bool isAlive { get; private set; }

    protected int scareCount = 0; //How many times we've been scared
    protected AttainedInformation_Enemy attainedInformation;

    private float movementSpeed = 2;

    private float viewDistance = 25;
    private float degreesBetweenRaycast = 2.0f;

    //All colliders hit in scan
    public List<Collider> hitColliders = new List<Collider>();

    public List<Collider> objectsToSee = new List<Collider>();

    Collider currentObject;

    float investigationTimer = 0;

    //Ref to NavMeshAgent
    private NavMeshAgent nMAgent;

    //Ref to current room
    public Room currentRoom;
    public int objectsExplored = 0;
    public int roomsExplored = 0;

    public EnemyState curState;

    public Vector3 entrance;
	// Use this for initialization
	protected virtual void Start () {

        nMAgent = GetComponent<NavMeshAgent>();

        attainedInformation = new AttainedInformation_Enemy();

        curState = EnemyState.Searching;

        entrance = transform.position;
	}

    // Update is called once per frame
    protected virtual void Update ()
    {
        switch(curState)
        {
            case EnemyState.Searching:

                if (currentRoom.explored)
                {
                    //attainedInformation.OnRoomExplored(currentRoom);
                    roomsExplored++;
                    Debug.Log(name + " Says: This room is explored, moving to next room");

                    if(roomsExplored <2)
                    {
                        nMAgent.SetDestination(currentRoom.GetNextRoom());
                    }
                    if (roomsExplored == 2)
                    {
                        print(name + " Says: The buildings been explored, leaving");

                        nMAgent.SetDestination(entrance);
                    }

                    objectsExplored = 0;
                    curState = EnemyState.ToNewRoom;
                }
                else if (currentObject) //If currently has object to explore
                {
                    //When close enough can begin investigation
                    if (Vector3.Distance(transform.position, currentObject.transform.position) < 5)
                    {
                        if (investigationTimer > currentObject.GetComponent<Item>().investigationTime)
                        {
                            switch (currentObject.tag)
                            {
                                case "Wall":
                                    print("I've explored " + currentObject.transform.parent.name);
                                    objectsToSee.Remove(currentObject.GetComponent<Collider>());
                                    attainedInformation.walls.Add(currentObject.GetComponent<Item>());
                                    currentObject = null;
                                    investigationTimer = 0;
                                    break;
                            }

                            objectsExplored++;
                        }
                        else
                        {
                            //Incremenet investigation;
                            investigationTimer += Time.deltaTime;
                        }
                    }
                }
                else if (objectsToSee.Count > 0 && currentObject == null) //If there are objects to explore
                {
                    //Find the first one in the list
                    currentObject = objectsToSee[0];
                    //Set it as the new destination
                    nMAgent.destination = currentObject.transform.position;

                }
                else if (objectsExplored == currentRoom.objectsToSee)
                {
                    currentRoom.explored = true;
                }


                Scan();

                break;

        }
	}

    public void Spawn()
    {

    }

    //Once the enemy leaves the house with a number of information
    private void OnSafetyReached()
    {

    }

    public void OnEnterNewRoom(Room newRoom)
    {
        currentRoom = newRoom;
        curState = EnemyState.Searching;
        nMAgent.speed = 2;
    }

    //Raycasts for vision
    private void Scan()
    {
        hitColliders.Clear();

        for(float i = 0; i<360; i += degreesBetweenRaycast)
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
            if(hitColliders[i].tag == "Player" && !objectsToSee.Contains(hitColliders[i]))
            {
                Debug.Log("I see the player!");
                attainedInformation.OnPlayerSeen();

                //Investigator gets scared moves to other room
                curState = EnemyState.ToNewRoom;
                nMAgent.SetDestination(currentRoom.GetNextRoom());
                nMAgent.speed = 4;
                scareCount++;
                return;
            }

            if(hitColliders[i].tag == "Death")
            {
                Debug.Log("I see a death!");
                attainedInformation.OnEnemySeen(hitColliders[i].GetComponent<Enemy>());
            }

            if(hitColliders[i].tag == "Trap")
            {
                Debug.Log("I See a Trap");
                attainedInformation.OnTrapSeen(hitColliders[i].GetComponent<Trap>());
            }

            if(hitColliders[i].tag == "Wall" && !objectsToSee.Contains(hitColliders[i]) && 
                !attainedInformation.walls.Contains(hitColliders[i].GetComponent<Item>()))
            {
                Debug.Log("I See a Wall");
                objectsToSee.Add(hitColliders[i]);
            }
        }
    }
}
