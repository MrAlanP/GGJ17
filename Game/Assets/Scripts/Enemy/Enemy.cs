using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public bool drawRaysDebug = false;

    protected int scareCount = 0; //How many times we've been scared

    private float movementSpeed = 2;

    private float aimAngle;
    private float viewDistance = 5.0f;
    private float degreesBetweenRaycast = 2.0f;

    //All colliders hit in scan
    private List<Collider2D> hitColliders = new List<Collider2D>(); 
	// Use this for initialization
	protected virtual void Start () {
	
	}

    // Update is called once per frame
    protected virtual void Update () {
        Scan();
	}

    //Raycasts for vision
    void Scan()
    {
        hitColliders.Clear();

        for(float i = 0; i<360; i += degreesBetweenRaycast)
        {
            float angleRadians = i * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));

            if (drawRaysDebug)
            {
                Debug.DrawRay(transform.position, direction * viewDistance, Color.red);
            }
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, viewDistance);
            if(hit.collider != null && !hitColliders.Contains(hit.collider))
            {
                hitColliders.Add(hit.collider);
            }
            
        }

        //Iterate over found colliders
        for(int i = 0; i<hitColliders.Count; i++)
        {

        }
    }
}
