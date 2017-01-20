using UnityEngine;
using System.Collections;

public class AttainedInformation : MonoBehaviour {


    public Room playerSeenRoom;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPlayerSeen()
    {
        playerSeenRoom = PlayerManager.Instance.currentRoom;
    }

    //When we've returned to safety
    public void ReportInformation()
    {

    }
}
