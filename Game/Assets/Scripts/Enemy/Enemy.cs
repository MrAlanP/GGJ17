using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public bool drawRaysDebug = false;

    public bool isAlive { get; private set; }

    protected int scareCount = 0; //How many times we've been scared

    private float movementSpeed = 2;

    public static float viewDistance = 25;
    private float degreesBetweenRaycast = 2.0f;

    //Sean, referance to trap activation
    public bool fearTrap = false;
    public bool deathTrap = false;

    //All colliders hit in scan
    public List<Collider> hitColliders = new List<Collider>();

    //Ref to NavMeshAgent
    protected NavMeshAgent nMAgent;

    //Ref to current room
    public Room currentRoom;

    public EnemyState curState;

    public int explorationTotal = 1;

    public List<Room> roomsExplored = new List<Room>();

    public Room targetRoom;

    // Use this for initialization
    protected virtual void Start() {

        nMAgent = GetComponent<NavMeshAgent>();

        curState = EnemyState.SearchingForNewRoom;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(nMAgent.speed < EnemyManager.Instance.movementSpeed)
        {
            nMAgent.speed = EnemyManager.Instance.movementSpeed;
        }

         
    }

    public void FindNewRoom()
    {
        if (!targetRoom)
        {
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

    public void Spawn()
    {
        nMAgent = GetComponent<NavMeshAgent>();
        viewDistance = EnemyManager.Instance.sightRange;
        nMAgent.speed = EnemyManager.Instance.movementSpeed;
    }

    public void OnEnterNewRoom(Room newRoom)
    {
        currentRoom = newRoom;
    }

    //Raycasts for vision
    protected void Scan()
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
            if(hitColliders[i].tag == "Player")
            {
                print(name + " Says: I've seen the Ghost, oh crumbs");
                targetRoom = RoomManager.Instance.GetNextRoom(roomsExplored);
                //Look for new Room
                nMAgent.SetDestination(targetRoom.transform.position);

                if (targetRoom.transform.position != RoomManager.Instance.startingRoom)
                {
                    print(name + " Says: Found room, claiming");
                    InvestigatorManager.Instance.roomsClaimed.Add(targetRoom, this);
                }

                curState = EnemyState.ToNewRoom;

                break;

                scareCount++;
            }
        }
    }

    public void OnTrapScare()
    {
        // Ramp up fear level
        scareCount++;
    }

    public virtual void OnTrapDeath()
    {
        // Play death, kill ect.
        isAlive = false;

    }

    public virtual void ScareState()
    {}
}
