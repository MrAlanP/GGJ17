using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public Animator animator;
    public bool drawRaysDebug = false;

    public bool isAlive = true;

    protected int scareCount = 0; //How many times we've been scared

    private float movementSpeed = 2;
    protected float fearSpeedMultiplier = 1;

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

    public int explorationTotal = 2;

    public List<Room> roomsExplored = new List<Room>();

    public Room targetRoom;

    bool deathWitnessed = false;
    protected bool playerWitnessed = false;

    // Use this for initialization
    protected virtual void Start() {

        nMAgent = GetComponent<NavMeshAgent>();

        curState = EnemyState.SearchingForNewRoom;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        nMAgent.speed = EnemyManager.Instance.movementSpeed * fearSpeedMultiplier;
    }

    public virtual void FindNewRoom()
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

            if (Physics.Raycast(transform.position + Vector3.up * .09f, direction, out hit, viewDistance))
            {
                Vector3 targetDir = hit.point - transform.position;
                float angle = Vector3.Angle(targetDir, nMAgent.desiredVelocity);

                if (angle < 45.0f)
                {
                    if (hit.collider != null && !hitColliders.Contains(hit.collider) &&
                        hit.collider.bounds.Intersects(currentRoom.GetComponent<Collider>().bounds))
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
                        //Debug.DrawRay(transform.position, direction * viewDistance, Color.red);
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
            if (hitColliders[i].tag == "Death" && !hitColliders[i].GetComponent<Enemy>().deathWitnessed)
            {
                //print(name + " Says: Oh God," + hitColliders[i].name + " is dead!");
                EnemyManager.Instance.alertness += 1;
                hitColliders[i].GetComponent<Enemy>().deathWitnessed = true;
                OnTrapScare();
            }

            if (hitColliders[i].tag == "Player" && !playerWitnessed)
            {
                OnPlayerSeen();

                break;
            }
        }
    }

    protected virtual void OnPlayerSeen()
    {
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
        nMAgent.enabled = false;
        tag = "Death";
        animator.SetBool("alive", false);
        EnemyManager.Instance.OnEnemyFinish(this, false);
    }

    public virtual void ScareState()
    {}

    protected virtual void OnFinishExploring()
    {
    }

}
