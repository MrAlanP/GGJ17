using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public bool drawRaysDebug = false;

    public bool isAlive { get; private set; }

    protected int scareCount = 0; //How many times we've been scared
    protected AttainedInformation_Enemy attainedInformation;

    private float movementSpeed = 2;

    private float viewDistance = 50.0f;
    private float degreesBetweenRaycast = 2.0f;

    //All colliders hit in scan
    private List<Collider> hitColliders = new List<Collider>();

    //Ref to NavMeshAgent
    private NavMeshAgent nMAgent;

    //Ref to current room
    public Room currentRoom;

    public EnemyState curState;
     
	// Use this for initialization
	protected virtual void Start () {

        nMAgent = GetComponent<NavMeshAgent>();

        attainedInformation = new AttainedInformation_Enemy();

        curState = EnemyState.Searching;

	}

    // Update is called once per frame
    protected virtual void Update ()
    {
        switch(curState)
        {
            case EnemyState.Searching:

                if (currentRoom.explored)
                {
                    Debug.Log(name + " Says: This room is explored, moving to next room");
                    nMAgent.SetDestination(currentRoom.GetNextRoom());

                    curState = EnemyState.ToNewRoom;
                }
                else
                { 
                    Scan();
                }

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
    }

    //Raycasts for vision
    private void Scan()
    {
        hitColliders.Clear();

        for(float i = 0; i<360; i += degreesBetweenRaycast)
        {
            float angleRadians = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angleRadians), 0, Mathf.Sin(angleRadians));

            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction, out hit, viewDistance))
            {
                if (hit.collider != null && !hitColliders.Contains(hit.collider))
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
                if(drawRaysDebug)
                {
                    Debug.DrawRay(transform.position, direction * viewDistance, Color.red);
                }
            }
        }

        //Iterate over found colliders
        for(int i = 0; i<hitColliders.Count; i++)
        {
            if(hitColliders[i].tag == "Player")
            {
                Debug.Log("I see the player!");
                attainedInformation.OnPlayerSeen();
            }
        }
    }
}
