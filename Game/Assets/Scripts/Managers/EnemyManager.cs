using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemyManager : MonoBehaviour {

    #region Public Variables
    public GameObject enemyUnitPrefab;
    #endregion

    #region Protected Variables
    protected List<Enemy> enemyUnits = new List<Enemy>();
    #endregion

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
