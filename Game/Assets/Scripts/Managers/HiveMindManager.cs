using UnityEngine;
using System.Collections;

public class HiveMindManager : Singleton<HiveMindManager> {

    public AttainedInformation_HiveMind collatedInformation;
	// Use this for initialization
	void Start () {
        collatedInformation = new AttainedInformation_HiveMind();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReportInformation(AttainedInformation info)
    {
        collatedInformation.MergeInformation(info);
    }
}
