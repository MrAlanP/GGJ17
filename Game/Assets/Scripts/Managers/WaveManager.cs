﻿using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

    public InvestigatorManager investigatorManager;
    public SwatManager swatManager;

    private int currentWave = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartWave()
    {


    }

    public void EndWave()
    {
        currentWave++;

    }
}