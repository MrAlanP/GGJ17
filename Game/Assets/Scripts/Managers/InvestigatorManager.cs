using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvestigatorManager : EnemyManager {

	// Update is called once per frame
	void Update () {
	
	}

    public override void OnEnemyFinish(Enemy enemy)
    {
        WaveManager.Instance.EndWave();

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
                WaveManager.Instance.StartWave();
            }
        }
    }
}
