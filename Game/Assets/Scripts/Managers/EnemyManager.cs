using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemyManager : Singleton<EnemyManager> {

    #region Public Variables
    public GameObject enemyUnitPrefab;
    public Transform unitsTransform;
    #endregion

    #region Protected Variables
    protected List<Enemy> enemyUnits = new List<Enemy>();
    protected int spawnUnitCount = 1;
    #endregion

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartWave()
    {
        for(int i = 0; i<spawnUnitCount; i++)
        {
            GameObject enemyUnit = Instantiate(enemyUnitPrefab);
            enemyUnit.transform.SetParent(unitsTransform);
            Enemy enemy = enemyUnit.GetComponent<Enemy>();
            enemyUnits.Add(enemy);
            enemy.Spawn();
        }

    }
}
