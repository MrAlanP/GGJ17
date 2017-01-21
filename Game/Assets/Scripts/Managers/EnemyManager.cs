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
    protected int waveCount = 0;
    #endregion

    // Use this for initialization
    void Start ()
    {
        StartWave();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartWave()
    {
        waveCount++;
        for(int i = 0; i < waveCount; i++)
        {
            GameObject enemyUnit = Instantiate(enemyUnitPrefab);
            enemyUnit.transform.SetParent(unitsTransform);
            Enemy enemy = enemyUnit.GetComponent<Enemy>();
            enemyUnits.Add(enemy);
            enemy.Spawn();
            enemy.gameObject.SetActive(true);
        }

    }

    public void OnInvestigatorFinish(Enemy enemy)
    {
        enemyUnits.Remove(enemy);
        enemy.gameObject.AddComponent<TimedObjectDestructor>();

        if (enemyUnits.Count == 0)
        {
            if (RoomManager.Instance.allRoomsExplored)
            {
                //StartWave Wave
            }
            else
            {
                StartWave();
            }
        }
    }

}
