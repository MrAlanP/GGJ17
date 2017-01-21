using UnityEngine;
using System.Collections;

public class PassCollisionsToGameobject : MonoBehaviour {

    public GameObject go;
    public bool isPlaying;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        go.SendMessage("OnTriggerStay", other);
    }
    void OnTriggerExit(Collider other)
    {
        go.SendMessage("OnTriggerExit", other);
    }
}
