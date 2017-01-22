using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvestigatorManager : EnemyManager {

	// Update is called once per frame
	void Update () {
	
	}

    public override void OnEnemyFinish(Enemy enemy, bool destroy)
    {
        roomsClaimed.Clear();

        enemyUnits.Remove(enemy);
        if (destroy)
        {
            enemy.gameObject.AddComponent<TimedObjectDestructor>();
        }

        if (enemyUnits.Count == 0)
        {
            WaveManager.Instance.EndWave();

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
