using UnityEngine;
using System.Collections;

public class InvestigatorManager : EnemyManager {
	
	// Update is called once per frame
	void Update () {
	
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
