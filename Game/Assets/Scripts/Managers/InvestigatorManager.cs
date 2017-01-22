using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvestigatorManager : EnemyManager {

    public override void OnEnemyFinish(Enemy enemy, bool destroy)
    {
        roomsClaimed.Clear();

        if(enemy.GetComponent<Enemy_Swat>())
        {
            if(enemy.GetComponent<Enemy_Swat>().hasPlayer)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }

        enemyUnits.Remove(enemy);
        if (destroy && !enemy.GetComponentInChildren<PlayerManager>())
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
