using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemyManager : Singleton<EnemyManager> {

    #region Public Variables
    public float alertness = 0;
    public GameObject InvestigatorPrefab;
    public GameObject SwatPrefab;
    public Transform unitsTransform;
    public float investigationSpeed = 1;
    public float movementSpeed = 1;
    public float sightRange = 25;
    public int extraSpawn = 0;
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

        switch ((int)alertness)
        {
            case 1:
                UIManager.Instance.alert = UIManager.Alert.stage2;
                investigationSpeed = 2;
                break;

            case 2:
                UIManager.Instance.alert = UIManager.Alert.stage3;
                extraSpawn = 1;
                movementSpeed = 2;
                break;

            case 3:
                UIManager.Instance.alert = UIManager.Alert.stage4;
                extraSpawn = 1;
                Enemy.viewDistance = 3;
                break;

            case 4:
                UIManager.Instance.alert = UIManager.Alert.stage5;
                extraSpawn = 2;
                break;
        }

    }

    public IEnumerator SpawnUnits(int UnitsToSpawn, SpawnType unitTypeToSpawn)
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < UnitsToSpawn; i++)
        {
            GameObject enemyUnit = null;

            switch (unitTypeToSpawn)
            {
                case SpawnType.Investigator:
                    enemyUnit = Instantiate(InvestigatorPrefab);
                    break;

                case SpawnType.Swat:

                    enemyUnit = Instantiate(SwatPrefab);
                    break;
            }

            enemyUnit.transform.SetParent(unitsTransform);
            enemyUnit.name = unitTypeToSpawn.ToString() + i;
            //enemyUnit.transform.position = RoomManager.Instance.startingRoom;
            enemyUnit.transform.position = RoomManager.Instance.entranceDoor;
            Enemy enemy = enemyUnit.GetComponent<Enemy>();
            enemyUnits.Add(enemy);
            enemy.Spawn();
            enemy.gameObject.SetActive(true);
        }
    }

    public virtual void OnEnemyFinish(Enemy enemy, bool destroy)
    {
    }
}
