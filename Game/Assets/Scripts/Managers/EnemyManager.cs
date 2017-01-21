using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemyManager : Singleton<EnemyManager> {

    #region Public Variables
    public float alertness = 0;
    public GameObject enemyUnitPrefab;
    public Transform unitsTransform;
    public float investigationSpeed = 1;
    public float movementSpeed = 1;
    public float sightRange = 25;
    #endregion

    #region Protected Variables
    protected List<Enemy> enemyUnits = new List<Enemy>();
    protected int spawnUnitCount = 1;
    #endregion

    public Dictionary<Room, Enemy> roomsClaimed = new Dictionary<Room, Enemy>();

    

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator SpawnWave(int UnitsToSpawn)
    {
        yield return new WaitForSeconds(2);

        switch ((int)alertness)
        {
            case 1:
                investigationSpeed = 2;
                break;

            case 2:
                movementSpeed = 2;
                break;

            case 3:
                sightRange = 3;
                break;

            case 4:
                break;

            case 5:
                break;
        }

        for(int i = 0; i < UnitsToSpawn; i++)
        {

            GameObject enemyUnit = Instantiate(enemyUnitPrefab);
            enemyUnit.transform.SetParent(unitsTransform);
            enemyUnit.transform.position = RoomManager.Instance.startingRoom;
            Enemy enemy = enemyUnit.GetComponent<Enemy>();
            enemyUnits.Add(enemy);
            enemy.Spawn();
            enemy.gameObject.SetActive(true);
        }

    }

    public virtual void OnEnemyFinish(Enemy enemy)
    {
    }
}
