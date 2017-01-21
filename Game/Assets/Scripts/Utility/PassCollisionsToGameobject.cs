using UnityEngine;
using System.Collections;

public class PassCollisionsToGameobject : MonoBehaviour {

    public GameObject go;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider playerColl)
    {
        go.SendMessage("OnTriggerStay", playerColl);
    }
}
